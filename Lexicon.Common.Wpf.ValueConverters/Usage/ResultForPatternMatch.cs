namespace Lexicon.Common.Wpf.ValueConverters.Usage;
public class ResultForPatternMatch<T>
{
    public ResultForPatternMatch(T result, IEnumerable<string> matchPatterns)
    {
        Result = result;
        Patterns = matchPatterns.ToList();
    }

    public T Result { get; }
    public IReadOnlyList<string> Patterns { get; }
}
