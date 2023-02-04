using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Amenities.Extensions;

namespace Lexicon.Common.Validation.Amenities.RuleSets;
public interface IRequiredRuleSet : IRequiredRuleSet<string?>
{
}
public class RequiredRuleSet : RequiredRuleSet<string?>, IRequiredRuleSet
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace();
    }
}
public interface IRequiredRuleSet<TProperty> : IRuleSet<TProperty>
{
}
public class RequiredRuleSet<TProperty> : AbstractRuleSet<TProperty>, IRequiredRuleSet<TProperty>
{
    public override void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty();
    }
}
