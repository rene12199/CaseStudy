using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Api.Converter;

public class CategoryConverter : ICategoryConverter
{
    public Category Convert(CategoryDto source)
    {
        if(source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        
        return new Category
        {
            CategoryId = source.CategoryId,
            Name = source.Name
        };
    }

    public CategoryDto Convert(Category source)
    {
        if(source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        
        return new CategoryDto
        {
            CategoryId = source.CategoryId,
            Name = source.Name
        };

    }
}

public interface ICategoryConverter : IConverter<CategoryDto, Category>, IConverter<Category, CategoryDto>
{
}