using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Amenities.Configurations;
using Lexicon.Common.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicon.Common.Validation.Amenities.RuleSets;
public interface IEmailRuleSet : IRuleSet<string?>
{
}
public class EmailRuleSet : AbstractRuleSet<string?>, IEmailRuleSet
{
    private const int DEFAULT_LENGTH_MINIMUM = 3;
    private const int DEFAULT_LENGTH_MAXIMUM = 254;

    private readonly IOptions<EmailRuleSetConfiguration> _emailRuleSetOptions;

    public EmailRuleSet(IOptions<EmailRuleSetConfiguration> emailRuleSetOptions)
    {
        _emailRuleSetOptions = emailRuleSetOptions;
    }

    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        EmailRuleSetConfiguration emailRuleSetConfiguration = _emailRuleSetOptions.Value;

        int? lengthMinimum = emailRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = emailRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .NotAnyWhiteSpace()
            .EmailAddress()
            .Length(lengthMinimum, lengthMaximum);
    }
}
