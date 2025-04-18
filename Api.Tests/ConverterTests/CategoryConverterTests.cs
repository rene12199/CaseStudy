using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Moq;
using NUnit.Framework;

namespace Api.Tests.ConverterTests;

[TestFixture]
public class CategoryConverterTests
{
    private ICategoryConverter _categoryConverter;

    [SetUp]
    public void Setup()
    {
        _categoryConverter = new CategoryConverter();
    }

    [Test]
    public void Convert_CategoryDto_ValidInput_ShouldReturnCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryName = "Test Category";
        var dto = new CategoryDto
        {
            CategoryId = categoryId,
            Name = categoryName
        };

        // Act
        var result = _categoryConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.Name, Is.EqualTo(categoryName));
        });
    }

    [Test]
    public void Convert_Category_ValidInput_ShouldReturnCategoryDto()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var categoryName = "Test Category";
        var category = new Category
        {
            CategoryId = categoryId,
            Name = categoryName
        };

        // Act
        var result = _categoryConverter.Convert(category);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.Name, Is.EqualTo(categoryName));
        });
    }

    [Test]
    public void Convert_CategoryDto_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        CategoryDto dto = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _categoryConverter.Convert(dto));
        Assert.That(exception.ParamName, Is.EqualTo("source"));
    }

    [Test]
    public void Convert_Category_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        Category category = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _categoryConverter.Convert(category));
        Assert.That(exception.ParamName, Is.EqualTo("source"));
    }

    [Test]
    public void Convert_CategoryDto_EmptyGuid_ShouldReturnCategoryWithEmptyGuid()
    {
        // Arrange
        var dto = new CategoryDto
        {
            CategoryId = Guid.Empty,
            Name = "Test Category"
        };

        // Act
        var result = _categoryConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(Guid.Empty));
            Assert.That(result.Name, Is.EqualTo(dto.Name));
        });
    }

    [Test]
    public void Convert_CategoryDto_EmptyName_ShouldReturnCategoryWithEmptyName()
    {
        // Arrange
        var dto = new CategoryDto
        {
            CategoryId = Guid.NewGuid(),
            Name = string.Empty
        };

        // Act
        var result = _categoryConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(dto.CategoryId));
            Assert.That(result.Name, Is.EqualTo(string.Empty));
        });
    }
}
