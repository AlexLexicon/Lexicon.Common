namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class ElementCannotShowException : Exception
{
    public ElementCannotShowException() : base("The IDataContextAndElementAccessor.Element cannot show.")
    {
    }
}
