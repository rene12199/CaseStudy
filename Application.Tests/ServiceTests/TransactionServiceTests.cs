using CaseStudy.Application.Services;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using Moq;

namespace CaseStudy.Application.Tests.ServiceTests;

public class TransactionServiceTests
{
    private Mock<ITransactionRepository> _transactionRepository;
    private ITransactionService _transactionService;

    [SetUp]
    public void Setup()
    {
        _transactionRepository = new Mock<ITransactionRepository>();
        _transactionService = new TransactionService(_transactionRepository.Object);
    }

    [Test]
    public void CreateTransaction_WithValidTransaction_ShouldCreateTransaction()
    {
        var transaction = new Transaction
        {
            Amount = 100,
            Category = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = "Test",
                Type = ExpenseType.Income
            },
            TransactionTime = DateTime.Now,
            User = new User
            {
                Name = "Test1",
                UserId = Guid.NewGuid()
            }
        };

        _transactionService.CreateTransaction(transaction);
        _transactionRepository.Verify(x => x.CreateTransaction(transaction), Times.Once);
    }

    [Test]
    public void CreateTransaction_WithOutUserId_ThrowNoUserException()
    {
        var transaction = new Transaction
        {
            Amount = 100,
            Category = new Category(),
            TransactionTime = DateTime.Now,
            User = new User
            {
                Name = "Test1",
                UserId = Guid.NewGuid()
            }
        };

        _transactionService.CreateTransaction(transaction);
        _transactionRepository.Verify(x => x.CreateTransaction(transaction), Times.Once);
    }
}