using CaseStudy.Core.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CaseStudy.Core.Models;

[BsonIgnoreExtraElements]
public class Category  
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid CategoryId { get; set; }

    public string Name { get; set; }
}