using MongoDB.Bson;
using MongoDB.Driver;

namespace CaseStudy.Api;

public static class MongoDbUtil
{
    public static void AddMongoDbClient(this IServiceCollection serviceCollection, MongoClientSettings connectionString)
    {
        var mongoClient = new MongoClient("mongodb://root:Test1@localhost:27017/");
        var pingResult = mongoClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        if(pingResult == null)
        {
            throw new Exception("Unable to Connect to Db");
        }

        serviceCollection.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient("mongodb://root:Test1@localhost:27017/"));
        
    }
}