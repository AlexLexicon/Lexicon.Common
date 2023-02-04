using Lexicon.Common.Validation.DependencyInjection.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicon.Common.Validation.DependencyInjection;
public class AddValidationBuilder : IAddValidationBuilder
{
    public AddValidationBuilder(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Services = services;
        Configuration = configuration;

        LexiconLanguageManager = new LexiconLanguageManager();
    }

    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
    public LexiconLanguageManager LexiconLanguageManager { get; }
}
