using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Amenities.Extensions;

namespace Lexicon.Common.Validation.Amenities.RuleSets;
public interface IGuidRuleSet : IGuidRuleSet<Guid>
{
}
public class GuidRuleSet : GuidRuleSet<Guid>, IGuidRuleSet<Guid>
{
}
public interface IGuidRuleSet<TProperty> : IRuleSet<TProperty>
{
}
public class GuidRuleSet<TProperty> : AbstractRuleSet<TProperty>, IGuidRuleSet<TProperty>
{
    public override void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        ruleBuilder
            .Guid()
            .DependentRules(() =>
            {
                ruleBuilder.NotSimplyEmpty();
            });
    }
}