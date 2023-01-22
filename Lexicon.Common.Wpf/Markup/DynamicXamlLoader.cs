using System.IO;
using System.Reflection;
using System.Windows.Markup;
using System.Windows;
using Lexicon.Common.Wpf.Exceptions;

namespace Lexicon.Common.Wpf.Markup;
//based on: https://stackoverflow.com/questions/910814/loading-xaml-at-runtime
public class DynamicXamlLoader : MarkupExtension
{
    protected readonly List<Assembly> _assemblies = new List<Assembly>();

    public DynamicXamlLoader()
    {
        IsSuppressingExceptions = true;

        Assembly? entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly is not null)
        {
            _assemblies.Add(entryAssembly);
        }

        Assembly? executingAssembly = Assembly.GetExecutingAssembly();
        if (executingAssembly is not null)
        {
            _assemblies.Add(executingAssembly);
        }
    }

    public string? XamlFileName { get; set; }
    public bool IsSuppressingExceptions { get; set; }

    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(XamlFileName);

        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget provideValue || provideValue.TargetObject is null || provideValue.TargetObject is not UIElement)
        {
            return null;
        }

        return LoadFile(XamlFileName);
    }

    protected virtual object? LoadFile(string xamlFileName)
    {
        foreach (Assembly assembly in _assemblies)
        {
            if (TryLoadFromAssembly(assembly, xamlFileName, out object? result))
            {
                return result;
            }
        }

        if (!IsSuppressingExceptions)
        {
            throw new XamlFileNotFoundException();
        }

        return null;
    }

    protected virtual bool TryLoadFromAssembly(Assembly assembly, string xamlFileName, out object? result)
    {
        result = null;

        string[] manifestResourceNames = assembly.GetManifestResourceNames();

        string fullXamlfileName = xamlFileName;

        if (fullXamlfileName.IndexOf('.') < 0)
        {
            fullXamlfileName = $"{xamlFileName}.xaml";
        }

        string? manifestResourceName = manifestResourceNames.FirstOrDefault(n => n.EndsWith($".{fullXamlfileName}"));

        if (manifestResourceName is null)
        {
            return false;
        }

        using Stream? xamlStream = assembly.GetManifestResourceStream(manifestResourceName);

        if (xamlStream is null)
        {
            return false;
        }

        result = XamlReader.Load(xamlStream);

        return true;
    }
}