namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Exceptions;
public class FrameworkElementNullException : Exception
{
    public FrameworkElementNullException() : base("The Element is null")
    {
    }
}
