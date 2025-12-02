using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.Domain.Products;
using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Application.Products.CreateVariant;

internal sealed class CreateVariantCommandHandler
    : ICommandHandler<CreateVariantCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVariantCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateVariantCommand request, 
        CancellationToken cancellationToken)
    {
        Barcode barcode = new(request.Barcode);
        Money price = new(request.PriceAmount, Currency.FromCode(request.PriceCurrencyCode));

        Result<Packaging> packagingResult = request.PackageType switch
        {
            PackageType.Piece => Result.Success(Packaging.Piece()),
            PackageType.Box => Packaging.Box(request.PackageQuantity),
            _ => Result.Failure<Packaging>(ProductErrors.Packaging.InvalidType)
        };

        if (packagingResult.IsFailure)
        {
            return Result.Failure<Guid>(packagingResult.Error);
        }

        Packaging packaging = packagingResult.Value;

        Result<Measurement> measurementResult = Measurement.Create(
            request.MeasurementAmount,
            UnitOfMeasure.FromCode(request.UnitOfMeasureCode));

        if (measurementResult.IsFailure)
        {
            return Result.Failure<Guid>(measurementResult.Error);
        }

        Measurement measurement = measurementResult.Value;

        Product? product = await _productRepository.GetByIdAsync(
            request.ProductId, 
            cancellationToken);

        if (product is null)
        {
            return Result.Failure<Guid>(ProductErrors.NotFound(request.ProductId));
        }

        Result<ProductVariant> addVariantResult = product.AddVariant(
            barcode,
            price,
            packaging,
            measurement,
            request.IsVatable);

        if (addVariantResult.IsFailure)
        { 
            return Result.Failure<Guid>(addVariantResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(addVariantResult.Value.Id);
    }
}
