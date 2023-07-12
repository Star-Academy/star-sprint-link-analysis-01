using FluentAssertions;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Converters;
using TransactionVisualizer.Utility.Converters.FlatToFull;
using TransactionVisualizer.Utility.Parsers.EnumParsers;

namespace TransactionVisualizerTest.UtilityTest.Converters.FlatToFull;

public class FlatAccountToAccountConverterTest
{
    [Fact]
    public void Convert_ShouldConvertFlatAccountToAccount()
    {
        // Arrange
        var converter = new FlatAccountToAccountConverter();
        var flatAccount = new FlatAccount
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

        // Act
        var result = converter.Convert(flatAccount);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(flatAccount.AccountID);
        result.CardId.Should().Be(flatAccount.CardID);
        result.Sheba.Should().Be(flatAccount.Sheba);
        result.AccountType.Should().Be(AccountType.Pasandaz);
        result.Owner.Should().NotBeNull();
        result.Owner.Id.Should().Be(flatAccount.OwnerID);
        result.Owner.Name.Should().Be(flatAccount.OwnerName);
        result.Owner.FamilyName.Should().Be(flatAccount.OwnerFamilyName);
        result.Branch.Should().NotBeNull();
        result.Branch.Name.Should().Be(flatAccount.BranchName);
        result.Branch.Address.Should().Be(flatAccount.BranchAdress);
        result.Branch.Telephone.Should().Be(flatAccount.BranchTelephone);
    }

    [Fact]
    public void ConvertAll_ShouldConvertAllFlatAccountsToAccounts()
    {
        // Arrange
        var converter = new FlatAccountToAccountConverter();
        var flatAccounts = new List<FlatAccount>
        {
            new FlatAccount
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
            }
        };

        // Act
        var result = converter.ConvertAll(flatAccounts);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(flatAccounts.Count);

        foreach (var (flatAccount, account) in flatAccounts.Zip(result, (f, a) => (f, a)))
        {
            account.Id.Should().Be(flatAccount.AccountID);
            account.CardId.Should().Be(flatAccount.CardID);
            account.Sheba.Should().Be(flatAccount.Sheba);
            account.AccountType.Should().Be(AccountTypeParser.Pars(flatAccount.AccountType));
            account.Owner.Should().NotBeNull();
            account.Owner.Id.Should().Be(flatAccount.OwnerID);
            account.Owner.Name.Should().Be(flatAccount.OwnerName);
            account.Owner.FamilyName.Should().Be(flatAccount.OwnerFamilyName);
            account.Branch.Should().NotBeNull();
            account.Branch.Name.Should().Be(flatAccount.BranchName);
            account.Branch.Address.Should().Be(flatAccount.BranchAdress);
            account.Branch.Telephone.Should().Be(flatAccount.BranchTelephone);
        }
    }
}