using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Api.Converter;

public class CategoryConverter : ICategoryConverter
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

    public CategoryDto Convert(Category source)
    {
        return new CategoryDto{
            CategoryId = source.CategoryId,
            Name = source.Name
        };
        
    }
}

public interface ICategoryConverter : IConverter<CategoryDto, Category>, IConverter<Category, CategoryDto> { }