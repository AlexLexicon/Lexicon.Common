using System.Windows.Input;

namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions;
public interface IDataContextShow
{
    ICommand? ShowCommand { set; }
}
