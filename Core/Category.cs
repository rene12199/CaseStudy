using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core;

public class Category
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public ExpenseType Type { get; set; }
}