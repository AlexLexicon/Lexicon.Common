namespace Lexicon.Common.Validation.DependencyInjection.Abstractions.Extensions;
public static class IAddValidationBuilderExtensions
{
    public static IAddValidationBuilder AddRuleSets<TAssemblyScanMarker>(this IAddValidationBuilder builder)
    {
        builder.Services.AddLexiconRuleSets<TAssemblyScanMarker>();

        return builder;
    }

    public static IAddValidationBuilder AddValidators<TAssemblyScanMarker>(this IAddValidationBuilder builder)
    {
        builder.Services.AddLexiconValidators<TAssemblyScanMarker>();

        return builder;
    }
}
