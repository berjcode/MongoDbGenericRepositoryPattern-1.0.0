namespace GenericMongoDbRepositoryPattern.Abstract;

public interface IDatabaseSettings
{
    public string ConnectionStrings { get; set; }
    public string DatabaseName { get; set; }
}
