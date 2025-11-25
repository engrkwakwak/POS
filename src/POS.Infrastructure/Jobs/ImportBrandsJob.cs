using System.Globalization;
using CsvHelper;
using Microsoft.Extensions.Logging;
using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Jobs;
using POS.Application.Abstractions.Storage;
using POS.Domain.Brands;
using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Infrastructure.Jobs;

internal sealed class ImportBrandsJob : IImportBrandsJobs
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly ILogger<ImportBrandsJob> _logger;
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ImportBrandsJob(
        IBlobStorageService blobStorageService, 
        ILogger<ImportBrandsJob> logger, 
        IBrandRepository brandRepository, 
        IUnitOfWork unitOfWork)
    {
        _blobStorageService = blobStorageService;
        _logger = logger;
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(string fileIdentifier, CancellationToken cancellationToken)
    {
        Result<Stream> downloadResult = await _blobStorageService.DownloadBrandImportAsync(
            fileIdentifier,
            cancellationToken);

        if (downloadResult.IsFailure)
        {
            _logger.LogError(
                "Failed to download brand import file {FileIdentifier}: {Error}",
                fileIdentifier,
                downloadResult.Error);

            return;
        }

        using Stream fileSteam = downloadResult.Value;
        using var reader = new StreamReader(fileSteam);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        try
        {
            HashSet<string> processedNames = new(StringComparer.OrdinalIgnoreCase);
            List<Brand> brandsToProcess = [];
            await foreach (BrandImportDto record in csv.GetRecordsAsync<BrandImportDto>(cancellationToken))
            {
                if (string.IsNullOrWhiteSpace(record.Name))
                {
                    continue;
                }

                string trimmedName = record.Name.Trim();

                if (processedNames.Contains(trimmedName))
                {
                    continue;
                }

                processedNames.Add(trimmedName);
                brandsToProcess.Add(Brand.Create(new Name(trimmedName)));
            }

            if (brandsToProcess.Count == 0)
            {
                _logger.LogWarning(
                    "Brand import file {FileIdentifier} contained no valid brands.",
                    fileIdentifier);
                return;
            }

            List<Brand> existingBrands = await _brandRepository
                .GetByNamesAsync(
                    processedNames,
                    cancellationToken);

            var existingDbNames = existingBrands
                .Select(b => b.Name.Value)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var brandsToInsert = brandsToProcess
                .Where(b => !existingDbNames.Contains(b.Name.Value))
                .ToList();

            if (brandsToInsert.Count > 0)
            {
                foreach (Brand brand in brandsToInsert)
                {
                    _brandRepository.Insert(brand);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Successfully imported {Count} new brands from file {FileIdentifier}.",
                    brandsToInsert.Count,
                    fileIdentifier);
            }
            else
            {
                _logger.LogInformation(
                    "Brand import file {FileIdentifier} processed, but all brands already existed.",
                    fileIdentifier);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "CRITICAL failure occurred while processing brand import {FileIdentifier}. Check specific exception type.",
                fileIdentifier);
        }
        finally
        {
            await _blobStorageService.DeleteBrandImportAsync(fileIdentifier, CancellationToken.None);
        }
    }

    private sealed record BrandImportDto(string Name);
}
