namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
public interface IDataContextFactory
{
    TDataContext Create<TDataContext>() where TDataContext : class;
    TDataContext CreateAndShow<TDataContext>() where TDataContext : class;
}
