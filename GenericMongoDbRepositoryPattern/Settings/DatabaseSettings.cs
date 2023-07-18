using GenericMongoDbRepositoryPattern.Abstract;

namespace GenericMongoDbRepositoryPattern.Settings;

public class DatabaseSettings : IDatabaseSettings
{
    public string ConnectionStrings { get; set; }
    public string DatabaseName { get; set; }
}
