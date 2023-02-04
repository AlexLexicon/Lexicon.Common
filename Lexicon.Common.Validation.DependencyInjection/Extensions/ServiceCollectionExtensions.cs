using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Lexicon.Common.Validation.DependencyInjection.Abstractions;
using Lexicon.Common.Validation.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicon.Common.Validation.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLexiconValidation(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        CreateAddValidationBuilder(services, configuration, null);

        return services;
    }
    public static IServiceCollection AddLexiconValidation(this IServiceCollection services, IConfiguration configuration, Action<IAddValidationBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(configure);

        CreateAddValidationBuilder(services, configuration, configure);

        return services;
    }
    public static void CreateAddValidationBuilder(IServiceCollection services, IConfiguration configuration, Action<IAddValidationBuilder>? configure)
    {
        var addValidationBuilder = new AddValidationBuilder(services, configuration);

        addValidationBuilder.LexiconLanguageManager.AddLexiconTranslations();

        ValidatorOptions.Global.LanguageManager = addValidationBuilder.LexiconLanguageManager;

        addValidationBuilder.Services.TryAddScoped(typeof(IRuleSetValidator<,>), typeof(RuleSetValidator<,>));

        configure?.Invoke(addValidationBuilder);
    }
}
