using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using POS.Application.Abstractions.Storage;
using POS.SharedKernel;

namespace POS.Infrastructure.Storage;

internal sealed class BlobStorageService(
    BlobServiceClient blobServiceClient,
    IOptions<BlobStorageOptions> options) : IBlobStorageService
{
    private readonly BlobStorageOptions _options = options.Value;

    private async Task<BlobContainerClient> GetBrandImportsContainerAsync(CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_options.Containers.BrandImports);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);
        return containerClient;
    }

    public async Task DeleteBrandImportAsync(string blobName, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = await GetBrandImportsContainerAsync(cancellationToken);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<Result<Stream>> DownloadBrandImportAsync(
        string blobName, 
        CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = await GetBrandImportsContainerAsync(cancellationToken);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        if(!await blobClient.ExistsAsync(cancellationToken))
        {
            return Result.Failure<Stream>(StorageErrors.NotFound);
        }

        Response<BlobDownloadStreamingResult> response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);
        return Result.Success(response.Value.Content);
    }

    public async Task UploadBrandImportAsync(Stream stream, string blobName, CancellationToken cancellationToken)
    {
        BlobContainerClient containerClient = await GetBrandImportsContainerAsync(cancellationToken);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.UploadAsync(stream, overwrite: true, cancellationToken);
    }
}
