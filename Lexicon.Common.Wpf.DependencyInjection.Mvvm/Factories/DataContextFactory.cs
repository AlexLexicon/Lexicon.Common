using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
public class DataContextFactory : IDataContextFactory
{
    internal static TDataContext CreateAndHandleDataContext<TDataContext, TModel1, TModel2, TModel3>(IServiceProvider serviceProvider, TModel1? model1 = null, TModel2? model2 = null, TModel3? model3 = null, IDataContextForElementHandler<TDataContext>? dataContextForElementHandler = null) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        TDataContext dataContext;
        if (model1 is not null && model2 is not null && model3 is not null)
        {
            dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model1, model2, model3);
        }
        else if (model1 is not null && model2 is not null)
        {
            dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model1, model2);
        }
        else if (model1 is not null)
        {
            dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model1);
        }
        else
        {
            dataContext = serviceProvider.GetRequiredService<TDataContext>();
        }

        dataContextForElementHandler ??= serviceProvider.GetService<IDataContextForElementHandler<TDataContext>>();

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
    internal static (TDataContext dataContext, FrameworkElement frameworkElement) ShowAndHandleDataContext<TDataContext, TModel1, TModel2, TModel3>(IServiceProvider serviceProvider, TModel1? model1 = null, TModel2? model2 = null, TModel3? model3 = null) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class
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

        TDataContext dataContext = CreateAndHandleDataContext(serviceProvider, model1, model2, model3, dataContextForElementHandler);

        return (dataContext, dataContextForElementHandler.FrameworkElement);
    }

    private readonly IServiceProvider _serviceProvider;

    public DataContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDataContext Create<TDataContext>() where TDataContext : class
    {
        return CreateAndHandleDataContext<TDataContext, object, object, object>(_serviceProvider);
    }
    public TDataContext Create<TDataContext, TModel>(TModel model) where TDataContext : class where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateAndHandleDataContext<TDataContext, TModel, object, object>(_serviceProvider, model);
    }
    public TDataContext Create<TDataContext, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TDataContext : class where TModel1 : class where TModel2 : class
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return CreateAndHandleDataContext<TDataContext, TModel1, TModel2, object>(_serviceProvider, model1, model2);
    }
    public TDataContext Create<TDataContext, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return CreateAndHandleDataContext<TDataContext, TModel1, TModel2, TModel3>(_serviceProvider, model1, model2, model3);
    }

    public TDataContext CreateAndShow<TDataContext>() where TDataContext : class
    {
        return CreateConfigureAndShow<TDataContext, object, object, object>();
    }
    public TDataContext CreateAndShow<TDataContext, TModel>(TModel model) where TDataContext : class where TModel : class
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateConfigureAndShow<TDataContext, TModel, object, object>(model);
    }
    public TDataContext CreateAndShow<TDataContext, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TDataContext : class where TModel1 : class where TModel2 : class
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);

        return CreateConfigureAndShow<TDataContext, TModel1, TModel2, object>(model1, model2);
    }
    public TDataContext CreateAndShow<TDataContext, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class
    {
        ArgumentNullException.ThrowIfNull(model1);
        ArgumentNullException.ThrowIfNull(model2);
        ArgumentNullException.ThrowIfNull(model3);

        return CreateConfigureAndShow<TDataContext, TModel1, TModel2, TModel3>(model1, model2, model3);
    }
    private TDataContext CreateConfigureAndShow<TDataContext, TModel1, TModel2, TModel3>(TModel1? model1 = null, TModel2? model2 = null, TModel3? model3 = null) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class
    {
        (TDataContext dataContext, FrameworkElement frameworkElement) = ShowAndHandleDataContext<TDataContext, TModel1, TModel2, TModel3>(_serviceProvider, model1, model2, model3);

        if (frameworkElement is not Window window)
        {
            throw new DataContextAssociatedElementCannotShowException(typeof(TDataContext));
        }

        window.Show();

        return dataContext;
    }
}
