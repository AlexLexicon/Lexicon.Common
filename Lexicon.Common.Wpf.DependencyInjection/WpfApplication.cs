using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplication
{
    public static WpfApplicationBuilder CreateBuilder(Application app)
    {
        ArgumentNullException.ThrowIfNull(app);

        return new WpfApplicationBuilder(app);
    }

    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _provider;

    internal WpfApplication(IConfigurationBuilder configurationBuilder, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        ArgumentNullException.ThrowIfNull(services);

        _configuration = configurationBuilder.Build();

        services.TryAddSingleton(_configuration);

        _provider = services.BuildServiceProvider();
    }

    public IConfiguration Configuration => _configuration;

    public IServiceProvider Services => _provider;
}
