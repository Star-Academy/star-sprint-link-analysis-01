using Microsoft.EntityFrameworkCore;
using TransactionVisualizerTest;

namespace TransactionVisualizer.Database;

public class DataBaseOperation : IDataBaseOperation
{
    private DbContext _dbContext;

    public DataBaseOperation(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public EntityState InsertBulk<T>(List<T> records)
    {
        if (records.Count == 0)
        {
            throw new ArgumentNullException();
        }

        throw new NotImplementedException();
    }

    public EntityState InsertRecord<T>(T record)
    {
        if (record is null) throw new ArgumentNullException();

        EntityState state = _dbContext.Add(record).State;
        try 
        {
            _dbContext.SaveChanges();
        }
        catch (System.ArgumentException)
        {

            //TODO : add extention to EntitySate
            return EntityState.Unchanged;
        }
        return state;
    }

    public bool Contain<T>(T record)
    {
        throw new NotImplementedException();
    }
}