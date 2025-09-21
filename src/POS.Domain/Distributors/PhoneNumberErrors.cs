using POS.SharedKernel;

namespace POS.Domain.Distributors;
public static class PhoneNumberErrors
{
    public static readonly Error Empty = Error.Validation(
        "PhoneNumber.Empty", 
        "Phone number cannot be empty.");
    public static readonly Error Invalid = Error.Validation(
        "PhoneNumber.Invalid",
        "The provided phone number is not valid for the selected region.");
}
