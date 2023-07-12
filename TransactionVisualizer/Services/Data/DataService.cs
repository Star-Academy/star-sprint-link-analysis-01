using TransactionVisualizer.DataRepository.BaseDataRepository;
using TransactionVisualizer.Models.Account;
using TransactionVisualizer.Models.Transaction;
using TransactionVisualizer.Utility.Builders.DataRepositoryBuilder;
using TransactionVisualizer.Utility.Builders.SelectorBuilder;
using TransactionVisualizer.Utility.Converters.FlatToFull;
using TransactionVisualizer.Utility.Parsers.FileParsers;

namespace TransactionVisualizer.Services.Data;

public class DataService : IDataService
{
    private readonly IFlatToFullConverter<Account, FlatAccount> _accountConverter;
    private readonly IFileParser<FlatAccount> _accountParser;
    private readonly IDataRepository<Account> _accountRepository;
    private readonly ISelectorBuilder _selectorBuilder;
    private readonly ISelectorKeyValueBuilder _selectorKeyValueBuilder;
    private readonly IFlatToFullConverter<Transaction, FlatTransaction> _transactionConverter;
    private readonly IFileParser<FlatTransaction> _transactionParser;
    private readonly IDataRepository<Transaction> _transactionRepository;


    public DataService(IFlatToFullConverter<Transaction, FlatTransaction> transactionConverter,
        IFlatToFullConverter<Account, FlatAccount> accountConverter, IFileParser<FlatTransaction> transactionParser,
        IFileParser<FlatAccount> accountParser, ISelectorKeyValueBuilder selectorKeyValueBuilder,
        ISelectorBuilder selectorBuilder, IElasticDataRepositoryBuilder<Account> accountRepositoryBuilder,
        IElasticDataRepositoryBuilder<Transaction> transactionRepositoryBuilder)
    {
        _transactionConverter = transactionConverter;
        _accountConverter = accountConverter;
        _transactionParser = transactionParser;
        _accountParser = accountParser;
        _selectorKeyValueBuilder = selectorKeyValueBuilder;
        _selectorBuilder = selectorBuilder;
        _accountRepository = accountRepositoryBuilder.Build();
        _transactionRepository = transactionRepositoryBuilder.Build();
    }

    public bool AddAccounts(string filePath)
    {
        var accounts = _accountParser.Pars(filePath);
        var fullAccounts = _accountConverter.ConvertAll(accounts);
        var response = _accountRepository.InsertAll(fullAccounts);
        return !response.Error;
    }

    public bool AddTransactions(string filePath)
    {
        var transactions = _transactionParser.Pars(filePath);
        transactions = transactions.Where(IsAccountIdFound).ToList();
        var fullTransactions = _transactionConverter.ConvertAll(transactions);
        var response = _transactionRepository.InsertAll(fullTransactions);
        return !response.Error;
    }

    private bool IsAccountIdFound(FlatTransaction item)
    {
        var isSourceFound = _accountRepository.Contain(
            _selectorBuilder.BuildKeyValueSelector<Account>(
                _selectorKeyValueBuilder.BuildFindAccountById(item.SourceAccount.ToString()
                )
            )
        );

        var isDestinationFound = _accountRepository.Contain(
            _selectorBuilder.BuildKeyValueSelector<Account>(
                _selectorKeyValueBuilder.BuildFindAccountById(item.DestinationAccount.ToString()
                )
            )
        );

        return isSourceFound && isDestinationFound;
    }
}