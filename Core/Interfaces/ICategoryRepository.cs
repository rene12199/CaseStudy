namespace CaseStudy.Core.Interfaces;

public interface ICategoryRepository
{
    Category GetCategoryById(Guid sourceCategoryId);
}