using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core.Models;

public class Transaction
{
    public DateTime TransactionTime { get; set; }

    public Category Category { get; set; }

    [BsonElement("user_id")] public User User { get; set; }

    public int Amount { get; set; }
}