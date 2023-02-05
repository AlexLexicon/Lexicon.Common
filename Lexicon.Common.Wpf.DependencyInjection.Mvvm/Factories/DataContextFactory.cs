using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Accessors;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
public class DataContextFactory : IDataContextFactory
{
    internal static TDataContext CreateDataContext<TDataContext, TModel>(IServiceProvider serviceProvider, TModel? model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextAndElementAccessor = serviceProvider.GetService<IDataContextAndElementAccessor<TDataContext>>();

        TDataContext dataContext;
        if (dataContextAndElementAccessor is null)
        {
            //there will only be a DataContextAndElementAccessor
            //if the 'ForElement' method was called when 
            //adding the DataContext to the services
            if (model is null)
            {
                dataContext = serviceProvider.GetRequiredService<TDataContext>();
            }
            else
            {
                dataContext = (TDataContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(TDataContext), model);
            }
        }
        else
        {
            //in this case the Framework element
            //will have also been constructed
            if (model is null)
            {
                dataContext = dataContextAndElementAccessor.GetDataContext(model);
            }
            else
            {
                dataContext = dataContextAndElementAccessor.GetDataContext();
            }
        }

        if (dataContext is IDataContextCreate dataContextCreate)
        {
            dataContextCreate.Create();
        }

        return dataContext;
    }
    internal static (TDataContext dataContext, Window window) GetCreateAndShowDataContextAndElementAccessor<TDataContext, TModel>(IServiceProvider serviceProvider, TModel? model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextAndElementAccessor = serviceProvider.GetService<IDataContextAndElementAccessor<TDataContext>>();

        if (dataContextAndElementAccessor is null)
        {
            throw new DataContextDoesNotHaveAssociatedElementException(typeof(TDataContext));
        }

        if (dataContextAndElementAccessor.Element is not Window window)
        {
            throw new DataContextAssociatedElementCannotShowException(typeof(TDataContext));
        }

        TDataContext dataContext;
        if (model is null)
        {
            dataContext = dataContextAndElementAccessor.GetDataContext();
        }
        else
        {
            dataContext = dataContextAndElementAccessor.GetDataContext(model);
        }

        if (dataContext is IDataContextCreate dataContextCreate)
        {
            dataContextCreate.Create();
        }

        return (dataContext, window);
    }

    private readonly IServiceProvider _serviceProvider;

    public DataContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDataContext Create<TDataContext>() where TDataContext : class
    {
        return CreateDataContext<TDataContext, object>(_serviceProvider, null);
    }
    public TDataContext Create<TDataContext, TModel>(TModel model) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(model);

        return CreateDataContext<TDataContext, TModel>(_serviceProvider, model);
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
        var (dataContext, window) = GetCreateAndShowDataContextAndElementAccessor<TDataContext, TModel>(_serviceProvider, model);

        window.Show();

        return dataContext;
    }
}
