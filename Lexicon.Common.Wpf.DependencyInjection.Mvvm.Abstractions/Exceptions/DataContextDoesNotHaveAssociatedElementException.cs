namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class DataContextDoesNotHaveAssociatedElementException : Exception
{
    public DataContextDoesNotHaveAssociatedElementException(Type dataContextType) : base($"The DataContext '{dataContextType?.Name}' does not have an associated element. Make sure to call 'ForElement' after 'AddDataContext' during service registration.")
    {
    }
}
