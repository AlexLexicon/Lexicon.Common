using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Services;
using Lexicon.Common.Wpf.Dialogs;

namespace Lexicon.Common.Wpf.DependencyInjection.Amenities.Services;
public class WindowsDialogService : IWindowsDialogService
{
    public string? SaveFile() => WindowsDialogs.SaveFile();
    public string? SaveFile(string filePathAndName)
    {
        ArgumentNullException.ThrowIfNull(filePathAndName);

        return WindowsDialogs.SaveFile(filePathAndName);
    }
    public string? SaveFile(SaveFileSettings settings)
    {
        return WindowsDialogs.SaveFile(new Dialogs.Settings.SaveFileSettings
        {
            Title = settings.Title,
            FileName = settings.FileName,
            InitialDirectory = settings.InitialDirectory,
            EnsureValidNames = settings.EnsureValidNames,
            DefaultExtension = settings.DefaultExtension,
        });
    }

    public string? OpenFile() => WindowsDialogs.OpenFile();
    public string? OpenFile(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return WindowsDialogs.OpenFile(initalDirectoryPath);
    }
    public string? OpenFile(OpenFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return WindowsDialogs.OpenFile(new Dialogs.Settings.OpenFileSettings
        {
            Title = settings.Title,
            FileName = settings.FileName,
            InitialDirectory = settings.InitialDirectory,
            DefaultExtension = settings.DefaultExtension,
            EnsureFileExists = settings.EnsureFileExists,
            EnsurePathExists = settings.EnsurePathExists,
        });
    }

    public string? SelectDirectory() => WindowsDialogs.SelectDirectory();
    public string? SelectDirectory(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return WindowsDialogs.SelectDirectory(initalDirectoryPath);
    }
    public string? SelectDirectory(SelectDirectorySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return WindowsDialogs.SelectDirectory(new Dialogs.Settings.SelectDirectorySettings
        {
            Title = settings.Title,
            InitialDirectory = settings.InitialDirectory,
        });
    }
}
