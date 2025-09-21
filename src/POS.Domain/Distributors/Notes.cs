using POS.SharedKernel;

namespace POS.Domain.Distributors;

public sealed record Notes
{
    public Notes(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
