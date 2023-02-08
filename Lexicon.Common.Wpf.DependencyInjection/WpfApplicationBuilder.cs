using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplicationBuilder
{
    internal WpfApplicationBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        Services = new ServiceCollection();
        Configuration = new ConfigurationManager();

        //we need the service provider for the DataContextFactory
        //maybe this isnt the best solution
        //however I dont want to have to inject many DataContextFactories<T>
        //for each type of DataContext. Instead just using IDataContextFactory is nice
        Services.AddSingleton(sp => sp);
        Services.AddSingleton(appxaml);
        Services.AddSingleton(appxaml.Dispatcher);
    }

    public IServiceCollection Services { get; }

    public ConfigurationManager Configuration { get; }

    public WpfApplication Build()
    {
        return new WpfApplication(Configuration, Services);
    }
}
