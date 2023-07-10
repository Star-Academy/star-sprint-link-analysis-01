namespace TransactionVisualizer.Models.ResponseModels.Builder;

using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

public interface IGraphResponseModelBuilder
{
    public GraphResponseModel<Account, BusinessModels.Transaction.Transaction> BuildTransactionGraphResponseModel(
        Dictionary<Account, List<Edge<Account, BusinessModels.Transaction.Transaction>>> graph);
}