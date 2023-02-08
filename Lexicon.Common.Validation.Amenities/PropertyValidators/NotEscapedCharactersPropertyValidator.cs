using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class NotEscapedCharactersPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    internal const string ESCAPED_CHARACTERS = "\n\r\t";

    public override string Name => nameof(NotEscapedCharactersPropertyValidator<object>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return !value.Any(ESCAPED_CHARACTERS.Contains);
    }
}