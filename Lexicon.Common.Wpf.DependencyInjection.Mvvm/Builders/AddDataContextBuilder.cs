using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Handlers;
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

        //add a factory to create 'DataContextForElementHandler'
        _services.AddTransient(sp =>
        {
            var dataContextForElementHandler = (DataContextForElementHandler<TDataContext>)ActivatorUtilities.CreateInstance(sp, typeof(DataContextForElementHandler<TDataContext>));

            var frameworkElement = sp.GetRequiredService<TElement>();

            dataContextForElementHandler.FrameworkElement = frameworkElement;

            return dataContextForElementHandler;
        });
        _services.AddTransient(typeof(IDataContextForElementHandler<>), typeof(DataContextForElementHandler<>));
    }
}
