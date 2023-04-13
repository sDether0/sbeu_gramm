namespace SBEU.Gramm.Middleware.Repositories.Interfaces;

/* An interface that defines the methods that will be used in the repository. */
public interface IBaseRepository<T> : IRepository
{
    Task<T> GetById(string id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Create(T entity);
    Task<T> Update(T entity, string id, string userId);
    Task<bool> Delete(string id);
}

public interface IRepository
{

}