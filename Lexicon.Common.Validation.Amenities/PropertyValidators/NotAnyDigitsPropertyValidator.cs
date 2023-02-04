using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class NotAnyDigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    private const string DIGITS = "0123456789";

    public override string Name { get; } = nameof(NotAnyDigitsPropertyValidator<T>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return !value.Any(DIGITS.Contains);
    }
}