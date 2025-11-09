using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.Domain.Brands;
using POS.Domain.Products;
using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Application.Products.Create;
internal sealed class CreateProductCommandHandler 
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IBrandRepository brandRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _brandRepository = brandRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var name = new Name(request.Name);
        var description = new Description(request.Description);

        bool brandExists = await _brandRepository.ExistsAsync(
            request.BrandId,
            cancellationToken);
        if (!brandExists)
        {
            return Result.Failure<Guid>(BrandErrors.NotFound);
        }

        Result<Product> productResult = Product.Create(
            name,
            description,
            request.ProductCategory,
            request.BrandId,
            request.IsVatable);

        if (productResult.IsFailure)
        {
            return Result.Failure<Guid>(productResult.Error);
        }

        Product product = productResult.Value;

        _productRepository.Insert(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
