using System.Net;
using System.Security;
using CaseStudy.Application.Repository;
using CaseStudy.Application.Services;
using CaseStudy.Core.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace CaseStudy.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionOptions =  builder.Configuration.GetSection(ConnectionOptions.Position)
                                       .Get<ConnectionOptions>();     
        
        var mongoDbClientSettings = new MongoClientSettings
        {
            Scheme = ConnectionStringScheme.MongoDB,
            Server = new MongoServerAddress(connectionOptions!.Host, connectionOptions!.Port),
            Credential = MongoCredential.CreateCredential(connectionOptions.Database, connectionOptions.Username, connectionOptions.Password),
        };

        try
        {
            builder.Services.AddMongoDbClient(mongoDbClientSettings);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return;
        }
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();

        var app = builder.Build();
        app.UseRouting();
#pragma warning disable ASP0014
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
#pragma warning restore ASP0014
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.Run();
    }
}