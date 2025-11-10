using FluentValidation;

namespace POS.Application.Products.Create;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithErrorCode(ProductErrorCodes.CreateProduct.MissingName)
            .MaximumLength(100).WithErrorCode(ProductErrorCodes.CreateProduct.NameTooLong);

        RuleFor(x => x.Description)
            .NotNull().WithErrorCode(ProductErrorCodes.CreateProduct.NullDescription)
            .MaximumLength(500).WithErrorCode(ProductErrorCodes.CreateProduct.DescriptionTooLong);

        RuleFor(x => x.ProductCategory)
            .IsInEnum().WithErrorCode(ProductErrorCodes.CreateProduct.InvalidCategory);

        RuleFor(x => x.BrandId)
            .NotEmpty().WithErrorCode(ProductErrorCodes.CreateProduct.MissingBrandId);
    }
}
