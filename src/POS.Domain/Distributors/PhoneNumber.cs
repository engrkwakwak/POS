using PhoneNumbers;
using POS.SharedKernel;

namespace POS.Domain.Distributors;

public sealed record PhoneNumber
{
    private static readonly PhoneNumberUtil PhoneUtil = PhoneNumberUtil.GetInstance();

    public string FullNumber { get; }
    public string Region { get; }

    private PhoneNumber(string fullNumber, string region)
    {
        FullNumber = fullNumber;
        Region = region;
    }

    public static Result<PhoneNumber> Create(string? number, string regionCode)
    {
        if (string.IsNullOrEmpty(number) || string.IsNullOrEmpty(regionCode))
        {
            return Result.Failure<PhoneNumber>(PhoneNumberErrors.Empty);
        }

        try
        {
            PhoneNumbers.PhoneNumber phoneNumberProto = PhoneUtil.Parse(number, regionCode.ToUpperInvariant());

            if (!PhoneUtil.IsValidNumber(phoneNumberProto))
            {
                return Result.Failure<PhoneNumber>(PhoneNumberErrors.Invalid);
            }

            string formattedNumber = PhoneUtil.Format(phoneNumberProto, PhoneNumberFormat.E164);

            return new PhoneNumber(formattedNumber, regionCode.ToUpperInvariant());
        }
        catch (NumberParseException)
        {
            return Result.Failure<PhoneNumber>(PhoneNumberErrors.Invalid);
        }
    }
}
