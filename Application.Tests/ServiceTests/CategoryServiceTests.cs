using CaseStudy.Application.Services;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using NUnit.Framework;
using Moq;

namespace CaseStudy.Application.Tests.ServiceTests;

[TestFixture]
public class CategoryServiceTests
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private CategoryService _categoryService;

    [SetUp]
    public void Setup()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _categoryService = new CategoryService(_categoryRepositoryMock.Object);
    }

    [Test]
    public void CreateCategory_ShouldCallRepositoryUpdateMethod()
    {
        // Arrange
        var category = new Category { CategoryId = Guid.NewGuid(), Name = "Test Category" };

        // Act
        _categoryService.CreateCategory(category);

        // Assert
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void UpdateCategory_ShouldCallRepositoryUpdateMethod()
    {
        // Arrange
        var category = new Category { CategoryId = Guid.NewGuid(), Name = "Test Category" };

        // Act
        _categoryService.UpdateCategory(category);

        // Assert
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void GetAllCategories_ShouldReturnCategoriesFromRepository()
    {
        // Arrange
        var expectedCategories = new List<Category>
        {
            new() { CategoryId = Guid.NewGuid(), Name = "Category 1" },
            new() { CategoryId = Guid.NewGuid(), Name = "Category 2" }
        };
        _categoryRepositoryMock.Setup(x => x.GetAllCategories()).Returns(expectedCategories);

        // Act
        var result = _categoryService.GetAllCategories();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(expectedCategories.Count));
        CollectionAssert.AreEquivalent(expectedCategories, result);
        _categoryRepositoryMock.Verify(x => x.GetAllCategories(), Times.Once);
    }

    [Test]
    public void DeleteCategory_ShouldCallRepositoryDeleteMethod()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        _categoryService.DeleteCategory(categoryId);

        // Assert
        _categoryRepositoryMock.Verify(x => x.DeleteCategory(categoryId), Times.Once);
    }
}
