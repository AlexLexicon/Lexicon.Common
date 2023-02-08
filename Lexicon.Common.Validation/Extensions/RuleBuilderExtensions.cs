using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using System.Reflection;

namespace Lexicon.Common.Validation.Extensions;
public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> UseRuleSetWhenNotNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IRuleSet<TProperty> ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(ruleSet);

        return ruleBuilder
            .UseRuleSet(ruleSet)
            .WhenProperty(p => p is not null)
            .Null()
            .WhenProperty(p => p is null);
    }
    public static void UseRuleSetWhenNotNull<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, IRuleSet<TProperty> ruleSet, Action<IRuleBuilderOptions<TProperty?, TProperty>>? builder = null) where TProperty : struct
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(ruleSet);

        ruleBuilder
            .ChildRules(inLineValidator =>
            {
                IRuleBuilderOptions<TProperty?, TProperty> ruleBuilderOptions = inLineValidator
                    .RuleFor(p => p!.Value)
                    .UseRuleSet(ruleSet);

                builder?.Invoke(ruleBuilderOptions);
            })
            .WhenProperty(p => p is not null)
            .Null()
            .WhenProperty(p => p is null);
    }

    public static IRuleBuilderOptions<T, TProperty> UseRuleSet<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, IRuleSet<TProperty> ruleSet)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(ruleSet);

        //i cant figure out how else to convert to a builder options
        var ruleBuilderOptions = ruleBuilder.Must(_ => true);

        ruleSet.Use(ruleBuilderOptions);

        return ruleBuilderOptions;
    }

    public static IRuleBuilderOptions<T, TProperty> WhenProperty<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Func<TProperty?, bool> whenPredicate)
    {
        ArgumentNullException.ThrowIfNull(ruleBuilder);
        ArgumentNullException.ThrowIfNull(whenPredicate);

        return ruleBuilder.When((model, context) =>
        {
            PropertyInfo? property = model?.GetType().GetProperty(context.PropertyName);

            object? value = property?.GetValue(model);

            bool result = whenPredicate.Invoke((TProperty?)value);

            return result;
        });
    }
}
