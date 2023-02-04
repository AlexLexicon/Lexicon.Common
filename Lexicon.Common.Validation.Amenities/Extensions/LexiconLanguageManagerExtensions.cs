using Lexicon.Common.Validation.Amenities.PropertyValidators;

namespace Lexicon.Common.Validation.Amenities.Extensions;
public static class LexiconLanguageManagerExtensions
{
    public static LexiconLanguageManager AddLexiconAmenitiesTranslations(this LexiconLanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(languageManager);

        languageManager.AddEnTranslation(nameof(DigitsPropertyValidator<object>), "'{PropertyName}' must contain only digits.");
        languageManager.AddEnTranslation(nameof(GuidPropertyValidator<object, object>), "'{PropertyName}' must be a Guid.");
        languageManager.AddEnTranslation(nameof(LettersPropertyValidator<object>), "'{PropertyName}' must contain only letters.");
        languageManager.AddEnTranslation(nameof(AlphanumericPropertyValidator<object>), "'{PropertyName}' must contain only letters or digits.");
        languageManager.AddEnTranslation(nameof(NotAllWhiteSpacesPropertyValidator<object>), "The '{PropertyName}' field is required.");
        languageManager.AddEnTranslation(nameof(NotAnyDigitsPropertyValidator<object>), "'{PropertyName}' must not contain any digits.");
        languageManager.AddEnTranslation(nameof(NotAnyWhiteSpacePropertyValidator<object>), "'{PropertyName}' must not contain any white space characters.");
        languageManager.AddEnTranslation(nameof(NotSimplyEmptyPropertyValidator<object, object>), "The '{PropertyName}' field is required.");

        return languageManager;
    }
}
