using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplicationBuilder
{
    private readonly IServiceCollection _services;
    private readonly ConfigurationManager _configurationManager;

    internal WpfApplicationBuilder(Application app)
    {
        ArgumentNullException.ThrowIfNull(app);

        _services = new ServiceCollection();
        _configurationManager = new ConfigurationManager();

        _services.AddSingleton(app);
        _services.AddSingleton(app.Dispatcher);
    }

    public IServiceCollection Services => _services;

    public ConfigurationManager Configuration => _configurationManager;

    public WpfApplication Build()
    {
        return new WpfApplication(_configurationManager, _services);
    }
}
