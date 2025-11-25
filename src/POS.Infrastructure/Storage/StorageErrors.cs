using POS.SharedKernel;

namespace POS.Infrastructure.Storage;

public static class StorageErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Storage.BlobNotFound",
        "The requested blob was not found.");
}
