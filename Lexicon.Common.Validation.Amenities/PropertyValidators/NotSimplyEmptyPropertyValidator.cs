using FluentValidation;
using System.Collections;

namespace Lexicon.Common.Validation.Amenities.PropertyValidators;
public class NotSimplyEmptyPropertyValidator<T, TProperty> : AbstractPropertyValidator<T, TProperty>
{
    public override string Name { get; } = nameof(NotSimplyEmptyPropertyValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        ArgumentNullException.ThrowIfNull(context);

        switch (value)
        {
            case null:
                return true;
            case string s when s == string.Empty:
            case Guid g when g == Guid.Empty:
            case ICollection { Count: 0 }:
            case Array { Length: 0 }:
            case IEnumerable e when !e.GetEnumerator().MoveNext():
                return false;
        }

        return true;
    }
}