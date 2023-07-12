using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.DataStructureModels.Graph;
using TransactionVisualizer.Models.RequestModels;
using TransactionVisualizer.Models.ResponseModels;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Services.Graph;

public interface IBankingTransactionNetworkService
{
    public Graph<Account, Transaction> GetState();

    public void SetState(GraphResponseModel<Account, Transaction> graph);

    public Graph<Account, Transaction> Expand(ExpandRequestModel<Account, Transaction> expandRequestModel);

    public decimal MaxFlowCalculator(MaxFlowCalculatorRequestModel<Account, Transaction> maxFlowCalculatorRequestModel);

    public Graph<Account, Transaction> InitialGraph(long accountId);
}