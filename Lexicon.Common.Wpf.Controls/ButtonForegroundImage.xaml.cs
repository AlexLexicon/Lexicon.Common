using System.Windows;
using System.Windows.Controls;

namespace Lexicon.Common.Wpf.Controls;
public partial class ButtonForegroundImage : Label
{
    public ButtonForegroundImage() => InitializeComponent();

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(string), typeof(ButtonForegroundImage));
    public string? ImageSource
    {
        get => (string?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}
