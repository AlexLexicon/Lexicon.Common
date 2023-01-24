using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Accessors;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Builders;
public class AddDataContextBuilder<TDataContext> where TDataContext : class
{
    private readonly IServiceCollection _services;

    internal AddDataContextBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public void ForElement<TElement>() where TElement : FrameworkElement
    {
        //add the FrameworkElement
        _services.AddTransient<TElement>();

        //add a factory to create 'DataContextAndElementAccessor'
        _services.AddTransient(sp =>
        {
            var dataContextAndElementAccessor = (DataContextAndElementAccessor<TDataContext>)ActivatorUtilities.CreateInstance(sp, typeof(DataContextAndElementAccessor<TDataContext>));

            var element = sp.GetRequiredService<TElement>();

            dataContextAndElementAccessor.AssignDataContext(element);

            return dataContextAndElementAccessor;
        });
    }
}
