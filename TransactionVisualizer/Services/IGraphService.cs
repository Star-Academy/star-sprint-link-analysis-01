using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.Graph;
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