using FluentValidation;
using Lexicon.Common.Validation.Amenities.PropertyValidators;
using System.Diagnostics;

namespace Lexicon.Common.Validation.Amenities.Extensions;
public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> Guid<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new GuidPropertyValidator<T, TProperty>());
    }

    public static IRuleBuilderOptions<T, string?> Digits<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new DigitsPropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, string?> Letters<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new LettersPropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, string?> Alphanumeric<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new AlphanumericPropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, TProperty> SimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new SimplyEmptyPropertyValidator<T, TProperty>());
    }

    public static IRuleBuilderOptions<T, string?> NotAllWhitespaces<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new NotAllWhiteSpacesPropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, string?> NotAnyWhiteSpace<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new NotAnyWhiteSpacePropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, string?> NotAnyDigits<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new NotAnyDigitsPropertyValidator<T>());
    }

    public static IRuleBuilderOptions<T, TProperty> NotSimplyEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);

        return ruleBuilder.SetValidator(new NotSimplyEmptyPropertyValidator<T, TProperty>());
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
