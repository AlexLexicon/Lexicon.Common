using Lexicon.Common.Wpf.ValueConverters.Usage;

namespace Lexicon.Common.Wpf.ValueConverters;
public sealed class ToStringConverter : ValueConverterBase<string>
{
    private static readonly ValueConverterParameterDefinition<ToStrings> CASE_PARAMETER = new ValueConverterParameterDefinition<ToStrings>("case", new ResultForPatternMatchCollection<ToStrings>
    {
        new ResultForPatternMatch<ToStrings>(ToStrings.Lower, new [] { "lower", "l", "false", "f", "0" }),
        new ResultForPatternMatch<ToStrings>(ToStrings.Upper, new [] { "upper", "u", "true", "t", "1" })
    });

    public ToStrings? DefaultCase { get; set; }

    protected override string? TConvert(object? value, ValueConverterArgs args)
    {
        string? result = value?.ToString();

        ToStrings toString = ToStrings.None;
        if (HasParameter(CASE_PARAMETER, out ToStrings toStringParameter))
        {
            toString = toStringParameter;
        }
        else if (DefaultCase is not null)
        {
            toString = DefaultCase.Value;
        }

        if (toString == ToStrings.Lower)
        {
            return result?.ToLower();
        }
        else if (toString == ToStrings.Upper)
        {
            return result?.ToUpper();
        }

        return result;
    }

    protected override object? TConvertBack(string? value, ValueConverterArgs args)
    {
        if (args.TargetType == typeof(string))
        {
            return value;
        }

        if (args.TargetType == typeof(bool))
        {
            return value is not null;
        }

        if (value is not null)
        {
            if (args.TargetType == typeof(short) && short.TryParse(value, out short shortValue))
            {
                return shortValue;
            }
            if (args.TargetType == typeof(int) && int.TryParse(value, out int intValue))
            {
                return intValue;
            }
            if (args.TargetType == typeof(long) && long.TryParse(value, out long longValue))
            {
                return longValue;
            }
            if (args.TargetType == typeof(float) && float.TryParse(value, out float floatValue))
            {
                return floatValue;
            }
            if (args.TargetType == typeof(double) && double.TryParse(value, out double doubleValue))
            {
                return doubleValue;
            }
            if (args.TargetType == typeof(decimal) && decimal.TryParse(value, out decimal decimalValue))
            {
                return decimalValue;
            }
        }

        return null;
    }
}
