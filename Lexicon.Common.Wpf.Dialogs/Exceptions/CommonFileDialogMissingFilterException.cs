namespace Lexicon.Common.Wpf.Dialogs.Exceptions;
public class CommonFileDialogMissingFilterException : Exception
{
    public CommonFileDialogMissingFilterException(Exception innerException) : base("The CommonFileDialog requires a file extension filter. Consider using DefaultExtension or setting a filter.", innerException)
    {
    }
}
