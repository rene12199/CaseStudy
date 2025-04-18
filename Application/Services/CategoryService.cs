using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

    }

    public void CreateCategory(Category category)
    {
        _categoryRepository.UpdateCategory(category);
    }

    public void UpdateCategory(Category category)
    {
        _categoryRepository.UpdateCategory(category);
    }

    public IList<Category> GetAllCategories()
    {
        return _categoryRepository.GetAllCategories();
    }

    public void DeleteCategory(Guid transactionId)
    {
        _categoryRepository.DeleteCategory(transactionId);
    }
}