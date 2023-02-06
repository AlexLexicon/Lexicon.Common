using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
public class DataContextFactory : IDataContextFactory
{
    internal static TDataContext CreateAndHandleDataContext<TDataContext, TModel>(IServiceProvider serviceProvider, TModel? model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextForElementHandler = serviceProvider.GetService<IDataContextForElementHandler<TDataContext>>();

        TDataContext dataContext;
        if (model is null)
        {
            dataContext = serviceProvider.GetRequiredService<TDataContext>();
        }
        else
        {
            dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model);
        }

        if (dataContextForElementHandler is not null)
        {
            dataContextForElementHandler.Handle(dataContext);
        }

        if (dataContext is IDataContextCreate dataContextCreate)
        {
            dataContextCreate.Create();
        }

        return dataContext;
    }
    internal static (TDataContext dataContext, FrameworkElement frameworkElement) ShowAndHandleDataContext<TDataContext, TModel>(IServiceProvider serviceProvider, TModel? model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextForElementHandler = serviceProvider.GetService<IDataContextForElementHandler<TDataContext>>();

        if (dataContextForElementHandler is null)
        {
            throw new DataContextDoesNotHaveAssociatedElementException(typeof(TDataContext));
        }

        if (dataContextForElementHandler.FrameworkElement is null)
        {
            throw new DataContextAssociatedElementNullException(typeof(TDataContext));
        }

        TDataContext dataContext;
        if (model is null)
        {
            dataContext = serviceProvider.GetRequiredService<TDataContext>();
        }
        else
        {
            dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model);
        }

        dataContextForElementHandler.Handle(dataContext);

        if (dataContext is IDataContextCreate dataContextCreate)
        {
            dataContextCreate.Create();
        }

        return (dataContext, dataContextForElementHandler.FrameworkElement);
    }

    private readonly IServiceProvider _serviceProvider;

    public DataContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDataContext Create<TDataContext>() where TDataContext : class
    {
        return CreateAndHandleDataContext<TDataContext, object>(_serviceProvider, null);
    }
    public TDataContext Create<TDataContext, TModel>(TModel model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateAndHandleDataContext<TDataContext, TModel>(_serviceProvider, model);
    }

    public TDataContext CreateAndShow<TDataContext>() where TDataContext : class
    {
        return CreateConfigureAndShow<TDataContext, object>(null);
    }
    public TDataContext CreateAndShow<TDataContext, TModel>(TModel model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateConfigureAndShow<TDataContext, TModel>(model);
    }
    private TDataContext CreateConfigureAndShow<TDataContext, TModel>(TModel? model) where TDataContext : class
    {
        (TDataContext dataContext, FrameworkElement frameworkElement) = ShowAndHandleDataContext<TDataContext, TModel>(_serviceProvider, model);

        if (frameworkElement is not Window window)
        {
            throw new DataContextAssociatedElementCannotShowException(typeof(TDataContext));
        }

        window.Show();

        return dataContext;
    }
}
