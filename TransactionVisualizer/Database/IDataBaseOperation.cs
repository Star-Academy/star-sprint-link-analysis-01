using Microsoft.EntityFrameworkCore;

namespace TransactionVisualizer.Database;

public interface IDataBaseOperation
{
    EntityState InsertBulk<T>(List<T> records);
    EntityState InsertRecord<T>(T record);
    List<T> SelectBulk<T>() where T : class;
    T? SelectRecord<T>(long id) where T : class;
    bool Contain<T>(long id) where T : class;
}