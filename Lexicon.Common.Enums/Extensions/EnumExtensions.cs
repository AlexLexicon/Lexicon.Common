using System.ComponentModel;
using System.Reflection;

namespace Lexicon.Common.Enums.Extensions;
public static class EnumExtensions
{
    public static string? GetDescription(this Enum value)
    {
        FieldInfo? field = value.GetType().GetField(value.ToString());

        if (field is null)
        {
            return null;
        }

        DescriptionAttribute[]? attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        DescriptionAttribute? descriptionAttribute = attributes?.FirstOrDefault();

        if (descriptionAttribute is not null)
        {
            return descriptionAttribute.Description;
        }

        return value.ToString();
    }
}
