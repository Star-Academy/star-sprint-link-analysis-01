using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Services;

public interface IGraphService
{
    public void SetState(GraphResponseModel<Account, Transaction> graph);

    public Graph<Account, Transaction> GetState();

    public Graph<Account, Transaction> Expand(Account accountId, int MaxLenght);

    public decimal MaxFlow(MaxFlowRequestModel<Account, Transaction> maxFlowRequestModel);

    public Graph<Account, Transaction> InitialGraph(long accountId);
}