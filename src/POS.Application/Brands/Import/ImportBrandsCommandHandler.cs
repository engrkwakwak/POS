using Hangfire;
using POS.Application.Abstractions.Jobs;
using POS.Application.Abstractions.Messaging;
using POS.Application.Abstractions.Storage;
using POS.SharedKernel;

namespace POS.Application.Brands.Import;

internal sealed class ImportBrandsCommandHandler : ICommandHandler<ImportBrandsCommand>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public ImportBrandsCommandHandler(
        IBlobStorageService blobStorageService, 
        IBackgroundJobClient backgroundJobClient)
    {
        _blobStorageService = blobStorageService;
        _backgroundJobClient = backgroundJobClient;
    }

    public async Task<Result> Handle(
        ImportBrandsCommand command, 
        CancellationToken cancellationToken)
    {
        string fileIdentifier = $"brands-{Guid.NewGuid()}.csv";

        await _blobStorageService.UploadBrandImportAsync(
            command.FileStream,
            fileIdentifier,
            cancellationToken);

        _backgroundJobClient.Enqueue<IImportBrandsJobs>(
            job => job.ExecuteAsync(fileIdentifier, CancellationToken.None));

        return Result.Success();
    }
}
