using Lexicon.Common.Wpf.Dialogs.Extensions;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicon.Common.Wpf.Dialogs;
public static class WindowsDialogs
{
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <returns>The file path that the user chooses to save a file too, null when the dialog is cancelled.</returns>
    public static string? SaveFile() => CreateSaveFile(null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <param name="filePathAndName">The full file path of the file to save, including the name and optionally the extension.</param>
    /// <returns>The file path that the user chooses to save a file too, null when the dialog is cancelled.</returns>
    public static string? SaveFile(string filePathAndName)
    {
        ArgumentNullException.ThrowIfNull(filePathAndName);

        var settings = new SaveFileSettings();

        //the following tries to seperate the file path and name
        //into the fileName, directory and extension variables
        string[] pathSegments = filePathAndName.Split('\\');

        settings.InitialDirectory = string.Join('\\', pathSegments.SkipLast(1));

        string? fileName = pathSegments.LastOrDefault();

        if (fileName is not null)
        {
            settings.DefaultExtension = fileName.Split('.').LastOrDefault();
        }

        settings.FileName = fileName;
        

        return CreateSaveFile(settings);
    }
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <param name="settings">Settings used to create the <see cref="CommonSaveFileDialog"/>.</param>
    /// <returns>The file path that the user chooses to save a file too, null when the dialog is cancelled.</returns>
    public static string? SaveFile(SaveFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return CreateSaveFile(settings);
    }

    private static string? CreateSaveFile(SaveFileSettings? settings)
    {
        settings ??= new SaveFileSettings();

        return InvokeShowDialogOnCommonSaveFileDialog(commonSaveFileDialog =>
        {
            if (commonSaveFileDialog.Title is not null)
            {
                commonSaveFileDialog.Title = settings.Title;
            }

            if (commonSaveFileDialog.DefaultFileName is not null)
            {
                commonSaveFileDialog.DefaultFileName = settings.FileName;
            }

            if (settings.InitialDirectory is not null)
            {
                commonSaveFileDialog.InitialDirectory = settings.InitialDirectory;
            }

            if (settings.DefaultExtension is not null)
            {
                commonSaveFileDialog.DefaultExtension = settings.DefaultExtension;
                commonSaveFileDialog.Filters.Add(new CommonFileDialogFilter(settings.DefaultExtension, settings.DefaultExtension));
            }

            if (settings.EnsureValidNames is not null)
            {
                commonSaveFileDialog.EnsureValidNames = settings.EnsureValidNames.Value;
            }
        });
    }

    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <returns>The full file path that the user chooses to open a file from, null is the dialog is cancelled.</returns>
    public static string? OpenFile() => CreateOpenFile(null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <param name="initalDirectoryPath">The full path to the directory to show initially.</param>
    /// <returns>The full file path that the user chooses to open a file from, null is the dialog is cancelled.</returns>
    public static string? OpenFile(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return CreateOpenFile(new OpenFileSettings
        {
            InitialDirectory = initalDirectoryPath,
        });
    }
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <param name="settings">Settings used to create the <see cref="CommonOpenFileDialog"/>.</param>
    /// <returns>The file path that the user chooses to open a file from, null when the dialog is cancelled.</returns>
    public static string? OpenFile(OpenFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return CreateOpenFile(settings);
    }

    private static string? CreateOpenFile(OpenFileSettings? settings)
    {
        settings ??= new OpenFileSettings();

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            if (commonOpenFileDialog.Title is not null)
            {
                commonOpenFileDialog.Title = settings.Title;
            }

            if (commonOpenFileDialog.DefaultFileName is not null)
            {
                commonOpenFileDialog.DefaultFileName = settings.FileName;
            }

            if (settings.InitialDirectory is not null)
            {
                commonOpenFileDialog.InitialDirectory = settings.InitialDirectory;
            }

            if (settings.DefaultExtension is not null)
            {
                commonOpenFileDialog.DefaultExtension = settings.DefaultExtension;
                commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(settings.DefaultExtension, settings.DefaultExtension));
            }

            if (settings.EnsureFileExists is not null)
            {
                commonOpenFileDialog.EnsureFileExists = settings.EnsureFileExists.Value;
            }

            if (settings.EnsurePathExists is not null)
            {
                commonOpenFileDialog.EnsurePathExists = settings.EnsurePathExists.Value;
            }
        });
    }

    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/> set to folder picker.
    /// </summary>
    /// <returns>The path that the user chooses, null is the dialog is cancelled.</returns>
    public static string? SelectDirectory() => CreateSelectDirectory(null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/> set to folder picker.
    /// </summary>
    /// <param name="initalDirectoryPath">The full path to the directory to show initially.</param>
    /// <returnsThe path that the user chooses, null when the dialog is cancelled.></returns>
    public static string? SelectDirectory(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return CreateSelectDirectory(new SelectDirectorySettings
        {
            InitialDirectory = initalDirectoryPath,
        });
    }
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/> set to folder picker.
    /// </summary>
    /// <param name="configure">Settings used to create the <see cref="CommonOpenFileDialog"/>.</param>
    /// <returns>The path that the user chooses, null when the dialog is cancelled.</returns>
    public static string? SelectDirectory(SelectDirectorySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return CreateSelectDirectory(settings);
    }

    private static string? CreateSelectDirectory(SelectDirectorySettings? settings)
    {
        settings ??= new SelectDirectorySettings();

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = settings.Title;
            commonOpenFileDialog.InitialDirectory = settings.InitialDirectory;
        });
    }

    /// <summary>
    /// Invokes the ShowDialog() method on a new instance of an <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <param name="configure">An action used to configure the created <see cref="CommonSaveFileDialog"/>.</param>
    /// <returns>The file path that the user chooses to save a file too, null is the dialog is cancelled.</returns>
    public static string? InvokeShowDialogOnCommonSaveFileDialog(Action<CommonSaveFileDialog> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var commonSaveFileDialog = new CommonSaveFileDialog();

        configure.Invoke(commonSaveFileDialog);

        return commonSaveFileDialog.GetFilePathFromShowDialog();
    }

    /// <summary>
    /// Invokes the ShowDialog() method on a new instance of an <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <param name="configure">An action used to configure the created <see cref="CommonOpenFileDialog"/>.</param>
    /// <returns>The path that the user chooses, null is the dialog is cancelled.</returns>
    public static string? InvokeShowDialogOnCommonOpenFileDialog(Action<CommonOpenFileDialog> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var commonOpenFileDialog = new CommonOpenFileDialog();

        configure.Invoke(commonOpenFileDialog);

        return commonOpenFileDialog.GetFilePathFromShowDialog();
    }
}
