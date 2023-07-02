using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TransactionVisualizer.Database;
// using TransactionVisualizer.Database;
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
        _dataBaseOperation = new DataBaseOperation(new DatabaseContext());
    }

    [Fact]
    public void InsertBulk_NullBulkPass_ThrowNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            _dataBaseOperation.InsertBulk<ClassNotStoredInDatabaseTest>(new List<ClassNotStoredInDatabaseTest>()));
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
        branch.Id = 2;
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

    [Fact]
    public void SelectBulk_ClassNotStoredInDatabase_ThrowNullException()
    {
        Assert.Throws<InvalidOperationException>(() => _dataBaseOperation.SelectBulk<ClassNotStoredInDatabaseTest>());
    }

    [Fact]
    public void SelectBulk_ReturnListAndCheckLength_True()
    {
        List<Branch> accounts = _dataBaseOperation.SelectBulk<Branch>();

        Assert.Equal(4, accounts.Count);
    }

    [Fact]
    public void SelectRecord_ClassNotStoredInDatabase_ThrowNullException()
    {
        Assert.Throws<InvalidOperationException>(() => _dataBaseOperation.SelectRecord<ClassNotStoredInDatabaseTest>(1));
    }

    [Fact]
    public void SelectRecord_NonExistId_ThrowNullException()
    {
        Account? account = _dataBaseOperation.SelectRecord<Account>(1);
        
        Assert.Null(account);
    }

    [Fact]
    public void SelectRecord_ReturnRecord_True()
    {
        
        Branch? account = _dataBaseOperation.SelectRecord<Branch>(1);

        Assert.Equal(1, account.Id);

    }

    [Fact]
    public void ContainRecord_ExistedRecord_ReturnTrue()
    {
        Branch branch = new Branch();
        branch.Id = 4;
        branch.Address = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک";
        branch.Name = "گلوبندک";
        branch.Telephone = "55638667";

        _dataBaseOperation.InsertRecord(branch);
        Assert.True(_dataBaseOperation.Contain<Branch>(branch.Id));

    }
    [Fact]
    public void ContainRecord_NotExistedRecord_ReturnFalse()
    {

        Assert.False(_dataBaseOperation.Contain<Branch>(10));

    }

    [Fact]
    public void InsertBulk_ExistedRecord_ReturnUnchanged()
    {
        Branch branch = new Branch();
        branch.Id = 6;
        branch.Address = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک";
        branch.Name = "گلوبندک";
        branch.Telephone = "55638667";
        
        _dataBaseOperation.InsertRecord(branch);
        
        
        EntityState actual = _dataBaseOperation.InsertBulk(new List<Branch> { branch });
        Assert.Equal(EntityState.Unchanged,actual);
    }
    [Fact]
    public void InsertBulk_NotExistedRecord_ReturnAdded()
    {
        Branch branch = new Branch();
        branch.Id = 7;
        branch.Address = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک";
        branch.Name = "گلوبندک";
        branch.Telephone = "55638667";
        
        
        
        
        EntityState actual = _dataBaseOperation.InsertBulk(new List<Branch> { branch });
        Assert.Equal(EntityState.Added,actual);
    }
}

class ClassNotStoredInDatabaseTest
{
}
