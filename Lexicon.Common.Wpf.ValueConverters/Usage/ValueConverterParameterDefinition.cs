using System.Runtime.CompilerServices;

namespace Lexicon.Common.Wpf.ValueConverters.Usage;
public class ValueConverterParameterDefinition<T> : ValueConverterParameterDefinition
{
    private readonly Func<string[], T?>? _parseDelegate;

    public ValueConverterParameterDefinition(string pattern) : this(pattern, parseDelegate: null, null)
    {
    }
    public ValueConverterParameterDefinition(string pattern, ValueConverterParameterOptions? options) : this(pattern, parseDelegate: null, options)
    {
    }
    public ValueConverterParameterDefinition(string pattern, Func<string[], T?>? parseDelegate) : this(pattern, parseDelegate, null)
    {
    }
    public ValueConverterParameterDefinition(string pattern, Func<string[], T?>? parseDelegate, ValueConverterParameterOptions? options) : base(pattern, options)
    {
        _parseDelegate = parseDelegate;
        ResultForPatternMatches = null;
    }
    public ValueConverterParameterDefinition(string pattern, ResultForPatternMatchCollection<T> resultForPatternMatchCollection) : this(pattern, resultForPatternMatchCollection, null)
    {
    }
    public ValueConverterParameterDefinition(string pattern, ResultForPatternMatchCollection<T> resultForPatternMatchCollection, ValueConverterParameterOptions? options) : base(pattern, options)
    {
        _parseDelegate = null;
        ResultForPatternMatches = resultForPatternMatchCollection;
    }

    protected ResultForPatternMatchCollection<T>? ResultForPatternMatches { get; }

    public override bool Match(IReadOnlyList<ValueConverterParameter> parameters) => Match(parameters, out T _);
    public virtual bool Match(IReadOnlyList<ValueConverterParameter> parameters, out T? value)
    {
        var comparison = Options?.Comparer ?? StringComparison.OrdinalIgnoreCase;

        ValueConverterParameter? parameter = parameters.FirstOrDefault(kvp => string.Equals(kvp.Key, Pattern, comparison));

        value = default;
        if (parameter is not null)
        {
            if (_parseDelegate is not null)
            {
                value = _parseDelegate.Invoke(parameter.Values);
                return true;
            }
            else if (ResultForPatternMatches is not null && parameter.Values.Any())
            {
                var matches = new List<ResultForPatternMatch<T>>();
                foreach (var parameterValue in parameter.Values)
                {
                    ResultForPatternMatch<T>? resultForPatternMatch = ResultForPatternMatches.FirstOrDefault(rpm => rpm.Patterns.Any(p => string.Equals(p, parameterValue, comparison)));

                    if (resultForPatternMatch is not null)
                    {
                        value = resultForPatternMatch.Result;
                        break;
                    }
                }
                return true;
            }
        }

        return false;
    }
}
public class ValueConverterParameterDefinition
{
    public ValueConverterParameterDefinition(string pattern) : this(pattern, null)
    {
    }
    public ValueConverterParameterDefinition(string pattern, ValueConverterParameterOptions? options)
    {
        Options = options;
        Pattern = pattern;
    }

    protected ValueConverterParameterOptions? Options { get; }
    public string Pattern { get; }

    public virtual bool Match(IReadOnlyList<ValueConverterParameter> parameters)
    {
        var comparison = Options?.Comparer ?? StringComparison.OrdinalIgnoreCase;

        return parameters.Any(kvp => string.Equals(kvp.Key, Pattern, comparison));
    }
}