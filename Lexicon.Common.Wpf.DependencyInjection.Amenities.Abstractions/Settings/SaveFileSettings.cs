﻿namespace Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Settings;
public class SaveFileSettings
{
    public string? Title { get; set; } = "Save File";
    public string? FileName { get; set; }
    public string? InitialDirectory { get; set; }
    public string? DefaultExtension { get; set; }
    public bool? EnsureValidNames { get; set; } = true;
}
