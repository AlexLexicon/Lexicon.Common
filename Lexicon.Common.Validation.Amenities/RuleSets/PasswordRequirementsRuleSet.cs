using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.Amenities.Configurations;
using Lexicon.Common.Validation.Amenities.Extensions;
using Microsoft.Extensions.Options;

namespace Lexicon.Common.Validation.Amenities.RuleSets;
public interface IPasswordRequirementsRuleSet : IRuleSet<string?>
{
}
public class PasswordRequirementsRuleSet : AbstractRuleSet<string?>, IPasswordRequirementsRuleSet
{
    private const string ALPHANUMERIC = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    //standard microsoft identity password requirements
    private const int DEFAULT_LENGTH_MINIMUM = 6;
    private static readonly int? DEFAULT_LENGTH_MAXIMUM = null;
    private const bool DEFAULT_REQUIRES_DIGIT = true;
    private const bool DEFAULT_REQUIRES_UPPER = true;
    private const bool DEFAULT_REQUIRES_LOWER = true;
    private const bool DEFAULT_REQUIRES_NONALPHANUMERIC = true;
    private const int REQUIRED_UNIQUE_CHARS = 1;

    private readonly IOptions<PasswordRequirementsRuleSetConfiguration> _passwordRequirementsRuleSetOptions;

    public PasswordRequirementsRuleSet(IOptions<PasswordRequirementsRuleSetConfiguration> passwordRequirementsRuleSetOptions)
    {
        _passwordRequirementsRuleSetOptions = passwordRequirementsRuleSetOptions;
    }

    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        PasswordRequirementsRuleSetConfiguration passwordRequirementsRuleSetConfiguration = _passwordRequirementsRuleSetOptions.Value;

        int? lengthMinimum = passwordRequirementsRuleSetConfiguration.MinimumLength ?? DEFAULT_LENGTH_MINIMUM;
        int? lengthMaximum = passwordRequirementsRuleSetConfiguration.MaximumLength ?? DEFAULT_LENGTH_MAXIMUM;

        ruleBuilder
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces()
            .Length(lengthMinimum, lengthMaximum);

        bool? requiresDigit = passwordRequirementsRuleSetConfiguration.RequireDigit ?? DEFAULT_REQUIRES_DIGIT;
        bool? requiresUpper = passwordRequirementsRuleSetConfiguration.RequireUppercase ?? DEFAULT_REQUIRES_UPPER;
        bool? requiresLower = passwordRequirementsRuleSetConfiguration.RequireLowercase ?? DEFAULT_REQUIRES_LOWER;
        bool? requireNonAlphanumeric = passwordRequirementsRuleSetConfiguration.RequireNonAlphanumeric ?? DEFAULT_REQUIRES_NONALPHANUMERIC;
        int? requiredUniqueChars = passwordRequirementsRuleSetConfiguration.RequiredUniqueChars ?? REQUIRED_UNIQUE_CHARS;

        if (requiresDigit == true)
        {
            ruleBuilder
                .Must(v => v is null || v is not null && v.Any(char.IsDigit))
                .WithMessage("Must contain at least 1 digit character.");
        }

        if (requiresUpper == true)
        {
            ruleBuilder
                 .Must(v => v is null || v is not null && v.Any(char.IsUpper))
                .WithMessage("Must contain at least 1 upper case character.");
        }

        if (requiresLower == true)
        {
            ruleBuilder
                .Must(v => v is null || v is not null && v.Any(char.IsLower))
                .WithMessage("Must contain at least 1 lower case character.");
        }

        if (requireNonAlphanumeric == true)
        {
            ruleBuilder
                .Must(v => v is null || v is not null && v.Any(v => !ALPHANUMERIC.Contains(v)))
                .WithMessage("Must contain at least 1 non-alphanumeric character.");
        }

        if (requiredUniqueChars is not null)
        {
            ruleBuilder
                .Must(v => v is null || v is not null && v.Distinct().Count() >= requiredUniqueChars.Value)
                .WithMessage($"Must use at least {requiredUniqueChars.Value} unique characters.");
        }
    }
}
