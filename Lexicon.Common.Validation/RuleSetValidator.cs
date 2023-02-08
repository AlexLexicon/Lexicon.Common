using FluentValidation.Results;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Abstractions.Extensions;
using Lexicon.Common.Validation.Extensions;

namespace Lexicon.Common.Validation;
public class RuleSetValidator<TRuleSet, TProperty> : AbstractValueValidator<TProperty>, IRuleSetValidator<TRuleSet, TProperty> where TRuleSet : IRuleSet<TProperty>
{
    public RuleSetValidator(TRuleSet ruleSet)
    {
        ValidationErrors = new List<string>();
        Validation = ValidateAndGetErrorMessages;

        RuleFor(v => v.Value).UseRuleSet(ruleSet);
    }

    public IReadOnlyList<string> ValidationErrors { get; protected set; }

    public Func<TProperty, IEnumerable<string>> Validation { get; }

    public bool IsValid => !ValidationErrors.Any();

    protected virtual IEnumerable<string> ValidateAndGetErrorMessages(TProperty instance)
    {
        ValidationResult result = Validate(instance);

        ValidationErrors = result.Errors.Sanitize().ToFrontEndErrorMessages();

        return ValidationErrors;
    }
}