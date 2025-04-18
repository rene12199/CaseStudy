using CaseStudy.Core.Models;

namespace CaseStudy.Core.Interfaces;

public interface ICategoryRepository
{
    Category GetCategoryById(Guid sourceCategoryId);
    void UpdateCategory(Category category);
    IList<Category> GetAllCategories();
    void DeleteCategory(Guid transactionId);
    bool TryGetCategoryById(Guid sourceCategoryId, out Category category);
}