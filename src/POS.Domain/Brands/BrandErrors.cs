using POS.SharedKernel;

namespace POS.Domain.Brands;
public static class BrandErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Brand.NotFound",
        "The specified brand was not found.");

    public static class Upload
    {
        public static readonly Error EmptyFile = Error.Problem(
            "Brand.Upload.EmptyFile",
            "The file must not be empty.");

        public static readonly Error InvalidFileType = Error.Problem(
            "Brand.Upload.InvalidFileType",
            "The uploaded file must be a csv.");

        public static readonly Error InvalidContentType = Error.Problem(
            "Brand.Upload.InvalidContentType",
            "The file content type must be 'text/csv'");
    }

    public static class Processing
    {
        public static readonly Error CorruptFile = Error.Problem(
            "Brand.Processing.CorruptFile",
            "The CSV file is corrupt or in an invalid format.");
    }
}
