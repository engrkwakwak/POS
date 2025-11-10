using POS.SharedKernel;

namespace POS.Domain.Brands;
public static class BrandErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Brand.NotFound",
        "The specified brand was not found.");
}
