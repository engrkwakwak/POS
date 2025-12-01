namespace POS.Infrastructure.Storage;

public sealed class BlobStorageOptions
{
    public const string SectionName = "BlobStorage";

    public ContainersOptions Containers { get; set; } = new();
}

public sealed class ContainersOptions
{
    public string BrandImports { get; set; } = string.Empty;
}
