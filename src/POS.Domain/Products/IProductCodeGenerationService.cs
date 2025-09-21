namespace POS.Domain.Products;
public interface IProductCodeGenerationService
{
    Task<ProductCode> GenerateAsync(ProductCategory category);
}
