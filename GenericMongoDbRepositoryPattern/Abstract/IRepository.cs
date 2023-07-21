using System.Linq.Expressions;

namespace GenericMongoDbRepositoryPattern.Abstract;

public interface IRepository<T>
{
    Task<bool> ExistsByIdAsync(string id, string type = "object");
    IQueryable<T> GetAllQueryable();
    Task<IQueryable<T>> GetAllQueryableAsync();
    IList<T> GetAll();
    Task<IList<T>> GetAllAsync();
    IList<T> FilterBy(Expression<Func<T, bool>> filter);
    Task<IList<T>> FilterByAsync(Expression<Func<T, bool>> filter);
    T GetById(string id, string type = "object");
    Task<T> GetByIdAsync(string id, string type = "object");
    void InsertOne(T entity);
    Task InsertOneAsync(T entity);
    void InsertMany(ICollection<T> entities);
    Task InsertManyAsync(ICollection<T> entities);
    T UpdateOne(T entity, string id, string type = "object");
    Task<T> UpdateOneAsync(T entity, string id, string type = "object");
    T DeleteOne(Expression<Func<T, bool>> filter);
    Task<T> DeleteOneAsync(Expression<Func<T, bool>> filter);
    T DeleteById(string id);
    Task<T> DeleteByIdAsync(string id);
    void DeleteMany(Expression<Func<T, bool>> filter);
    Task DeleteManyAsync(Expression<Func<T, bool>> filter);
}
