using Lexicon.Common.Wpf.DependencyInjection.Amenities.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicon.Common.Wpf.DependencyInjection.Amenities.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLexiconAmenities(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<IWindowsDialogService, IWindowsDialogService>();

        return services;
    }
}
