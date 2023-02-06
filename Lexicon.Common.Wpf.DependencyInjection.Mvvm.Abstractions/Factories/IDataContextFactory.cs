namespace Lexicon.Common.Wpf.DependencyInjection.Mvvm.Abstractions.Factories;
public interface IDataContextFactory
{
    TDataContext Create<TDataContext>() where TDataContext : class;
    TDataContext Create<TDataContext, TModel>(TModel model) where TDataContext : class where TModel : class;
    TDataContext Create<TDataContext, TModel1, TModel2>(TModel1 model1, TModel2 model2) where TDataContext : class where TModel1 : class where TModel2 : class;
    TDataContext Create<TDataContext, TModel1, TModel2, TModel3>(TModel1 model1, TModel2 model2, TModel3 model3) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class;
    TDataContext CreateAndShow<TDataContext>() where TDataContext : class;
    TDataContext CreateAndShow<TDataContext, TModel>(TModel model) where TDataContext : class where TModel : class;
    TDataContext CreateAndShow<TDataContext, TModel1, TModel2>(TModel1 model, TModel2 model2) where TDataContext : class where TModel1 : class where TModel2 : class;
    TDataContext CreateAndShow<TDataContext, TModel1, TModel2, TModel3>(TModel1 model, TModel2 model2, TModel3 model3) where TDataContext : class where TModel1 : class where TModel2 : class where TModel3 : class;
}
