using FluentValidation;
using POS.Domain.Products;
using POS.Domain.Shared;

namespace POS.Application.Products.CreateVariant;

public class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand>
{
    public CreateVariantCommandValidator(IProductVariantRepository productVariantRepository)
    {
        RuleFor(x => x.Barcode)
            .NotEmpty().WithErrorCode(ProductErrorCodes.CreateVariant.MissingBarcode)
            .MaximumLength(20).WithErrorCode(ProductErrorCodes.CreateVariant.BarcodeTooLong)
            .MustAsync(productVariantRepository.IsBarcodeUniqueAsync).WithErrorCode(ProductErrorCodes.CreateVariant.DuplicateBarcode);

        RuleFor(x => x.PriceAmount)
            .GreaterThanOrEqualTo(0).WithErrorCode(ProductErrorCodes.CreateVariant.NegativePrice)
            .PrecisionScale(18, 2, true).WithErrorCode(ProductErrorCodes.CreateVariant.PricePrecisionExceeded);

        RuleFor(x => x.PriceCurrencyCode)
            .NotEmpty().WithErrorCode(ProductErrorCodes.CreateVariant.MissingCurrencyCode)
            .Must(code => code.Length == 3).WithErrorCode(ProductErrorCodes.CreateVariant.InvalidCurrencyCode)
            .Must(code => Currency.All.Any(c => c.Code == code)).WithErrorCode(ProductErrorCodes.CreateVariant.InvalidCurrencyCode);

        RuleFor(x => x.PackageType)
            .IsInEnum().WithErrorCode(ProductErrorCodes.CreateVariant.InvalidPackageType);

        RuleFor(x => x.PackageQuantity)
            .GreaterThan(0).WithErrorCode(ProductErrorCodes.CreateVariant.NegativeQuantity)
            .GreaterThan(1).When(x => x.PackageType == PackageType.Box).WithErrorCode(ProductErrorCodes.CreateVariant.MismatchPackaging)
            .Equal(1).When(x => x.PackageType == PackageType.Piece).WithErrorCode(ProductErrorCodes.CreateVariant.MismatchPackaging);

        RuleFor(x => x.MeasurementAmount)
            .GreaterThan(0).WithErrorCode(ProductErrorCodes.CreateVariant.InvalidMeasurementAmount)
            .PrecisionScale(18, 2, true).WithErrorCode(ProductErrorCodes.CreateVariant.MeasurementAmountPrecisionExceeded);

        RuleFor(x => x.UnitOfMeasureCode)
            .NotEmpty().WithErrorCode(ProductErrorCodes.CreateVariant.MissingUnitOfMeasureCode)
            .Must(code => UnitOfMeasure.All.Any(u => u.Code == code)).WithErrorCode(ProductErrorCodes.CreateVariant.InvalidUnitOfMeasureCode);
    }
}
