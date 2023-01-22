using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicon.Common.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureAndBind<T>(this IServiceCollection services, IConfiguration configuration) where T : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<T>(config =>
        {
            configuration.Bind(typeof(T).Name, config);
        });

        //the following allows the configuration type to be used with an IOptionsMonitor<>
        services.AddSingleton<IOptionsChangeTokenSource<T>, ConfigurationChangeTokenSource<T>>();

        return services;
    }
}
