using Lexicon.Common.Enums.Extensions;

namespace Lexicon.Common.Enums;
public static class Enumx
{
    public static string? GetDescription(Enum value)
    {
        return value.GetDescription();
    }

    public static IEnumerable<T> GetValues<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>();
    }
}
