using FluentValidation;

namespace Lexicon.Common.Validation.Abstractions;
public interface IRuleSet<TProperty>
{
    void Use<T>(IRuleBuilderOptions<T, TProperty> ruleBuilder);
}