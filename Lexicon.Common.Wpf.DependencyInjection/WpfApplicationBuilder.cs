using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplicationBuilder
{
    private readonly IServiceCollection _services;
    private readonly ConfigurationManager _configurationManager;

    internal WpfApplicationBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        _services = new ServiceCollection();
        _configurationManager = new ConfigurationManager();

        //we need the service provider for the DataContextFactory
        //maybe this isnt the best solution
        //however I dont want to have to inject many DataContextFactories<T>
        //for each type of DataContext. Instead just using IDataContextFactory is nice
        _services.AddSingleton(sp => sp);
        _services.AddSingleton(appxaml);
        _services.AddSingleton(appxaml.Dispatcher);
    }

    public IServiceCollection Services => _services;

    public ConfigurationManager Configuration => _configurationManager;

    public WpfApplication Build()
    {
        return new WpfApplication(_configurationManager, _services);
    }
}
