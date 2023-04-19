namespace BlogApp.IDataAccess.Repositories;

public interface IRepository<T>
{
    void Add(T entity);

    void Remove(T entity);

    void Update(T entity);

    IEnumerable<T> GetAll();
}
