using Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;

namespace Lexicon.Common.Wpf.DependencyInjection.Extensions;
public static class WpfApplicationExtensions
{
    public static void Run<TWindow>(this WpfApplication app) where TWindow : Window
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Run(new RunWindow<TWindow>
        {
            App = app,
        });
    }

    private class RunWindow<TWindow> : IWpfApplicationRun where TWindow : Window
    {
        public required WpfApplication App { get; init; }

        public async Task StartupAsync()
        {
            var dispatcher = App.Services.GetRequiredService<Dispatcher>();
            var window = App.Services.GetRequiredService<TWindow>();

            await dispatcher.BeginInvoke(window.Show, DispatcherPriority.ApplicationIdle);
        }
    }
}
