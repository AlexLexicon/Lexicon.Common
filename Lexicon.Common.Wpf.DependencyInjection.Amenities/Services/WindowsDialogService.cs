using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions;
using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Services;

namespace Lexicon.Common.Wpf.DependencyInjection.Amenities.Services;
public class WindowsDialogService : IWindowsDialogService
{
    public string? SaveFile() => Dialogs.WindowsDialogs.SaveFile();
    public string? SaveFile(string filePathAndName)
    {
        ArgumentNullException.ThrowIfNull(filePathAndName);

        return Dialogs.WindowsDialogs.SaveFile(filePathAndName);
    }
    public string? SaveFile(SaveFileSettings settings)
    {
        return Dialogs.WindowsDialogs.SaveFile(new Dialogs.SaveFileSettings
        {
            Title = settings.Title,
            FileName = settings.FileName,
            InitialDirectory = settings.InitialDirectory,
            EnsureValidNames = settings.EnsureValidNames,
            DefaultExtension = settings.DefaultExtension,
        });
    }

    public string? OpenFile() => Dialogs.WindowsDialogs.OpenFile();
    public string? OpenFile(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return Dialogs.WindowsDialogs.OpenFile(initalDirectoryPath);
    }
    public string? OpenFile(OpenFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return Dialogs.WindowsDialogs.OpenFile(new Dialogs.OpenFileSettings
        {
            Title = settings.Title,
            FileName = settings.FileName,
            InitialDirectory = settings.InitialDirectory,
            DefaultExtension = settings.DefaultExtension,
            EnsureFileExists = settings.EnsureFileExists,
            EnsurePathExists = settings.EnsurePathExists,
        });
    }

    public string? SelectDirectory() => Dialogs.WindowsDialogs.SelectDirectory();
    public string? SelectDirectory(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return Dialogs.WindowsDialogs.SelectDirectory(initalDirectoryPath);
    }
    public string? SelectDirectory(SelectDirectorySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return Dialogs.WindowsDialogs.SelectDirectory(new Dialogs.SelectDirectorySettings
        {
            Title = settings.Title,
            InitialDirectory = settings.InitialDirectory,
        });
    }
}
