namespace webAplication.DAL.Interfaces;

/// <summary>
/// interface of base repository of data.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseRepository<T>
{
    bool Create(T entity);

    T Get(int id);

    IEnumerable<T> Select();

    bool Delete(T entity);
    
}