using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TransactionVisualizer.Database;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace TransactionVisualizerTest;

public class DataBaseOperationTest
{
    private IDataBaseOperation _dataBaseOperation;
    private ITestOutputHelper _testOutputHelper;

    public DataBaseOperationTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _dataBaseOperation = new DataBaseOperation(new InMemoryDataBase());
    }

    [Fact]
    public void InsertBulk_NullBulkPass_ThrowNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _dataBaseOperation.InsertBulk<Account>(new List<Account>()));
    }

    [Fact]
    public void InsertRecord_InsertRealRecord_Added()
    {
        // Account flatAccount = new Account
        // {
        //     AccountID = 6534454617,
        //     CardID = "6104335000000190",
        //     Sheba = "IR120778801496000000198",
        //     AccountType = AccountType.Jari,
        //     Branch = 
        // };

        Branch branch = new Branch();
        branch.Id = 1;
        branch.Address = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک";
        branch.Name = "گلوبندک";
        branch.Telephone = "55638667";


        var state = _dataBaseOperation.InsertRecord<Branch>(branch);
        Assert.Equal(EntityState.Added, state);
    }
    [Fact]
    public void InsertRecord_DuplicatedRecord_UnChangedState()
    {
        Branch branch = new Branch();
        branch.Id = 1;
        branch.Address = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک";
        branch.Name = "گلوبندک";
        branch.Telephone = "55638667";


        var state = _dataBaseOperation.InsertRecord<Branch>(branch);
        state = _dataBaseOperation.InsertRecord<Branch>(branch);
        Assert.Equal(EntityState.Unchanged, state);
    }

    [Fact]
    public void InsertRecord_NullRecord_ThrowNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _dataBaseOperation.InsertRecord<Account>(null));
    }
}