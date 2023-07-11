using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Models.ResponseModels.Builder;

public interface IGraphResponseModelBuilder
{
    public GraphResponseModel<Account.Account, Transaction.Transaction> BuildTransactionGraphResponseModel(
        Dictionary<Account.Account, List<Edge<Account.Account, Transaction.Transaction>>> graph);
}