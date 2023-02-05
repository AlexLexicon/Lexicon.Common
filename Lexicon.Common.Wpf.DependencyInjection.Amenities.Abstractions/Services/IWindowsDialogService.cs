using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Settings;

namespace Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Services;
public interface IWindowsDialogService
{
    string? SaveFile();
    string? SaveFile(string filePathAndName);
    string? SaveFile(SaveFileSettings settings);

    string? OpenFile();
    string? OpenFile(string initalDirectoryPath);
    string? OpenFile(OpenFileSettings settings);

    string? SelectDirectory();
    string? SelectDirectory(string initalDirectoryPath);
    string? SelectDirectory(SelectDirectorySettings settings);
}
