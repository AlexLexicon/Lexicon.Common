using Lexicon.Common.DependencyInjection.Extensions;
using Lexicon.Common.Validation.Amenities.Configurations;
using Lexicon.Common.Validation.Amenities.Extensions;
using Lexicon.Common.Validation.Amenities.RuleSets;
using Lexicon.Common.Validation.DependencyInjection.Abstractions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicon.Common.Validation.DependencyInjection.Amenities.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLexiconAmenities(this IServiceCollection services, IConfiguration configuration, LexiconLanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.ConfigureAndBind<EmailRuleSetConfiguration>(configuration);
        services.ConfigureAndBind<NameRuleSetConfiguration>(configuration);
        services.ConfigureAndBind<PasswordRequirementsRuleSetConfiguration>(configuration);

        languageManager.AddLexiconAmenitiesTranslations();

        services.AddLexiconRuleSets<IEmailRuleSet>();

        return services;
    }
}
