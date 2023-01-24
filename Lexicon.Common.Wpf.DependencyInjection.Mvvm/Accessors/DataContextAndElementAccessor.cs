﻿using CommunityToolkit.Mvvm.Input;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
using System.Windows;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Accessors;
//i didnt add an interface to this accessor
//since its so specific to wpf because of the 'FrameworkElement'
public class DataContextAndElementAccessor<TDataContext> where TDataContext : class
{
    public DataContextAndElementAccessor(TDataContext dataContext)
    {
        DataContext = dataContext;
    }

    public TDataContext DataContext { get; }
    public FrameworkElement? Element { get; private set; }

    public void AssignDataContext(FrameworkElement frameworkElement)
    {
        ArgumentNullException.ThrowIfNull(frameworkElement);

        Element = frameworkElement;

        Element.DataContext = DataContext;

        if (DataContext is IDataContextClose dcClose)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotCloseException();
            }

            dcClose.CloseCommand = new RelayCommand(window.Close);
        }

        if (DataContext is IDataContextHide dcHide)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotHideException();
            }

            dcHide.HideCommand = new RelayCommand(window.Hide);
        }

        if (DataContext is IDataContextShow dcShow)
        {
            if (Element is not Window window)
            {
                throw new ElementCannotShowException();
            }

            dcShow.ShowCommand = new RelayCommand(window.Show);
        }

        if (DataContext is IDataContextShowDialog dcShowDialog)
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