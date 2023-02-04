using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Amenities.Configurations;
using Lexicon.Common.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicon.Common.Validation.Amenities.RuleSets;
public interface INameRuleSet : IRuleSet<string?>
{
}
public class NameRuleSet : AbstractRuleSet<string?>, INameRuleSet
{
    private const int DEFAULT_LENGTH_MINIMUM = 1;
    private static readonly int? DEFAULT_LENGTH_MAXIMUM = null;

    private readonly IOptions<NameRuleSetConfiguration> _nameRuleSetOptions;

    public NameRuleSet(IOptions<NameRuleSetConfiguration> nameRuleSetOptions)
    {
        _nameRuleSetOptions = nameRuleSetOptions;
    }

    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        NameRuleSetConfiguration nameRuleSetConfiguration = _nameRuleSetOptions.Value;

        int? lengthMinimum = nameRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = nameRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace()
            .Letters()
            .NotAnyDigits()
            .Length(lengthMinimum, lengthMaximum);
    }
}
