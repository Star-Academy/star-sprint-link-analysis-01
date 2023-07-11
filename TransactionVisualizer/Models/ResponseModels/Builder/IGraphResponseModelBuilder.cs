using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Models.ResponseModels.Builder;

using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;

public interface IGraphResponseModelBuilder
{
    public GraphResponseModel<BusinessLogicModels.Account.Account, BusinessModels.Transaction.Transaction> BuildTransactionGraphResponseModel(
        Dictionary<BusinessLogicModels.Account.Account, List<Edge<BusinessLogicModels.Account.Account, BusinessModels.Transaction.Transaction>>> graph);
}