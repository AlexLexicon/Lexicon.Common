using System.Windows;

namespace Lexicon.Common.Wpf.AttachedProperties;
public static class Window
{
    /*
     * IsMaximized
     */
    public static readonly DependencyProperty IsMaximizedProperty = DependencyProperty.RegisterAttached("IsMaximized", typeof(bool), typeof(Window), new PropertyMetadata(false, OnIsMaximized_Window_PropertyChanged, OnIsMaximized_Window_CoerceValueCallback));
    public static bool GetIsMaximized(DependencyObject obj) => (bool)obj.GetValue(IsMaximizedProperty);
    public static void SetIsMaximized(DependencyObject obj, bool value) => obj.SetValue(IsMaximizedProperty, value);
    private static void OnIsMaximized_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool newIsMaximized)
        {
            window.SizeChanged -= OnIsMaximized_Window_SizeChanged;
            window.SizeChanged += OnIsMaximized_Window_SizeChanged;

            bool currentIsMaximized = window.WindowState == WindowState.Maximized;

            if (currentIsMaximized != newIsMaximized)
            {
                window.WindowState = newIsMaximized ? WindowState.Maximized : WindowState.Normal;
            }
        }
    }
    private static void OnIsMaximized_Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is System.Windows.Window window)
        {
            bool previousIsMaximized = GetIsMaximized(window);
            bool currentIsMaximized = window.WindowState == WindowState.Maximized;

            if (currentIsMaximized != previousIsMaximized)
            {
                SetIsMaximized(window, currentIsMaximized);
            }
        }
    }
    private static object? OnIsMaximized_Window_CoerceValueCallback(DependencyObject d, object baseValue)
    {
        if (d is System.Windows.Window window)
        {
            OnIsMaximized_Window_PropertyChanged(window, new DependencyPropertyChangedEventArgs(IsMaximizedProperty, window.WindowState == WindowState.Maximized, baseValue));
        }

        return baseValue;
    }

    /*
     * ToggleInvokeShow
     */
    public static readonly DependencyProperty ToggleInvokeShowProperty = DependencyProperty.RegisterAttached("ToggleShow", typeof(bool), typeof(Window), new PropertyMetadata(OnToggleInvokeShow_Window_PropertyChanged));
    public static bool GetToggleShow(DependencyObject obj) => (bool)obj.GetValue(ToggleInvokeShowProperty);
    public static void SetToggleShow(DependencyObject obj, bool value) => obj.SetValue(ToggleInvokeShowProperty, value);
    private static void OnToggleInvokeShow_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool isInvoked && isInvoked)
        {
            window.Show();
            SetToggleShow(window, false);
        }
    }

    /*
     * ToggleInvokeClose
     */
    public static readonly DependencyProperty ToggleInvokeCloseProperty = DependencyProperty.RegisterAttached("ToggleClose", typeof(bool), typeof(Window), new PropertyMetadata(OnToggleInvokeClose_Window_PropertyChanged));
    public static bool GetToggleClose(DependencyObject obj) => (bool)obj.GetValue(ToggleInvokeCloseProperty);
    public static void SetToggleClose(DependencyObject obj, bool value) => obj.SetValue(ToggleInvokeCloseProperty, value);
    private static void OnToggleInvokeClose_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool isInvoked && isInvoked)
        {
            window.Close();
            SetToggleClose(window, false);
        }
    }

    /*
     * ToggleInvokeHide
     */
    public static readonly DependencyProperty ToggleInvokeHideProperty = DependencyProperty.RegisterAttached("ToggleHide", typeof(bool), typeof(Window), new PropertyMetadata(OnToggleInvokeHide_Window_PropertyChanged));
    public static bool GetToggleHide(DependencyObject obj) => (bool)obj.GetValue(ToggleInvokeCloseProperty);
    public static void SetToggleHide(DependencyObject obj, bool value) => obj.SetValue(ToggleInvokeCloseProperty, value);
    private static void OnToggleInvokeHide_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool isInvoked && isInvoked)
        {
            window.Hide();
            SetToggleHide(window, false);
        }
    }

    /*
     * ToggleInvokeShowDialog
     */
    public static readonly DependencyProperty ToggleInvokeShowDialogProperty = DependencyProperty.RegisterAttached("ToggleShowDialog", typeof(bool), typeof(Window), new PropertyMetadata(OnToggleInvokeShowDialog_Window_ProeprtyChanged));
    public static bool GetToggleShowDialog(DependencyObject obj) => (bool)obj.GetValue(ToggleInvokeShowDialogProperty);
    public static void SetToggleShowDialog(DependencyObject obj, bool value) => obj.SetValue(ToggleInvokeShowDialogProperty, value);
    private static void OnToggleInvokeShowDialog_Window_ProeprtyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool isInvoked && isInvoked)
        {
            window.ShowDialog();
            SetToggleShowDialog(window, false);
        }
    }
}
