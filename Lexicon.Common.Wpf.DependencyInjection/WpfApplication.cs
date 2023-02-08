using Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection;
public sealed class WpfApplication
{
    public static WpfApplicationBuilder CreateBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        return new WpfApplicationBuilder(appxaml);
    }

    internal WpfApplication(IConfigurationBuilder configurationBuilder, IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(configurationBuilder);
        ArgumentNullException.ThrowIfNull(services);

        Configuration = configurationBuilder.Build();

        services.TryAddSingleton(Configuration);

        Services = services.BuildServiceProvider();

        var appxaml = Services.GetRequiredService<Application>();

        appxaml.Startup += Startup;
    }

    public IConfiguration Configuration { get; }

    public IServiceProvider Services { get; }

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
