using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core.Models;

[BsonIgnoreExtraElements]
public class User
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public required Guid UserId { get; set; }

    public string Name { get; set; }

    public int MonthlyBudget { get; set; }
}