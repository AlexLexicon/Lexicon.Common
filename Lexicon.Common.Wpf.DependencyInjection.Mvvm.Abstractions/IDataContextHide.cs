using System.Windows.Input;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
public interface IDataContextHide
{
    ICommand? HideCommand { set; }
}
