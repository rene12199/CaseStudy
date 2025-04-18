using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Moq;

namespace Api.Tests.ConverterTests;

[TestFixture]
public class TransactionConverterTests
{
    private ITransactionConverter _transactionConverter;
    private Mock<ICategoryRepository> _categoryRepository;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = new Mock<ICategoryRepository>();
        _transactionConverter = new TransactionConverter(_categoryRepository.Object);
    }

    [Test]
    public void Convert_CreateTransactionDto_ValidInput_ShouldReturnTransaction()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var transactionTime = DateTime.Now;
        var amount = 100.50;
        
        var dto = new CreateTransactionDto
        {
            Amount = amount,
            CategoryId = categoryId,
            TransactionTime = transactionTime,
            UserId = userId,
            Type = ExpenseType.Income
        };

        var expectedCategory = new Category
        {
            CategoryId = categoryId,
            Name = "Test Category"
        };

        _categoryRepository.Setup(x => x.GetCategoryById(categoryId))
            .Returns(expectedCategory);

        // Act
        var result = _transactionConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(amount));
            Assert.That(result.Category, Is.EqualTo(expectedCategory));
            Assert.That(result.TransactionTime, Is.EqualTo(transactionTime));
            Assert.That(result.User.UserId, Is.EqualTo(userId));
            Assert.That(result.User.Name, Is.EqualTo("Test1")); // Hardcoded value in converter
            Assert.That(result.Type, Is.EqualTo(ExpenseType.Income));
        });
        _categoryRepository.Verify(x => x.GetCategoryById(categoryId), Times.Once);
    }

    [Test]
    public void Convert_CreateTransactionDto_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        CreateTransactionDto dto = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _transactionConverter.Convert(dto));
        Assert.That(exception.ParamName, Is.EqualTo("source"));
    }

    [Test]
    public void Convert_CreateTransactionDto_NonExistentCategory_ShouldReturnTransactionWithNullCategory()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var amount = 100.50;
        var transactionTime = DateTime.Now;
        
        var dto = new CreateTransactionDto
        {
            Amount = amount,
            CategoryId = categoryId,
            TransactionTime = transactionTime,
            UserId = Guid.NewGuid(),
            Type = ExpenseType.Income
        };

        _categoryRepository.Setup(x => x.GetCategoryById(categoryId))
            .Returns((Category)null);

        // Act
        var result = _transactionConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(amount));
            Assert.That(result.Category, Is.Null);
            Assert.That(result.TransactionTime, Is.EqualTo(transactionTime));
            Assert.That(result.Type, Is.EqualTo(ExpenseType.Income));
        });
        _categoryRepository.Verify(x => x.GetCategoryById(categoryId), Times.Once);
    }

    [Test]
    public void Convert_Transaction_ValidInput_ShouldReturnTransactionViewDto()
    {
        // Arrange
        var amount = 100.50;
        var transactionTime = DateTime.Now;
        var categoryName = "Test Category";
        
        var transaction = new Transaction
        {
            Amount = amount,
            Category = new Category { Name = categoryName },
            TransactionTime = transactionTime,
            Type = ExpenseType.Income
        };

        // Act
        var result = _transactionConverter.Convert(transaction);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(amount));
            Assert.That(result.Category, Is.EqualTo(categoryName));
            Assert.That(result.TransactionTime, Is.EqualTo(transactionTime));
            Assert.That(result.Type, Is.EqualTo(ExpenseType.Income));
        });
    }

    [Test]
    public void Convert_Transaction_NullCategory_ShouldReturnDtoWithNullCategory()
    {
        // Arrange
        var amount = 100.50;
        var transactionTime = DateTime.Now;
        
        var transaction = new Transaction
        {
            Amount = amount,
            Category = null,
            TransactionTime = transactionTime,
            Type = ExpenseType.Expense
        };

        // Act
        var result = _transactionConverter.Convert(transaction);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(amount));
            Assert.That(result.Category, Is.Null);
            Assert.That(result.TransactionTime, Is.EqualTo(transactionTime));
            Assert.That(result.Type, Is.EqualTo(ExpenseType.Expense));
        });
    }

    [Test]
    public void Convert_CreateTransactionDto_ExpenseType_ShouldReturnTransactionWithExpenseType()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var amount = 50.25;
        
        var dto = new CreateTransactionDto
        {
            Amount = amount,
            CategoryId = categoryId,
            TransactionTime = DateTime.Now,
            UserId = Guid.NewGuid(),
            Type = ExpenseType.Expense
        };

        var category = new Category
        {
            CategoryId = categoryId,
            Name = "Test Category"
        };

        _categoryRepository.Setup(x => x.GetCategoryById(categoryId))
            .Returns(category);

        // Act
        var result = _transactionConverter.Convert(dto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Amount, Is.EqualTo(amount));
            Assert.That(result.Type, Is.EqualTo(ExpenseType.Expense));
            Assert.That(result.Category, Is.EqualTo(category));
        });
    }
}