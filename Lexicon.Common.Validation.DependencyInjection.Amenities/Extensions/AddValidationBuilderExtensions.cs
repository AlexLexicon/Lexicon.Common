using Lexicon.Common.Validation.DependencyInjection.Abstractions;

namespace Lexicon.Common.Validation.DependencyInjection.Amenities.Extensions;
public static class AddValidationBuilderExtensions
{
    public static IAddValidationBuilder AddAmenities(this IAddValidationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexiconAmenities(builder.Configuration, builder.LexiconLanguageManager);

        return builder;
    }
}
