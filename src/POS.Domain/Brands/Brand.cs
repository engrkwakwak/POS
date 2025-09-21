using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Brands;
public sealed class Brand : Entity
{
    private Brand(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    private Brand()
    {
    }

    public Name Name { get; private set; }

    public static Brand Create(Name name)
    {
        var brand = new Brand(Guid.NewGuid(), name);

        brand.Raise(new BrandCreatedDomainEvent(brand.Id));

        return brand;
    }
}
