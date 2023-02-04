using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicon.Common.Validation.DependencyInjection.Abstractions;
public interface IAddValidationBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
    LexiconLanguageManager LexiconLanguageManager { get; }
}
