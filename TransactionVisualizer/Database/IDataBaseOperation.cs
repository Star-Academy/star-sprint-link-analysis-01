using Microsoft.EntityFrameworkCore;

namespace TransactionVisualizer.Database;

public interface IDataBaseOperation
{
    EntityState InsertBulk<T>(List<T> records);
    EntityState InsertRecord<T>(T record);
    bool Contain<T>(T record);
}