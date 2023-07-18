using GenericMongoDbRepositoryPattern.Abstract;
using MongoDB.Driver;

namespace GenericMongoDbRepositoryPattern.Context;

public class MongoDbContext
{
    #region Fields
    private readonly IMongoDatabase _mongoDatabase;
    #endregion
    #region Ctor
    public MongoDbContext(IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionStrings);
        _mongoDatabase = client.GetDatabase(settings.DatabaseName);
    }
    #endregion
    #region methods
    public IMongoCollection<T> GetCollection<T>()
    {
        return _mongoDatabase.GetCollection<T>(typeof(T).Name.Trim());
    }
    public IMongoDatabase GetDatabase()
    {
        return _mongoDatabase;
    }
    #endregion
}
