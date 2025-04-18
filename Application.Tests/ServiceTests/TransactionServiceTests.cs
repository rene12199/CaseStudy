using CaseStudy.Application.Services;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using MongoDB.Driver;
using Moq;

namespace CaseStudy.Application.Tests.ServiceTests;

[TestFixture]
public class TransactionServiceTests
{
    private Mock<ITransactionRepository> _transactionRepository;
    private ITransactionService _transactionService;
    private Transaction _sampleTransaction;
    private Guid _categoryId;
    private Guid _userId;

    [SetUp]
    public void Setup()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_transactionRepository.Object);

        _categoryId = Guid.NewGuid();
        _userId = Guid.NewGuid();
        _sampleTransaction = new Transaction
        {
            Amount = 100,
            Category = new Category
            {
                CategoryId = _categoryId,
                Name = "Test Category"
            },
            TransactionTime = DateTime.Now,
            User = new User
            {
                Name = "Test User",
                UserId = _userId
            },
            Type = ExpenseType.Expense
        };
    }

    [Test]
    public void CreateTransaction_WithValidTransaction_ShouldCreateTransaction()
    {
        // Act
        _transactionService.CreateTransaction(_sampleTransaction);

        // Assert
        _transactionRepository.Verify(x => x.CreateTransaction(_sampleTransaction), Times.Once);
    }

    [Test]
    public void CreateTransaction_WithNullTransaction_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _transactionService.CreateTransaction(null));
        _transactionRepository.Verify(x => x.CreateTransaction(It.IsAny<Transaction>()), Times.Never);
    }

    [Test]
    public void FilterTransactions_WithNoFilters_ShouldReturnAllTransactions()
    {
        // Arrange
        var filter = new TransactionFilter();
        var expectedTransactions = new List<Transaction> { _sampleTransaction };
        _transactionRepository.Setup(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()))
            .Returns(expectedTransactions);

        // Act
        var result = _transactionService.FilterTransactions(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTransactions));
        _transactionRepository.Verify(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()), Times.Once);
    }

    [Test]
    public void FilterTransactions_WithDateRange_ShouldApplyDateFilters()
    {
        // Arrange
        var createdAfter = DateTime.Now.AddDays(-7);
        var createdBefore = DateTime.Now;
        var filter = new TransactionFilter
        {
            CreatedAfter = createdAfter,
            CreatedBefore = createdBefore
        };

        var expectedTransactions = new List<Transaction> 
        { 
            new Transaction 
            { 
                TransactionTime = DateTime.Now.AddDays(-5) 
            } 
        };

        _transactionRepository.Setup(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()))
            .Returns(expectedTransactions);

        // Act
        var result = _transactionService.FilterTransactions(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTransactions));
        _transactionRepository.Verify(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()), Times.Once);
    }

    [Test]
    public void FilterTransactions_WithCategoryId_ShouldApplyCategoryFilter()
    {
        // Arrange
        var filter = new TransactionFilter
        {
            CategoryId = _categoryId
        };

        var expectedTransactions = new List<Transaction> { _sampleTransaction };
        _transactionRepository.Setup(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()))
            .Returns(expectedTransactions);

        // Act
        var result = _transactionService.FilterTransactions(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTransactions));
        _transactionRepository.Verify(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()), Times.Once);
    }

    [Test]
    public void FilterTransactions_WithAllFilters_ShouldApplyAllFilters()
    {
        // Arrange
        var filter = new TransactionFilter
        {
            CategoryId = _categoryId,
            CreatedAfter = DateTime.Now.AddDays(-7),
            CreatedBefore = DateTime.Now
        };

        var expectedTransactions = new List<Transaction> { _sampleTransaction };
        _transactionRepository.Setup(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()))
            .Returns(expectedTransactions);

        // Act
        var result = _transactionService.FilterTransactions(filter);

        // Assert
        Assert.That(result, Is.EqualTo(expectedTransactions));
        _transactionRepository.Verify(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()), Times.Once);
    }
    
    [Test]
    public void FilterTransactions_WithInvalidDateRange_ShouldReturnEmptyList()
    {
        // Arrange
        var filter = new TransactionFilter
        {
            CreatedAfter = DateTime.Now,
            CreatedBefore = DateTime.Now.AddDays(-7) // Before is earlier than After
        };

        var expectedTransactions = new List<Transaction>();
        _transactionRepository.Setup(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()))
            .Returns(expectedTransactions);

        // Act
        var result = _transactionService.FilterTransactions(filter);

        // Assert
        Assert.That(result, Is.Empty);
        _transactionRepository.Verify(x => x.ApplyFilters(It.IsAny<FilterDefinition<Transaction>>()), Times.Once);
    }
}