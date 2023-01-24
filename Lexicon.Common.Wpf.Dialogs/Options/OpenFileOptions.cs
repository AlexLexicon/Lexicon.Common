namespace Lexicon.Common.Wpf.Dialogs.Options;
public class OpenFileOptions
{
    public string? Title { get; set; } = "Open File";
    public string? FileName { get; set; }
    public string? InitialDirectory { get; set; }
    public string? DefaultExtension { get; set; }
    public bool? EnsureFileExists { get; set; } = true;
    public bool? EnsurePathExists { get; set; } = true;
}
