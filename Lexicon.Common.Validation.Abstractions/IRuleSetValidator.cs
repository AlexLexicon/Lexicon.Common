namespace Lexicon.Common.Validation.Abstractions;
public interface IRuleSetValidator<TRuleSet, TProperty> : IValueValidator<TProperty> where TRuleSet : IRuleSet<TProperty>
{
    IReadOnlyList<string> ValidationErrors { get; }
    bool IsValid { get; }
    Func<TProperty, IEnumerable<string>> Validation { get; }
}