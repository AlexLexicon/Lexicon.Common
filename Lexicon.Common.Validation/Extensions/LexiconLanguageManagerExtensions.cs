using FluentValidation.Validators;

namespace Lexicon.Common.Validation.Extensions;
public static class LexiconLanguageManagerExtensions
{
    public static LexiconLanguageManager AddLexiconTranslations(this LexiconLanguageManager languageManager)
    {
        languageManager.AddEnTranslation(nameof(NotNullValidator<object, object>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(NotEmptyValidator<object, object>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(GreaterThanValidator<object, int>), "'{PropertyName}' must be greater than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(GreaterThanOrEqualValidator<object, int>), "'{PropertyName}' must be greater than or equal to {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LessThanValidator<object, int>), "'{PropertyName}' must be less than {ComparisonValue}.");
        languageManager.AddEnTranslation(nameof(LessThanOrEqualValidator<object, int>), "'{PropertyName}' must be less than or equal to {ComparisonValue}.");

        return languageManager;
    }
}
