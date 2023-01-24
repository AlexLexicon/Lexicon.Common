using Lexicon.Common.Wpf.Dialogs.Extensions;
using Lexicon.Common.Wpf.Dialogs.Options;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicon.Common.Wpf.Dialogs;
public static class WindowsDialogs
{
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <returns>The file path that the user chooses to save a file too, null is the dialog is cancelled.</returns>
    public static string? SaveFile() => SaveFile(filePathAndName: null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <returns>The file path that the user chooses to save a file too, null is the dialog is cancelled.</returns>
    public static string? SaveFile(string? filePathAndName)
    {
        string? fileName = null;
        string? directory = null;
        string? extension = null;

        //the following tries to seperate the file path and name
        //into the fileName, directory and extension variables
        if (filePathAndName is not null)
        {
            string[] pathSegments = filePathAndName.Split('\\');

            directory = string.Join('\\', pathSegments.SkipLast(1));

            fileName = pathSegments.LastOrDefault();

            extension = null;
            if (fileName is not null)
            {
                string[] extensions = fileName.Split('.');

                if (extensions.Length > 1)
                {
                    extension = extensions.LastOrDefault();
                }
            }
        }

        return SaveFile(optionsAction: options =>
        {
            options.FileName = fileName;
            options.InitialDirectory = directory;
            options.DefaultExtension = extension;
        });
    }
    /// <summary>
    /// Shows a new instance of a <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <param name="optionsAction">An action used to configure the created <see cref="CommonSaveFileDialog"/> with the most common options.</param>
    /// <returns>The file path that the user chooses to save a file too, null is the dialog is cancelled.</returns>
    public static string? SaveFile(Action<SaveFileOptions>? optionsAction)
    {
        var options = new SaveFileOptions();

        optionsAction?.Invoke(options);

        return InvokeShowDialogOnCommonSaveFileDialog(commonSaveFileDialog =>
        {
            if (commonSaveFileDialog.Title is not null)
            {
                commonSaveFileDialog.Title = options.Title;
            }

            if (commonSaveFileDialog.DefaultFileName is not null)
            {
                commonSaveFileDialog.DefaultFileName = options.FileName;
            }

            if (options.EnsureValidNames is not null)
            {
                commonSaveFileDialog.EnsureValidNames = options.EnsureValidNames.Value;
            }

            if (options.DefaultExtension is not null)
            {
                commonSaveFileDialog.DefaultExtension = options.DefaultExtension;
                commonSaveFileDialog.Filters.Add(new CommonFileDialogFilter(options.DefaultExtension, options.DefaultExtension));
            }

            if (options.InitialDirectory is not null)
            {
                commonSaveFileDialog.InitialDirectory = options.InitialDirectory;
            }
        });
    }

    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <returns>The full file path that the user chooses to open a file from, null is the dialog is cancelled.</returns>
    public static string? OpenFile() => OpenFile(optionsAction: null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <param name="optionsAction">An action used to configure the created <see cref="CommonOpenFileDialog"/> with the most common options.</param>
    /// <returns>The file path that the user chooses to open a file from, null is the dialog is cancelled.</returns>
    public static string? OpenFile(Action<OpenFileOptions>? optionsAction)
    {
        var options = new OpenFileOptions();

        optionsAction?.Invoke(options);

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            if (commonOpenFileDialog.Title is not null)
            {
                commonOpenFileDialog.Title = options.Title;
            }
            if (commonOpenFileDialog.DefaultFileName is not null)
            {
                commonOpenFileDialog.DefaultFileName = options.FileName;
            }
            if (options.EnsureFileExists is not null)
            {
                commonOpenFileDialog.EnsureFileExists = options.EnsureFileExists.Value;
            }
            if (options.EnsurePathExists is not null)
            {
                commonOpenFileDialog.EnsurePathExists = options.EnsurePathExists.Value;
            }
            if (options.DefaultExtension is not null)
            {
                commonOpenFileDialog.DefaultExtension = options.DefaultExtension;
                commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(options.DefaultExtension, options.DefaultExtension));
            }
        });
    }

    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/> set to folder picker.
    /// </summary>
    /// <returns>The path that the user chooses, null is the dialog is cancelled.</returns>
    public static string? SelectDirectory() => SelectDirectory(optionsAction: null);
    /// <summary>
    /// Shows a new instance of a <see cref="CommonOpenFileDialog"/> set to folder picker.
    /// </summary>
    /// <param name="optionsAction">An action used to configure the created <see cref="CommonOpenFileDialog"/> with the most common options.</param>
    /// <returns>The path that the user chooses, null is the dialog is cancelled.</returns>
    public static string? SelectDirectory(Action<SelectDirectoryOptions>? optionsAction)
    {
        var options = new SelectDirectoryOptions();

        optionsAction?.Invoke(options);

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = options.Title;
            commonOpenFileDialog.InitialDirectory = options.InitialDirectory;
        });
    }

    /// <summary>
    /// Invokes the ShowDialog() method on a new instance of an <see cref="CommonSaveFileDialog"/>.
    /// </summary>
    /// <param name="setupAction">An action used to configure the created <see cref="CommonSaveFileDialog"/>.</param>
    /// <returns>The file path that the user chooses to save a file too, null is the dialog is cancelled.</returns>
    public static string? InvokeShowDialogOnCommonSaveFileDialog(Action<CommonSaveFileDialog> setupAction)
    {
        ArgumentNullException.ThrowIfNull(setupAction);

        var commonSaveFileDialog = new CommonSaveFileDialog();

        setupAction.Invoke(commonSaveFileDialog);

        return commonSaveFileDialog.GetFilePathFromShowDialog();
    }

    /// <summary>
    /// Invokes the ShowDialog() method on a new instance of an <see cref="CommonOpenFileDialog"/>.
    /// </summary>
    /// <param name="setupAction">An action used to configure the created <see cref="CommonOpenFileDialog"/>.</param>
    /// <returns>The path that the user chooses, null is the dialog is cancelled.</returns>
    public static string? InvokeShowDialogOnCommonOpenFileDialog(Action<CommonOpenFileDialog> setupAction)
    {
        ArgumentNullException.ThrowIfNull(setupAction);

        var commonOpenFileDialog = new CommonOpenFileDialog();

        setupAction.Invoke(commonOpenFileDialog);

        return commonOpenFileDialog.GetFilePathFromShowDialog();
    }
}
