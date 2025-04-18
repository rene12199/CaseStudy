using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core.Models;

[BsonIgnoreExtraElements]
public class Transaction
{
    public DateTime TransactionTime { get; set; }

    [BsonElement("category_id")] public Category? Category { get; set; }

    [BsonElement("user_id")] public User User { get; set; }

    public double Amount { get; set; }

    public ExpenseType Type { get; set; }
}