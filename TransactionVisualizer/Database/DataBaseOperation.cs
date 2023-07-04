using System.Collections;
using Microsoft.EntityFrameworkCore;
using TransactionVisualizer.Models;

namespace TransactionVisualizer.Database;

public class DataBaseOperation : IDataBaseOperation
{
    private readonly DbContext _dbContext;

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

        foreach (var record in records)
        {
            _dbContext.Add(record);
        }

        try
        {
            _dbContext.SaveChanges();
        }
        catch (System.Exception e)
        {
            return EntityState.Unchanged;
        }


        return EntityState.Added;
    }

    public EntityState InsertRecord<T>(T record)
    {
        if (record is null) throw new ArgumentNullException();

        EntityState state = _dbContext.Add(record).State;
        try
        {
            _dbContext.SaveChanges();
        }
        catch (System.Exception e)
        {
            //TODO : add extention to EntitySate
            return EntityState.Unchanged;
        }

        return state;
    }

    public List<T> SelectBulk<T>() where T : class
    {
        return _dbContext.Set<T>().ToList();
    }

    public T? SelectRecord<T>(long id) where T : class
    {
        return _dbContext.Find<T>(id);
    }


    public bool Contain<T>(long id) where T : class
    {
        return SelectRecord<T>(id) != null;
    }
}