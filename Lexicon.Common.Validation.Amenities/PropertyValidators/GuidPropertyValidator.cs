using FluentValidation;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class GuidPropertyValidator<T, TProperty> : AbstractPropertyValidator<T, TProperty>
{
    public override string Name { get; } = nameof(GuidPropertyValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        ArgumentNullException.ThrowIfNull(context);

        if (value is null)
        {
            return true;
        }

        if (value is Guid)
        {
            return true;
        }

        if (value is string str && Guid.TryParse(str, out Guid _))
        {
            return true;
        }

        return false;
    }
}