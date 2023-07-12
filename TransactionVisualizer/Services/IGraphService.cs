using TransactionVisualizer.Models;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Models.DataStructureModels.Graph;

namespace TransactionVisualizer.Services;

public interface IGraphService
{
    public void SetState(Graph<Account, Transaction> graph);
    public Graph<Account, Transaction> GetState();
    public Graph<Account, Transaction> Expand(Account account, int MaxLenght);
    public decimal MaxFlow(Account source , Account destination);
    public Graph<Account, Transaction> InitialGraph(long accountId);
}