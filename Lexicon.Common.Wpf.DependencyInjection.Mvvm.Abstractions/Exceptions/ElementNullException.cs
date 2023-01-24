namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class ElementNullException : Exception
{
    public ElementNullException() : base($"The IDataContextAndElementAccessor.Element was null.")
    {
    }
}
