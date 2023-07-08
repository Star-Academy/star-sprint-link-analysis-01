using FluentAssertions;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Utility.Converters;

namespace TransactionVisualizerTest.UtilityTest.Converters;

public class FlatAccountToAccountConverterTest
{
    [Fact]
    public void Convert_ShouldConvertFlatAccountToAccount()
    {
        // Arrange
        var converter = new FlatAccountToAccountConverter();
        var flatAccount = new FlatAccount
        {
            AccountId = 6534454617,
            CardId = "6104335000000190",
            Sheba = "IR120778801496000000198",
            AccountType = "پس انداز",
            BranchTelephone = "55638667",
            BranchAdress = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک",
            BranchName = "گلوبندک",
            OwnerName = "افسر",
            OwnerFamilyName = "طباطبایی",
            OwnerId = 1227114110
        };

        // Act
        var result = converter.Convert(flatAccount);

        // Assert
        result.Should().NotBeNull();
        result.AccountId.Should().Be(flatAccount.AccountId);
        result.CardId.Should().Be(flatAccount.CardId);
        result.Sheba.Should().Be(flatAccount.Sheba);
        result.AccountType.Should().Be(AccountType.Pasandaz);
        result.Owner.Should().NotBeNull();
        result.Owner.Id.Should().Be(flatAccount.OwnerId);
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
                AccountId = 6534454617,
                CardId = "6104335000000190",
                Sheba = "IR120778801496000000198",
                AccountType = "پس انداز",
                BranchTelephone = "55638667",
                BranchAdress = "تهران-خیابان خیام-بالاتر از چهارراه گلوبندک",
                BranchName = "گلوبندک",
                OwnerName = "افسر",
                OwnerFamilyName = "طباطبایی",
                OwnerId = 1227114110
            }
        };

        // Act
        var result = converter.ConvertAll(flatAccounts);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(flatAccounts.Count);

        foreach (var (flatAccount, account) in flatAccounts.Zip(result, (f, a) => (f, a)))
        {
            account.AccountId.Should().Be(flatAccount.AccountId);
            account.CardId.Should().Be(flatAccount.CardId);
            account.Sheba.Should().Be(flatAccount.Sheba);
            account.AccountType.Should().Be(flatAccount.AccountType.ParsAccountType());
            account.Owner.Should().NotBeNull();
            account.Owner.Id.Should().Be(flatAccount.OwnerId);
            account.Owner.Name.Should().Be(flatAccount.OwnerName);
            account.Owner.FamilyName.Should().Be(flatAccount.OwnerFamilyName);
            account.Branch.Should().NotBeNull();
            account.Branch.Name.Should().Be(flatAccount.BranchName);
            account.Branch.Address.Should().Be(flatAccount.BranchAdress);
            account.Branch.Telephone.Should().Be(flatAccount.BranchTelephone);
        }
    }
}