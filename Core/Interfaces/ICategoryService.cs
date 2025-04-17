namespace CaseStudy.Core.Interfaces;

public interface ICategoryService
{
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    IList<Category> GetAllCategories();
    void DeleteCategory(Guid transactionId);
}