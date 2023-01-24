namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class ElementCannotCloseException : Exception
{
    public ElementCannotCloseException() : base("The IDataContextAndElementAccessor.Element cannot close.")
    {
    }
}
