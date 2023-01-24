using Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Windows;
using System.Windows.Threading;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplication
{
    public static WpfApplicationBuilder CreateBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        return new WpfApplicationBuilder(appxaml);
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

        var appxaml = _provider.GetRequiredService<Application>();

        appxaml.Startup += Startup;
    }

    public IConfiguration Configuration => _configuration;

    public IServiceProvider Services => _provider;

    private IWpfApplicationRun? ApplicationRun { get; set; }

    public void Run(IWpfApplicationRun run)
    {
        ArgumentNullException.ThrowIfNull(run);

        ApplicationRun = run;
    }

    private async void Startup(object sender, StartupEventArgs e)
    {
        if (ApplicationRun is not null)
        {
            await ApplicationRun.StartupAsync();
        }
    }
}
