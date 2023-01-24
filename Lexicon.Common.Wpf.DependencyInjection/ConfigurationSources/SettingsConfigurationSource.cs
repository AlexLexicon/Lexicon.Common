using Lexicon.Common.Wpf.DependencyInjection.ConfigurationProviders;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Lexicon.Common.Wpf.DependencyInjection.ConfigurationSources;
public class SettingsConfigurationSource : IConfigurationSource
{
    public ApplicationSettingsBase? Settings { get; set; }
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return new SettingsConfigurationProvider(Settings);
    }
}
