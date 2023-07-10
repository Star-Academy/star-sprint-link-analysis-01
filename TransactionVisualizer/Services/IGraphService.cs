using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Services;

public interface IGraphService
{
    public void SetState(GraphResponseModel<Account, Transaction> graph);
    public CustomGraph<Account, Transaction> GetState();
    public CustomGraph<Account, Transaction> Expand(Account accountId, int MaxLenght);
    public decimal MaxFlow(MaxFlowRequestModel<Account, Transaction> maxFlowRequestModel);
    public CustomGraph<Account, Transaction> InitialGraph(long accountId);
}