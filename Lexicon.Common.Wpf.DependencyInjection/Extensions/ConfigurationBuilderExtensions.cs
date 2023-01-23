using Lexicon.Common.Wpf.DependencyInjection.ConfigurationSources;
using Lexicon.Common.Wpf.DependencyInjection.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Configuration;

namespace Lexicon.Common.Wpf.DependencyInjection.Extensions;
public static class ConfigurationBuilderExtensions
{
    public static void AddSettings(this IConfigurationBuilder configurationBuilder, ApplicationSettingsBase settings)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        ArgumentNullException.ThrowIfNull(settings);

        configurationBuilder.Add<SettingsConfigurationSource>(source =>
        {
            source.Settings = settings;
        });
    }
    public static void AddSettings(this IConfigurationBuilder configurationBuilder, IServiceCollection services, ApplicationSettingsBase settings)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(settings);

        AddSettings(configurationBuilder, settings);

        services.AddSingleton(settings);

        services.TryAddSingleton<ISettingsService, SettingsService>();
    }
}
