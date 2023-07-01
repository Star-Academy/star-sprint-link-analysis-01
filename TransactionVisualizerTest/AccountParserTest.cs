using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;
using Xunit.Abstractions;

namespace TransactionVisualizerTest;

public class AccountParserTest
{
    private ITestOutputHelper _out;

    public AccountParserTest(ITestOutputHelper @out)
    {
        _out = @out;
    }
    
    [Fact]
    public void ParseAccountTest()
    {
        string path =
            "/Users/mahdimazaheri/Programming/Mohaymen/TransactionVisualizer/TransactionVisualizerTest/Accounts.csv";
        IParser<FlatAccount> iParser = new Parser<FlatAccount>();
        FlatAccount flatAccount = new FlatAccount
        {
            AccountID = 6534454617,
            CardID = "6104335000000190",
            Sheba = "IR120778801496000000198",
            AccountType = "پس انداز",
            BranchTelephone = "55638667",
            BranchAdress = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک",
            BranchName = "گلوبندک",
            OwnerName = "افسر",
            OwnerFamilyName = "طباطبایی",
            OwnerID = 1227114110
        };


        FlatAccount flatAccounts = iParser.Pars(path)[0];
        
        // _out.WriteLine(accounts);
        Assert.Equivalent(flatAccounts, flatAccount);
    }
}