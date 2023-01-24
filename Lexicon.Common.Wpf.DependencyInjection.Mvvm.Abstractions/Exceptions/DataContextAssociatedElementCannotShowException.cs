namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class DataContextAssociatedElementCannotShowException : Exception
{
    public DataContextAssociatedElementCannotShowException(Type dataContextType) : base($"The DataContext '{dataContextType?.Name}' associated element is not a type that can be shown.")
    {
    }
}
