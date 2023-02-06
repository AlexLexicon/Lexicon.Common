namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class DataContextAssociatedElementNullException : Exception
{
    public DataContextAssociatedElementNullException(Type dataContextType) : base($"The DataContext '{dataContextType?.Name}' associated element is null.")
    {
    }
}