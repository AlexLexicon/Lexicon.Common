using CommunityToolkit.Mvvm.Input;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Accessors;
public interface IDataContextAndElementAccessor<TDataContext> where TDataContext : class
{
    FrameworkElement? Element { get; }
    TDataContext GetDataContext();
    TDataContext GetDataContext<TModel>(TModel model);
    void AssignDataContext(FrameworkElement frameworkElement);
}
public class DataContextAndElementAccessor<TDataContext> : IDataContextAndElementAccessor<TDataContext> where TDataContext : class
{
    private readonly IServiceProvider _serviceProvider;

    public DataContextAndElementAccessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public FrameworkElement? Element { get; protected set; }

    public virtual TDataContext GetDataContext() => _serviceProvider.GetRequiredService<TDataContext>();
    public virtual TDataContext GetDataContext<TModel>(TModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        return (TDataContext)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(TDataContext), model);
    }

    public virtual void AssignDataContext(FrameworkElement frameworkElement)
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        Element = frameworkElement;

        TDataContext dataContext = GetDataContext();

        Element.DataContext = dataContext;

        if (dataContext is IDataContextClose dcClose)
        {
            //todo make the following extenable?
            if (Element is not Window window)
            {
                throw new ElementCannotCloseException();
            }

            dcClose.CloseCommand = new RelayCommand(window.Close);
        }

        if (dataContext is IDataContextHide dcHide)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotHideException();
            }

            dcHide.HideCommand = new RelayCommand(window.Hide);
        }

        if (dataContext is IDataContextShow dcShow)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotShowException();
            }

            dcShow.ShowCommand = new RelayCommand(window.Show);
        }

        if (dataContext is IDataContextShowDialog dcShowDialog)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotShowDialogException();
            }

            dcShowDialog.ShowDialogCommand = new RelayCommand(() => window.ShowDialog());
        }

        return;
    }
}
