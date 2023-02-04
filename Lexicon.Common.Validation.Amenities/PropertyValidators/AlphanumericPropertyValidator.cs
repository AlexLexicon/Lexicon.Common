using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class AlphanumericPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public override string Name { get; } = nameof(AlphanumericPropertyValidator<T>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return value.All(c => DigitsPropertyValidator<object>.DIGITS.Contains(c) || LettersPropertyValidator<object>.LETTERS.Contains(c));
    }
}