namespace Lexicon.Common.Wpf.ValueConverters.Exceptions;
public class NotConvertableException : Exception
{
    private const string MESSAGE = "Cannot convert to specific value in value converter";
    public NotConvertableException() : base(MESSAGE)
    {
    }
    public NotConvertableException(Exception? innerException) : base(MESSAGE, innerException)
    {
    }
}
