using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Lexicon.Common.Wpf.DependencyInjection.ConfigurationProviders;
public class SettingsConfigurationProvider : ConfigurationProvider
{
    private readonly ApplicationSettingsBase? _settings;

    public SettingsConfigurationProvider(ApplicationSettingsBase? settings)
    {
        _settings = settings;

        if (_settings is not null)
        {
            _settings.SettingsSaving += (sender, e) => Load();
        }
    }

    public override void Load()
    {
        if (_settings is not null)
        {
            bool reload = false;

            foreach (var setting in _settings.Properties)
            {
                if (setting is SettingsProperty settingsProperty)
                {
                    string propertyName = settingsProperty.Name;
                    string dataKey = propertyName.Replace('_', ':');
                    string? dataValue = _settings[propertyName]?.ToString();

                    if (Data.ContainsKey(dataKey))
                    {
                        if (Data[dataKey] != dataValue)
                        {
                            Data[dataKey] = dataValue;
                            reload = true;
                        }
                    }
                    else
                    {
                        Data.Add(dataKey, dataValue);
                        reload = true;
                    }
                }
            }

            if (reload)
            {
                OnReload();
            }
        }
    }
}
