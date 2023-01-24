using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Accessors;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Factories;
public class DataContextFactory : IDataContextFactory
{
    internal static TDataContext GetCreateDataContext<TDataContext>(IServiceProvider serviceProvider) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextAndElementAccessor = serviceProvider.GetService<DataContextAndElementAccessor<TDataContext>>();

        if (dataContextAndElementAccessor is null)
        {
            //there will only be a DataContextAndElementAccessor
            //if the 'ForElement' method was called when 
            //adding the DataContext to the services
            return serviceProvider.GetRequiredService<TDataContext>();
        }

        //in this case the Framework element
        //will have also been constructed
        return dataContextAndElementAccessor.DataContext;
    }
    internal static (TDataContext dataContext, Window window) GetCreateAndShowDataContextAndElementAccessor<TDataContext>(IServiceProvider serviceProvider) where TDataContext : class
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dataContextAndElementAccessor = serviceProvider.GetRequiredService<DataContextAndElementAccessor<TDataContext>>();

        if (dataContextAndElementAccessor is null)
        {
            throw new DataContextDoesNotHaveAssociatedElementException(typeof(TDataContext));
        }

        if (dataContextAndElementAccessor.Element is not Window window)
        {
            throw new DataContextAssociatedElementCannotShowException(typeof(TDataContext));
        }

        return (dataContext: dataContextAndElementAccessor.DataContext, window);
    }

    private readonly IServiceProvider _serviceProvider;

    public DataContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TDataContext Create<TDataContext>() where TDataContext : class
    {
        return GetCreateDataContext<TDataContext>(_serviceProvider);
    }

    public TDataContext CreateAndShow<TDataContext>() where TDataContext : class
    {
        var (dataContext, window) = GetCreateAndShowDataContextAndElementAccessor<TDataContext>(_serviceProvider);

        window.Show();

        return dataContext;
    }
}
