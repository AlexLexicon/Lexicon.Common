namespace Lexicon.Common.Validation.Abstractions;
public interface IRuleSetValidator<TRuleSet, TProperty> : IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    IEnumerable<string> ValidationErrors { get; }
    Func<TProperty, IEnumerable<string>> Validation { get; }
}