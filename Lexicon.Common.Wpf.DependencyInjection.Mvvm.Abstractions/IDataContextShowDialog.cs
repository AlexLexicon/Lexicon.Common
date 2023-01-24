using System.Windows.Input;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
public interface IDataContextShowDialog
{
    ICommand ShowDialogCommand { set; }
}
