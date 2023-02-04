using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class LettersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    internal const string LETTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public override string Name { get; } = nameof(LettersPropertyValidator<T>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return value.All(LETTERS.Contains);
    }
}