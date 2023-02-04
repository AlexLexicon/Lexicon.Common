using FluentValidation;

namespace Lexicon.Common.Validation.Abstractions;
public abstract class AbstractRuleSet<TProperty> : IRuleSet<TProperty>
{
    public abstract void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder);
}