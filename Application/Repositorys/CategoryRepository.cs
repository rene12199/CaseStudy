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

    public Category? GetCategoryById(Guid sourceCategoryId)
    {
        return Categories.FindSync(f => f.CategoryId == sourceCategoryId).SingleOrDefault();
    }

    public void UpdateCategory(Category category)
    {

        if(!TryGetCategoryById(category.CategoryId, out _))
        {
            //todo do Exception
        }

        var update = Builders<Category>
            .Update.Set(f => f, category);
        Categories.UpdateOne(c => c.CategoryId == category.CategoryId, update);
    }

    public IList<Category> GetAllCategories()
    {
        return Categories.FindSync(f => true).ToList();
    }

    public void DeleteCategory(Guid categoryId)
    {
        Categories.DeleteOne(c => c.CategoryId == categoryId);
    }

    public bool TryGetCategoryById(Guid sourceCategoryId, out Category? category)
    {
        category = GetCategoryById(sourceCategoryId);
        return category != null;
    }

    public void CreateCategory(Category category)
    {
        if(TryGetCategoryById(category.CategoryId, out _))
        {
            //todo do Exception
        }

        Categories.InsertOne(category);
    }
}