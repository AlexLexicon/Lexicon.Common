using FluentValidation.Resources;

namespace Lexicon.Common.Validation;
public class LexiconLanguageManager : LanguageManager
{
    public void AddEnTranslation(string key, string message)
    {
        AddTranslation("en", key, message);
        AddTranslation("en-US", key, message);
        AddTranslation("en-GB", key, message);
    }
}
