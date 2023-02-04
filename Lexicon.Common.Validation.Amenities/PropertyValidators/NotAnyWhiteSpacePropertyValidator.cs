using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class NotAnyWhiteSpacePropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public override string Name { get; } = nameof(NotAnyWhiteSpacePropertyValidator<T>);

    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        return !value.Contains(' ');
    }
}