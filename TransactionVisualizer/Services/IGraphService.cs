using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Graph;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Services;

public interface IGraphService
{
    public void SetState(CustomGraph<Account, Transaction> graph);
    public CustomGraph<Account, Transaction> GetState();
    public CustomGraph<Account, Transaction> Expand(Account account, int MaxLenght);
    public decimal MaxFlow(Account source , Account destination);
}