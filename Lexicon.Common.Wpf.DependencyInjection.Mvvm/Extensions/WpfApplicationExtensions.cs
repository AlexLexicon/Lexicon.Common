using Lexicon.Common.Wpf.DependencyInjection.Abstractions.Services;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Extensions;
public static class WpfApplicationExtensions
{
    public static void Create<TDataContext>(this WpfApplication app) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Run(new CreateRun<TDataContext>
        {
            App = app
        });
    }
    public static void CreateAndShow<TDataContext>(this WpfApplication app) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Run(new CreateRunAndShow<TDataContext>
        {
            App = app
        });
    }

    private class CreateRun<TDataContext> : IWpfApplicationRun where TDataContext : class
    {
        public required WpfApplication App { get; init; }

        public Task StartupAsync()
        {
            DataContextFactory.CreateAndHandleDataContext<TDataContext, object>(App.Services, null);

            return Task.CompletedTask;
        }
    }
    private class CreateRunAndShow<TDataContext> : IWpfApplicationRun where TDataContext : class
    {
        public required WpfApplication App { get; init; }

        public async Task StartupAsync()
        {
            var dispatcher = App.Services.GetRequiredService<Dispatcher>();
            (_, FrameworkElement frameworkElement) = DataContextFactory.ShowAndHandleDataContext<TDataContext, object>(App.Services, null);

            if (frameworkElement is not Window window)
            {
                throw new DataContextAssociatedElementCannotShowException(typeof(TDataContext));
            }

            await dispatcher.BeginInvoke(window.Show, DispatcherPriority.ApplicationIdle);
        }
    }
}
