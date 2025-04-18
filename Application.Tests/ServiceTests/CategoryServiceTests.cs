using CaseStudy.Application.Services;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Moq;

namespace CaseStudy.Application.Tests.ServiceTests;

[TestFixture]
public class CategoryServiceTests
{
    [SetUp]
    public void Setup()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _categoryService = new CategoryService(_categoryRepositoryMock.Object);
    }

    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private CategoryService _categoryService;

    [Test]
    public void CreateCategory_ValidCategory_ShouldCallRepositoryUpdateMethod()
    {
        // Arrange
        var category = new Category {CategoryId = Guid.NewGuid(), Name = "Test Category"};

        // Act
        _categoryService.CreateCategory(category);

        // Assert
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void CreateCategory_NullCategory_ShouldThrowArgumentNullException()
    {
        // Arrange
        Category category = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryService.CreateCategory(category));
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(It.IsAny<Category>()), Times.Never);
    }
    
    [Test]
    public void UpdateCategory_ValidCategory_ShouldCallRepositoryUpdateMethod()
    {
        // Arrange
        var category = new Category {CategoryId = Guid.NewGuid(), Name = "Test Category"};

        // Act
        _categoryService.UpdateCategory(category);

        // Assert
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void UpdateCategory_NullCategory_ShouldThrowArgumentNullException()
    {
        // Arrange
        Category category = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryService.UpdateCategory(category));
        _categoryRepositoryMock.Verify(x => x.UpdateCategory(It.IsAny<Category>()), Times.Never);
    }

    [Test]
    public void GetAllCategories_HasCategories_ShouldReturnCategoriesFromRepository()
    {
        // Arrange
        var expectedCategories = new List<Category>
        {
            new() {CategoryId = Guid.NewGuid(), Name = "Category 1"},
            new() {CategoryId = Guid.NewGuid(), Name = "Category 2"}
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
    public void GetAllCategories_NoCategories_ShouldReturnEmptyList()
    {
        // Arrange
        var emptyList = new List<Category>();
        _categoryRepositoryMock.Setup(x => x.GetAllCategories()).Returns(emptyList);

        // Act
        var result = _categoryService.GetAllCategories();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
        _categoryRepositoryMock.Verify(x => x.GetAllCategories(), Times.Once);
    }

  
}