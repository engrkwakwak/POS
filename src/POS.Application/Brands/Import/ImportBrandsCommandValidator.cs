using FluentValidation;

namespace POS.Application.Brands.Import;

public sealed class ImportBrandsCommandValidator : AbstractValidator<ImportBrandsCommand>
{
    public ImportBrandsCommandValidator()
    {
        RuleFor(c => c.FileStream)
            .NotNull();

        RuleFor(c => c.FileName)
            .Must(f => f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            .WithErrorCode(BrandErrorCodes.UploadBrands.InvalidFileType);

        RuleFor(c => c.ContentType)
            .Must(c => c == "text/csv")
            .WithErrorCode(BrandErrorCodes.UploadBrands.InvalidContentType);
    }
}
