using Microsoft.EntityFrameworkCore;
using webAplication.DAL.Interfaces;

namespace webAplication.DAL;

public class Repository<T> : IBaseRepository<T> where T : Entity
{
    public Repository(DbSet<T> set)
    {
        _t = set.ToList();
    }

    private IEnumerable<T> _t;
    public bool Create(T entity)
    {
        throw new NotImplementedException();
    }

    public T Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Select()
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }
}