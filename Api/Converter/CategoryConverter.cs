using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;

namespace CaseStudy.Api.Converter;

public class CategoryConverter : IConverter<CategoryDto, Category>
{
    private readonly ICategoryRepository _transactionRepository;

    public CategoryConverter(ICategoryRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;

    }

    public Category Convert(CategoryDto source)
    {
        return new Category
        {
            CategoryId = source.CategoryId,
            Name = source.Name
        };
    }

    public CategoryDto ReverseConvert(Category categories)
    {
        return new CategoryDto{
            CategoryId = categories.CategoryId,
            Name = categories.Name
        };
    }
}