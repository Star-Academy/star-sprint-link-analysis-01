using Nest;

namespace TransactionVisualizer.DataRepository.ModelsRepository;

public interface IModelRepository<TModel> where TModel : class
{
    void AddAll(List<TModel> models);

    List<TModel> Search(Func<SearchDescriptor<TModel>, ISearchRequest> selector);
}
