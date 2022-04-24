using Akvelon.TestTask.DAL.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Akvelon.TestTask.DAL;

public class MongoDbContext
{
    private readonly IMongoDatabase _mongoDatabase;

    public MongoDbContext(IOptions<MongoDbOption> options)
    {
        var mongoClient = new MongoClient(
            options.Value.ConnectionString);

        _mongoDatabase = mongoClient.GetDatabase(
            options.Value.DatabaseName);
    }

    public async Task CreateCollectionIfNotExists(string name)
    {
        var collections = await _mongoDatabase.ListCollectionNamesAsync();
        if ((await collections.ToListAsync()).Any(x => x.Equals(name)))
        {
            return;
        }

        await _mongoDatabase.CreateCollectionAsync(name);
    }

    public async Task<IMongoCollection<T>> GetCollection<T>(string name)
    {
        return _mongoDatabase.GetCollection<T>(name);
    }
}