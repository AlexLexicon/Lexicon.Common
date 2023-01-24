using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Builders;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Extensions;
public static class ServiceCollectionExtensions
{
    public static AddDataContextBuilder<TDataContext> AddDataContext<TDataContext>(this IServiceCollection services) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(services);

        //we want to add this factory if any data contexts are added
        services.TryAddSingleton<IDataContextFactory, DataContextFactory>();

        //add the DataContext
        services.AddTransient<TDataContext>();

        //add the DataContextFactory
        services.AddTransient<IDataContextFactory, DataContextFactory>();

        return new AddDataContextBuilder<TDataContext>(services);
    }
}
