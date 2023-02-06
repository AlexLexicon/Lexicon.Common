using CommunityToolkit.Mvvm.Input;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Handlers;
public interface IDataContextForElementHandler<TDataContext> where TDataContext : class
{
    FrameworkElement? FrameworkElement { get; set; }
    void Handle(TDataContext dataContext);
}
public class DataContextForElementHandler<TDataContext> : IDataContextForElementHandler<TDataContext> where TDataContext : class
{
    public FrameworkElement? FrameworkElement { get; set; }

    public virtual void Handle(TDataContext dataContext)
    {
        ArgumentNullException.ThrowIfNull(dataContext);

        if (FrameworkElement is null)
        {
            throw new FrameworkElementNullException();
        }

        FrameworkElement.DataContext = dataContext;

        if (dataContext is IDataContextClose dcClose)
        {
            //todo make the following extenable?
            if (FrameworkElement is not Window window)
            {
                throw new ElementCannotCloseException();
            }

            dcClose.CloseCommand = new RelayCommand(window.Close);
        }

        if (dataContext is IDataContextHide dcHide)
        {
            if (FrameworkElement is not Window window)
            {
                throw new ElementCannotHideException();
            }

            dcHide.HideCommand = new RelayCommand(window.Hide);
        }

        if (dataContext is IDataContextShow dcShow)
        {
            if (FrameworkElement is not Window window)
            {
                throw new ElementCannotShowException();
            }

            dcShow.ShowCommand = new RelayCommand(window.Show);
        }

        if (dataContext is IDataContextShowDialog dcShowDialog)
        {
            if (FrameworkElement is not Window window)
            {
                throw new ElementCannotShowDialogException();
            }

            dcShowDialog.ShowDialogCommand = new RelayCommand(() => window.ShowDialog());
        }

        return;
    }
}
