using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using MongoDB.Driver;

namespace CaseStudy.Application.Repositorys;

public class TransactionRepository : ITransactionRepository
{
    public TransactionRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("case_study");
        Transactions = database.GetCollection<Transaction>("transactions");
    }

    private IMongoCollection<Transaction> Transactions { get; }

    public void CreateTransaction(Transaction transaction)
    {
        Transactions.InsertOne(transaction);
    }
}