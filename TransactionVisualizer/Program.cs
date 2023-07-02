using Nest;

public class Program
{
    public static void Main(string[] args)
    {
        string indexName = "bank-accounts-3";
        index(indexName);
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex(indexName)
            .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true);
        
        var client = new ElasticClient(settings);

        string serchVale = "Jane Doe";
        var searchResponse = client.Search<BankAccount>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(f => f.AccountHolderName)
                    .Query(serchVale)
                )
            )
        );

        Console.WriteLine(searchResponse.IsValid);
        
        if (searchResponse.IsValid)
        {
            // Console.WriteLine();
            // Console.WriteLine($"Found {searchResponse.Documents.Count} bank accounts.");
            foreach (var account in searchResponse.Documents)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Account Holder Name: {account.AccountHolderName}, Balance: {account.Balance}");
            }
        }
        else
        {
            // Console.WriteLine(searchResponse);

            //Console.WriteLine($"Failed to search bank accounts. Error: {searchResponse.OriginalException.Message}");
        }
    }

    static void index(string indexName)
    {
        var bankAccounts = new List<BankAccount>
        {
            new BankAccount { AccountNumber = 1001, AccountHolderName = "John Doe", Balance = 1000.00m },
            new BankAccount { AccountNumber = 1002, AccountHolderName = "Jane Doe", Balance = 2000.00m },
            new BankAccount { AccountNumber = 1003, AccountHolderName = "Bob Smith", Balance = 500.00m }
        };
        
        var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex(indexName);

        var client = new ElasticClient(settings);

        var indexManyResponse = client.IndexMany(bankAccounts);

        if (indexManyResponse.Errors)
        {
            foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
            {
                Console.WriteLine($"Failed to index document with ID {itemWithError.Id}. Error: {itemWithError.Error}");
            }
        }
        else
        {
            Console.WriteLine($"Indexed {bankAccounts.Count} documents successfully.");
        }
    }
    
    static void indexMony(string indexName)
    {
        var bankAccounts = new List<BankAccount>
        {
            new BankAccount { AccountNumber = 1001, AccountHolderName = "John Doe", Balance = 1000.00m },
            new BankAccount { AccountNumber = 1002, AccountHolderName = "Jane Doe", Balance = 2000.00m },
            new BankAccount { AccountNumber = 1003, AccountHolderName = "Bob Smith", Balance = 500.00m }
        };
        
        var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex(indexName);

        var client = new ElasticClient(settings);

        var indexManyResponse = client.Index(bankAccounts, idx => idx.Index(indexName));

        Console.WriteLine(indexManyResponse.IsValid);
        if (!indexManyResponse.IsValid)
        {
            // foreach (var itemWithError in indexManyResponse.ItemsWithErrors)
            // {
            //     Console.WriteLine($"Failed to index document with ID {itemWithError.Id}. Error: {itemWithError.Error}");
            // }
        }
        else
        {
            Console.WriteLine($"Indexed {bankAccounts.Count} documents successfully.");
        }
    }

}

public class BankAccount
{
    public int AccountNumber { get; set; }
    public string AccountHolderName { get; set; }
    public decimal Balance { get; set; }
}

public interface IElasticOperation
{ 
    BulkResponse IndexBulk<T>(List<T> documents) where T : class;
    IndexResponse IndexDocument<T>(IIndexRequest<T> document) where T : class;
    T? SearchDocument<T>(long id);
    bool Contain<T>(long id);
}

public class ElasticOperation : IElasticOperation
{
    public string IndexName { get; set; }
    public ElasticClient Client { get; set; }

    public ElasticOperation(string url, string indexName)
    {
        IndexName = indexName;
        Client = new ElasticClient(new ConnectionSettings(new Uri(url)).DefaultIndex(indexName));
    }

    public BulkResponse IndexBulk<T>(List<T> documents) where T : class
    {
        return Client.IndexMany(documents);
    }

    public IndexResponse IndexDocument<T>(IIndexRequest<T> document) where T : class
    {
        return Client.Index<T>(document);
    }

    public T? SearchDocument<T>(long id)
    {
        throw new NotImplementedException();
    }

    public bool Contain<T>(long id)
    {
        throw new NotImplementedException();
    }
}