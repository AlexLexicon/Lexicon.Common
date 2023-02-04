using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class DigitsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    internal const string DIGITS = "0123456789";

    public override string Name { get; } = nameof(DigitsPropertyValidator<T>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return value.All(DIGITS.Contains);
    }
}