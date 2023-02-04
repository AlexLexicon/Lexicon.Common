using FluentValidation;
using System.Collections;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class SimplyEmptyPropertyValidator<T, TProperty> : AbstractPropertyValidator<T, TProperty>
{
    public override string Name { get; } = nameof(SimplyEmptyPropertyValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        ArgumentNullException.ThrowIfNull(context);

        switch (value)
        {
            case null:
            case string s when s == string.Empty:
            case Guid g when g == Guid.Empty:
            case ICollection { Count: 0 }:
            case Array { Length: 0 }:
            case IEnumerable e when !e.GetEnumerator().MoveNext():
                return true;
        }

        return false;
    }
}