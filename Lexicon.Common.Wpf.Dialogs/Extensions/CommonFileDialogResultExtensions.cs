using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicon.Common.Wpf.Dialogs.Extensions;
public static class CommonFileDialogResultExtensions
{
    public static string? GetFilePathFromShowDialog(this CommonFileDialog commonFileDialog)
    {
        ArgumentNullException.ThrowIfNull(commonFileDialog);

        bool? result = commonFileDialog.ShowDialog() switch
        {
            CommonFileDialogResult.Ok => true,
            CommonFileDialogResult.Cancel => false,
            _ => null,
        };

        if (result is not null && result.Value)
        {
            return commonFileDialog.FileName;
        }

        return null;
    }
}
