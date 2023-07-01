using Microsoft.EntityFrameworkCore;
using TransactionVisualizer.Models;
using TransactionVisualizer.Models.Transaction;

namespace TransactionVisualizer.Database;

public class InMemoryDataBase : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("TransactionVisualizer");
    }
}