using TransactionVisualizer.Models;
using TransactionVisualizer.Models.ParserModel;
using Xunit.Abstractions;

namespace TransactionVisualizerTest;

public class AccountParserTest
{
    [Fact]
    public void ParseAccountTest()
    {
        //Arrange
        
        string path =
            "/Accounts.csv";
        path = GetFullPath.ConvertRelativeToAbsolute(path) ;
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


        //Act
        FlatAccount flatAccounts = iParser.Pars(path)[0];

        // Assert
        Assert.Equivalent(flatAccounts, flatAccount);
    }
}