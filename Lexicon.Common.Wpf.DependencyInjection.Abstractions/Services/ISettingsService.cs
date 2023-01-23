namespace Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
public interface ISettingsService
{
    Task Save<T>(T configuration) where T : class;
}
