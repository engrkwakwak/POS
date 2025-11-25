using POS.SharedKernel;

namespace POS.Application.Abstractions.Storage;

public interface IBlobStorageService
{
    Task UploadBrandImportAsync(Stream stream, string blobName, CancellationToken cancellationToken);

    Task<Result<Stream>> DownloadBrandImportAsync(string blobName, CancellationToken cancellationToken);

    Task DeleteBrandImportAsync(string blobName, CancellationToken cancellationToken);
}
