namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class ElementCannotShowDialogException : Exception
{
    public ElementCannotShowDialogException() : base("The IDataContextAndElementAccessor.Element cannot show dialog.")
    {
    }
}
