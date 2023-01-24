namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class ElementCannotHideException : Exception
{
    public ElementCannotHideException() : base("The IDataContextAndElementAccessor.Element cannot hide.")
    {
    }
}
