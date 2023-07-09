using TransactionVisualizer.Models;
using TransactionVisualizer.Models.BusinessLogicModels.Account;
using TransactionVisualizer.Models.BusinessLogicModels.Transaction;
using TransactionVisualizer.Models.Graph;

namespace TransactionVisualizer.Services;

public interface IGraphService
{
    public void SetState(CustomGraph<Account, Transaction> graph);
    public CustomGraph<Account, Transaction> GetState();
    public CustomGraph<Account, Transaction> Expand(Account account, int MaxLenght);
    public decimal MaxFlow(Account source , Account destination);
    public CustomGraph<Account, Transaction> InitialGraph(long accountId);
}