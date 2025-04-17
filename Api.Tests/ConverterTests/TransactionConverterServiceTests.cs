using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using Moq;

namespace Api.Tests.ConverterTests;

public class TransactionConverterServiceTests
{
    private TransactionConverter _transactionConverter;
    private Mock<ICategoryRepository> _transactionRepository;

    [SetUp]
    public void Setup()
    {
        _transactionRepository = new Mock<ICategoryRepository>();
        _transactionConverter = new TransactionConverter(_transactionRepository.Object);
    }

    [Test]
    public void ConvertDtoToDomainObject_ShouldReturnDomainObject()
    {
        var categoryGuid = Guid.NewGuid();
        var transaction = new TransactionDto
        {
            Amount = 100,
            CategoryId = categoryGuid,
            TransactionTime = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        
        _transactionRepository.Setup(x => x.GetCategoryById(categoryGuid)).Returns(new Category()
        {
            CategoryId = categoryGuid,
            Name = "Test",
            Type = ExpenseType.Income
        });

        var domainObject = _transactionConverter.Convert(transaction);

        _transactionRepository.Verify(x => x.GetCategoryById(categoryGuid), Times.Once);
        Assert.That(domainObject.Amount, Is.EqualTo(transaction.Amount));
        Assert.That(domainObject.Category.CategoryId, Is.EqualTo(transaction.CategoryId));
        Assert.That(domainObject.TransactionTime, Is.EqualTo(transaction.TransactionTime));
        Assert.That(domainObject.User.UserId, Is.EqualTo(transaction.UserId));
    }

    [Test]
    public void ConvertDtoToDomainObject_WithNull_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => _transactionConverter.Convert(null));
    }

    //todo Implement test when new Feature is implemented
    // [Test]
    // public void ConvertDtoToDomainObject_WithUnkownUserId_ShouldThrowException()
    // {
    //     Assert.Throws<ArgumentNullException>(() => _transactionConverter.Convert(null));
    // }
}