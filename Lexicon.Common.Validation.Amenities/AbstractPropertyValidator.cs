using FluentValidation.Validators;

namespace Lexicon.Common.Validation.Amenities;
public abstract class AbstractPropertyValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    protected override string GetDefaultMessageTemplate(string errorCode) => Localized(errorCode, Name);
}
