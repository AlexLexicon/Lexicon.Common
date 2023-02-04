using FluentValidation;
using FluentValidation.Results;
using Lexicon.Common.Validation.Abstractions;

namespace Lexicon.Common.Validation;
public abstract class AbstractValueValidator<TProperty> : AbstractValidator<ValidationValue<TProperty>>, IValueValidator<TProperty>
{
    public ValidationResult Validate(TProperty instance)
    {
        return Validate(new ValidationValue<TProperty>
        {
            Value = instance
        });
    }

    public Task<ValidationResult> ValidateAsync(TProperty instance, CancellationToken cancellation = new())
    {
        return ValidateAsync(new ValidationValue<TProperty>
        {
            Value = instance
        }, cancellation);
    }
}
