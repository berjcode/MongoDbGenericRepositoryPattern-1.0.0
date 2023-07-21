using GenericMongoDbRepositoryPattern.Abstract;
using GenericMongoDbRepositoryPattern.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace GenericMongoDbRepositoryPattern.Concrete;

public class MongoDbGenericRepository<T> : IRepository<T>
{
    #region Fields
    private readonly MongoDbContext _dbContext;
    private readonly IMongoCollection<T> _collection;
    #endregion

    #region Ctor
    public MongoDbGenericRepository(IDatabaseSettings databaseSettings)
    {
        _dbContext = new MongoDbContext(databaseSettings);
        _collection = _dbContext.GetCollection<T>();
    }
    #endregion

    #region methods
    public IQueryable<T> GetAllQueryable()
    {
        var data = _collection.AsQueryable();

        return data;
    }
    public async Task<IQueryable<T>> GetAllQueryableAsync()
    {
        var data = _collection.AsQueryable();

        return data;
    }
    public T DeleteById(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var data = _collection.FindOneAndDelete(filter);

        return data;
    }
    public async Task<T> DeleteByIdAsync(string id)
    {
        var objectId = ObjectId.Parse(id);
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var data = await _collection.FindOneAndDeleteAsync(filter);

        return data;
    }
    public void DeleteMany(Expression<Func<T, bool>> filter)
    {
        _collection.DeleteMany(filter);
    }

    public async Task DeleteManyAsync(Expression<Func<T, bool>> filter)
    {
        await _collection.DeleteManyAsync(filter);
    }

    public T DeleteOne(Expression<Func<T, bool>> filter)
    {
        var deleteDocument = _collection.FindOneAndDelete(filter);

        return deleteDocument;
    }
    public async Task<T> DeleteOneAsync(Expression<Func<T, bool>> filter)
    {
        var deleteDocument = await _collection.FindOneAndDeleteAsync(filter);

        return deleteDocument;
    }
    public IList<T> FilterBy(Expression<Func<T, bool>> filter)
    {
        var data = _collection.Find(filter).ToList();

        return data;
    }
    public async Task<IList<T>> FilterByAsync(Expression<Func<T, bool>> filter)
    {
        var data = await _collection.Find(filter).ToListAsync();

        return data;
    }
    public IList<T> GetAll()
    {
        var data = _collection.AsQueryable().ToList();

        return data;
    }
    public async Task<IList<T>> GetAllAsync()
    {
        var data = await _collection.AsQueryable().ToListAsync();

        return data;
    }
    public T GetById(string id, string type = "object")
    {
        Object objectId = null;
        if (type == "guid")
        {
            objectId = Guid.Parse(id);
        }
        else
        {
            objectId = ObjectId.Parse(id);
        }
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var data = _collection.Find(filter).FirstOrDefault();

        return data;
    }
    public async Task<T> GetByIdAsync(string id, string type = "object")
    {
        Object objectId = null;
        if (type == "guid")
        {
            objectId = Guid.Parse(id);
        }
        else
        {
            objectId = ObjectId.Parse(id);
        }
        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var data = await _collection.Find(filter).FirstOrDefaultAsync();

        return data;
    }
    public void InsertMany(ICollection<T> entities)
    {
        _collection.InsertMany(entities);
    }
    public async Task InsertManyAsync(ICollection<T> entities)
    {
        await _collection.InsertManyAsync(entities);
    }
    public void InsertOne(T entity)
    {
        _collection.InsertOne(entity);

    }
    public async Task InsertOneAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
    public T UpdateOne(T entity, string id, string type = "object")
    {
        object objectId = null;
        if (type == "guid")
            objectId = Guid.Parse(id);
        else
            objectId = ObjectId.Parse(id);

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        _collection.ReplaceOne(filter, entity);

        return entity;
    }
    public async Task<T> UpdateOneAsync(T entity, string id, string type = "object")
    {
        object objectId = null;

        if (type == "guid")
            objectId = Guid.Parse(id);
        else
            objectId = ObjectId.Parse(id);

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await _collection.ReplaceOneAsync(filter, entity);

        return entity;
    }

    public async Task<bool> ExistsByIdAsync(string id, string type = "object")
    {
        object obejctId = null;
        if (type == "guid")
            obejctId = Guid.Parse(id);
        else
            obejctId = ObjectId.Parse(id);

        var filter = Builders<T>.Filter.Eq("_id", obejctId);
        var documentExists = await _collection.Find(filter).AnyAsync();
        return documentExists;
    }

    #endregion
}
