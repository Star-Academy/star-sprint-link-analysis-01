using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Utility.Builders.ResponseModelBuilder;

public interface IGraphResponseModelBuilder
{
    public GraphResponseModel<Account, Transaction> BuildTransactionGraphResponseModel(
        Dictionary<Account, List<Edge<Account, Transaction>>> graph);
}