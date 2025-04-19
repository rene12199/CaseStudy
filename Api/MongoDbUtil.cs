using CaseStudy.Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CaseStudy.Api;

public static class MongoDbUtil
{
    public static void AddMongoDbClient(this IServiceCollection serviceCollection,
        MongoClientSettings connectionSettings)
    {
        var mongoClient = new MongoClient(connectionSettings);
        Console.WriteLine($"Connected to MongoDb: {connectionSettings.Server.Host}:{connectionSettings.Server.Port}");
        var pingResult = mongoClient.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        if(pingResult == null)
        {
            throw new Exception("Unable to Connect to Db");
        }

        serviceCollection.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(connectionSettings));

    }

    public static void CreateBaseData(MongoClientSettings settings)
    {
        var mongoClient = new MongoClient(settings);

        var categories = mongoClient.GetDatabase("case_study").GetCollection<Category>("categories");
        categories.DeleteMany(f => f.CategoryId != Guid.Empty);
        categories.InsertOne(new Category
        {
            CategoryId = Guid.Parse("DA0E7E1C-0CF7-444B-86E9-3113E07EB277"),
            Name = "Food"
        });

        categories.InsertOne(new Category
        {
            CategoryId = Guid.Parse("B2E825C6-B9D7-4504-AB0E-F21FF5055265"),
            Name = "Rent"
        });

        categories.InsertOne(new Category
        {
            CategoryId = Guid.Parse("4C34C939-6C9B-412C-BA71-AC0929268D69"),
            Name = "Work"
        });

        var users = mongoClient.GetDatabase("case_study").GetCollection<User>("users");
        users.DeleteMany(f => f.UserId != Guid.Empty);

        users.InsertOne(new User
        {

            Name = "Test1",
            UserId = Guid.Parse("62D2FAF5-1D79-403B-AD95-4417D425EBEB"),
            MonthlyBudget = 2500

        });


    }
}