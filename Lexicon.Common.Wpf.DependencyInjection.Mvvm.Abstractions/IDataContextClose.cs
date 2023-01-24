using System.Windows.Input;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
public interface IDataContextClose
{
    ICommand? CloseCommand { set; }
}
