using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using MongoDB.Driver;

namespace CaseStudy.Application.Repositorys;

public class CategoryRepository : ICategoryRepository
{
    public CategoryRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("case_study");
        Categories = database.GetCollection<Category>("categories");
    }


    private IMongoCollection<Category> Categories { get; }


    public Category GetCategoryById(Guid sourceCategoryId)
    {
        return Categories.FindSync(f => f.CategoryId == sourceCategoryId).SingleOrDefault();
    }
}