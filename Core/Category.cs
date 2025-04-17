using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core;

[BsonIgnoreExtraElements]
public class Category
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CategoryId { get; set; }

    public string Name { get; set; }
}