using FluentAssertions;
using FluentAssertions.Execution;
using TransactionVisualizer;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility;

namespace TransactionVisualizerTest;

public class GraphGeneratorTests
{

    [Fact]
    public void GenerateTransactionGraph_ThrowsArgumentNullException_WhenAccountIsNull()
    {
        // Arrange
        var generator = new GraphGenerator();
        var transactions = new List<Transaction>
        {
            new Transaction { ID = 1, SourceAcount = 1, DestiantionAccount = 2, Amount = 100 }
        };
        var act = generator.GenerateTransactionGraph(transactions, null);
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => act);
    }

    [Fact]
    public void GenerateTransactionGraph_ThrowsAccountNotFoundException_WhenAccountDoesNotExist()
    {
        // Arrange
        var generator = new GraphGenerator();
        var accounts = new List<Account>
        {
            new Account { AccountID = 1, CardID = "1234", Sheba = "1234567890" }
        };
        generator.AddAccounts(accounts);
        var transactions = new List<Transaction>
        {
            new Transaction { ID = 1, SourceAcount = 1, DestiantionAccount = 2, Amount = 100 }
        };

        // Act & Assert
        Assert.Throws<AccountNotFoundException>(() => generator.GenerateTransactionGraph(transactions, accounts[0]));
    }

    [Fact]
    public void GenerateTransactionGraph_AddsVertexAndEdgesToGraph()
    {
        // Arrange
        var generator = new GraphGenerator();
        var accounts = new List<Account>
        {
            new Account { AccountID = 1, CardID = "1234", Sheba = "1234567890" },
            new Account { AccountID = 2, CardID = "5678", Sheba = "0987654321" },
            new Account { AccountID = 3, CardID = "9012", Sheba = "1234509876" }
        };
        generator.AddAccounts(accounts);
        var transactions = new List<Transaction>
        {
            new Transaction { ID = 1, SourceAcount = 1, DestiantionAccount = 2, Amount = 100 },
            
        };
        var transactions2 = new List<Transaction>
        {
            new Transaction { ID = 2, SourceAcount = 2, DestiantionAccount = 3, Amount = 100 }
            
        };
        

        // Act
        generator.Expand(accounts[0], transactions);
        var graph = generator.Expand(accounts[1], transactions2);
        
        // Assert
        
        Assert.Contains(accounts[0], graph.Vertex);
        Assert.Contains(accounts[1], graph.Vertex);
        Assert.Equal(accounts[0], graph.Edge[0].Source);
        Assert.Equal(accounts[1], graph.Edge[0].Destination);
        Assert.Equal(transactions[0], graph.Edge[0].Content);
    }
    
}