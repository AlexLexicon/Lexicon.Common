using FluentValidation;
using Lexicon.Common.Validation.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicon.Common.Validation.DependencyInjection.Abstractions.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLexiconRuleSets<TAssemblyScanMarker>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Type[] concreteTypes = typeof(TAssemblyScanMarker).Assembly.DefinedTypes.ToArray();

        var results = new List<AddRuleSetsResult>();
        foreach (Type concreteType in concreteTypes)
        {
            if (!concreteType.IsAbstract && TypeOrBaseTypeIsAbstractRuleSet(concreteType, out Type? abstractRuleSetGenericArgumentType) && abstractRuleSetGenericArgumentType is not null)
            {
                Type[] interfaceTypes = concreteType.GetInterfaces();

                var ruleSetInterfaces = new List<Type>();
                foreach (Type interfaceType in interfaceTypes)
                {
                    if (InterfaceTypeIsIRuleSet(interfaceType, abstractRuleSetGenericArgumentType))
                    {
                        if (!ruleSetInterfaces.Contains(interfaceType))
                        {
                            ruleSetInterfaces.Add(interfaceType);
                        }
                    }
                }

                results.Add(new AddRuleSetsResult
                {
                    ConcreteType = concreteType,
                    InterfaceTypes = ruleSetInterfaces,
                    AbstractRuleSetGenericArgumentType = abstractRuleSetGenericArgumentType,
                });
            }
        }

        if (results.Any())
        {
            foreach (AddRuleSetsResult result in results)
            {
                //add the AbstractRuleSet type
                services.AddScoped(result.ConcreteType);

                foreach (var interfaceType in result.InterfaceTypes)
                {
                    //add all IRuleSet interfaces for the found AbstractRuleSet
                    services.Add(new ServiceDescriptor(interfaceType, sp =>
                    {
                        return sp.GetRequiredService(result.ConcreteType);
                    }, ServiceLifetime.Scoped));
                }
            }
        }

        return services;

        bool TypeOrBaseTypeIsAbstractRuleSet(Type type, out Type? abstractRuleSetGenericArgumentType)
        {
            ArgumentNullException.ThrowIfNull(type);

            abstractRuleSetGenericArgumentType = null;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AbstractRuleSet<>))
            {
                abstractRuleSetGenericArgumentType = type.GetGenericArguments()[0];

                return true;
            }

            if (type.BaseType is null)
            {
                return false;
            }

            return TypeOrBaseTypeIsAbstractRuleSet(type.BaseType, out abstractRuleSetGenericArgumentType);
        }
        bool InterfaceTypeIsIRuleSet(Type interfaceType, Type abstractGenericType)
        {
            ArgumentNullException.ThrowIfNull(interfaceType);
            ArgumentNullException.ThrowIfNull(abstractGenericType);

            if (interfaceType.IsInterface && interfaceType.IsGenericType)
            {
                Type ruleSetInterfaceType = typeof(IRuleSet<>).MakeGenericType(abstractGenericType);

                if (interfaceType == ruleSetInterfaceType)
                {
                    return true;
                }
            }

            Type[] subInterfaces = interfaceType.GetInterfaces();

            foreach (Type subInterface in subInterfaces)
            {
                return InterfaceTypeIsIRuleSet(subInterface, abstractGenericType);
            }

            return false;
        }
    }
    private class AddRuleSetsResult
    {
        public required Type ConcreteType { get; init; }
        public required List<Type> InterfaceTypes { get; init; }
        public required Type AbstractRuleSetGenericArgumentType { get; init; }
    }

    public static IServiceCollection AddLexiconValidators<TAssemblyScanMarker>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        AssemblyScanner scanResults = AssemblyScanner.FindValidatorsInAssembly(typeof(TAssemblyScanMarker).Assembly);
        foreach (AssemblyScanner.AssemblyScanResult scanResult in scanResults)
        {
            services.Add(new ServiceDescriptor(scanResult.InterfaceType, scanResult.ValidatorType, ServiceLifetime.Scoped));
        }

        return services;
    }
}
