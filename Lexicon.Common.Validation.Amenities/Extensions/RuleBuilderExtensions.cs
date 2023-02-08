using FluentValidation;
using FluentValidation.Validators;
using Lexicon.Common.Validation.Amenities.PropertyValidators;
using System.Diagnostics;

namespace Lexicon.Common.Validation.Amenities.Extensions;
public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string?> Alphanumeric<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<AlphanumericPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, string?> Digits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<DigitsPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, TProperty> Guid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.CreateAndSetValidator<GuidPropertyValidator<T, TProperty>, T, TProperty>();
    public static IRuleBuilderOptions<T, string?> Letters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<LettersPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, string?> NotAllWhitespaces<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<NotAllWhiteSpacesPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, string?> NotAnyDigits<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<NotAnyDigitsPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, string?> NotAnyWhiteSpace<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<NotAnyWhiteSpacePropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, string?> NotEscapedCharacters<T>(this IRuleBuilder<T, string?> ruleBuilder) => ruleBuilder.CreateAndSetValidator<NotEscapedCharactersPropertyValidator<T>, T, string?>();
    public static IRuleBuilderOptions<T, TProperty> NotSimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.CreateAndSetValidator<NotSimplyEmptyPropertyValidator<T, TProperty>, T, TProperty>();
    public static IRuleBuilderOptions<T, TProperty> SimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) => ruleBuilder.CreateAndSetValidator<SimplyEmptyPropertyValidator<T, TProperty>, T, TProperty>();

    private static IRuleBuilderOptions<T, TProperty> CreateAndSetValidator<TValidator, T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) where TValidator : IPropertyValidator<T, TProperty>, new()
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new TValidator());
    }

    public static IRuleBuilder<T, string?> Length<T>(this IRuleBuilder<T, string?> ruleBuilder, int? min, int? max)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        if (min is not null && min.Value == int.MinValue)
        {
            min = null;
        }

        if (max is not null && max.Value == int.MaxValue)
        {
            max = null;
        }

        if (min is null && max is null)
        {
            return ruleBuilder;
        }

        if (min is null)
        {
            if (max is null)
            {
                throw new UnreachableException($"{nameof(max)} should never be able to null by this point.");
            }

            return DefaultValidatorExtensions.MaximumLength(ruleBuilder, max.Value);
        }

        if (max is null)
        {
            if (min == 1)
            {
                //if we are saying the length must be at least 1 its the same as being simply not empty
                //which is a better message to use rather than length
                //if the simply not empty is already included, duplicate error messages are removed
                return ruleBuilder.NotSimplyEmpty();
            }

            return DefaultValidatorExtensions.MinimumLength(ruleBuilder, min.Value);
        }

        if (min == max)
        {
            return DefaultValidatorExtensions.Length(ruleBuilder, min.Value);
        }

        return DefaultValidatorExtensions.Length(ruleBuilder, min.Value, max.Value);
    }
}
