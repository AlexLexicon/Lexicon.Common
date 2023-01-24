using Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
using System.Configuration;
using System.Reflection;

namespace Lexicon.Common.Wpf.DependencyInjection.Services;
public class SettingsService : ISettingsService
{
    private readonly ApplicationSettingsBase _settings;

    public SettingsService(ApplicationSettingsBase settings)
    {
        _settings = settings;
    }

    public Task Save<T>(T? configuration) where T : class
    {
        if (configuration is not null)
        {
            List<ConfigProperty> configProperties = GetConfigProperties(typeof(T).Name, configuration);

            foreach (var configProperty in configProperties)
            {
                _settings[configProperty.SettingKey] = configProperty.Value;
            }

            _settings.Save();
        }

        return Task.CompletedTask;
    }

    private List<ConfigProperty> GetConfigProperties(string currentPath, object? value, PropertyInfo? propertyInfo = null)
    {
        string settingKey = propertyInfo is null ? currentPath : $"{currentPath}_{propertyInfo.Name}";

        if (propertyInfo is not null)
        {
            Type propertyType = propertyInfo.PropertyType;

            if (propertyType == typeof(bool) ||
                propertyType == typeof(byte) ||
                propertyType == typeof(char) ||
                propertyType == typeof(decimal) ||
                propertyType == typeof(double) ||
                propertyType == typeof(float) ||
                propertyType == typeof(int) ||
                propertyType == typeof(long) ||
                propertyType == typeof(sbyte) ||
                propertyType == typeof(short) ||
                propertyType == typeof(string) ||
                propertyType == typeof(System.Collections.Specialized.StringCollection) ||
                propertyType == typeof(DateTime) ||
                propertyType == typeof(System.Drawing.Color) ||
                propertyType == typeof(System.Drawing.Point) ||
                propertyType == typeof(System.Drawing.Size) ||
                propertyType == typeof(Guid) ||
                propertyType == typeof(TimeSpan) ||
                propertyType == typeof(uint) ||
                propertyType == typeof(ulong) ||
                propertyType == typeof(ushort))
            {
                return new List<ConfigProperty>
                {
                    new ConfigProperty
                    {
                        SettingKey = settingKey,
                        Value = value,
                    }
                };
            }

            var nullableConfigPropertyValue = new NullableConfigPropertyValue();
            SetConfigPropertyNullableType<bool>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<byte>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<char>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<decimal>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<double>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<float>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<int>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<long>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<sbyte>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<short>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<DateTime>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<Guid>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<TimeSpan>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<uint>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<ulong>(propertyType, value, ref nullableConfigPropertyValue);
            SetConfigPropertyNullableType<ushort>(propertyType, value, ref nullableConfigPropertyValue);

            if (nullableConfigPropertyValue.IsNullable)
            {
                return new List<ConfigProperty>
                {
                    new ConfigProperty
                    {
                        SettingKey = settingKey,
                        Value = nullableConfigPropertyValue.Value,
                    }
                };
            }
        }

        if (value is not null)
        {
            Type valueType = value.GetType();

            List<ConfigProperty> valueConfigProperties = new List<ConfigProperty>();

            foreach (PropertyInfo valuePropertyInfo in valueType.GetProperties())
            {
                object? valueValue = valuePropertyInfo.GetValue(value);

                var configProperties = GetConfigProperties(settingKey, valueValue, valuePropertyInfo);

                valueConfigProperties.AddRange(configProperties);
            }

            return valueConfigProperties;
        }

        return new List<ConfigProperty>();
    }

    private void SetConfigPropertyNullableType<T>(Type propertyType, object? value, ref NullableConfigPropertyValue configProperty, T defaultValue = default) where T : struct
    {
        if (!configProperty.IsNullable)
        {
            if (propertyType == typeof(T?))
            {
                T? nullableValue = (T?)value;

                object? configValue = defaultValue;

                if (nullableValue.HasValue)
                {
                    configValue = nullableValue.Value;
                }

                configProperty.Value = (T)configValue;
                configProperty.IsNullable = true;
            }
        }
    }

    private class NullableConfigPropertyValue
    {
        public NullableConfigPropertyValue()
        {
            IsNullable = false;
        }

        public bool IsNullable { get; set; }
        public object? Value { get; set; }
    }

    private class ConfigProperty
    {
        public required string SettingKey { get; set; }
        public required object? Value { get; set; }
    }
}