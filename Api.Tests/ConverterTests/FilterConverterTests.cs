using CaseStudy.Api.Converter;
using CaseStudy.Api.DTOs;
using CaseStudy.Core.Models;
using NUnit.Framework;

namespace Api.Tests.ConverterTests;

[TestFixture]
public class FilterConverterTests
{
    private FilterConverter _filterConverter;

    [SetUp]
    public void Setup()
    {
        _filterConverter = new FilterConverter();
    }

    [Test]
    public void Convert_ValidInput_ShouldReturnTransactionFilter()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var createdAfter = DateTime.Now.AddDays(-7);
        var createdBefore = DateTime.Now;

        var filterDto = new TransactionFilterDto
        {
            CategoryId = categoryId,
            CreatedAfter = createdAfter,
            CreatedBefore = createdBefore
        };

        // Act
        var result = _filterConverter.Convert(filterDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.CreatedAfter, Is.EqualTo(createdAfter));
            Assert.That(result.CreatedBefore, Is.EqualTo(createdBefore));
        });
    }

    [Test]
    public void Convert_NullInput_ShouldThrowArgumentNullException()
    {
        // Arrange
        TransactionFilterDto filterDto = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _filterConverter.Convert(filterDto));
        Assert.That(exception.ParamName, Is.EqualTo("source"));
    }

    [Test]
    public void Convert_NullDates_ShouldReturnFilterWithNullDates()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var filterDto = new TransactionFilterDto
        {
            CategoryId = categoryId,
            CreatedAfter = null,
            CreatedBefore = null
        };

        // Act
        var result = _filterConverter.Convert(filterDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.CreatedAfter, Is.Null);
            Assert.That(result.CreatedBefore, Is.Null);
        });
    }

    [Test]
    public void Convert_EmptyGuid_ShouldReturnFilterWithEmptyGuid()
    {
        // Arrange
        var createdAfter = DateTime.Now.AddDays(-7);
        var createdBefore = DateTime.Now;
        var filterDto = new TransactionFilterDto
        {
            CategoryId = Guid.Empty,
            CreatedAfter = createdAfter,
            CreatedBefore = createdBefore
        };

        // Act
        var result = _filterConverter.Convert(filterDto);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.CategoryId, Is.EqualTo(Guid.Empty));
            Assert.That(result.CreatedAfter, Is.EqualTo(createdAfter));
            Assert.That(result.CreatedBefore, Is.EqualTo(createdBefore));
        });
    }

    [Test]
    public void ReverseConvert_ShouldThrowNotImplementedException()
    {
        // Arrange
        var filter = new TransactionFilter();

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => _filterConverter.ReverseConvert(filter));
    }
}
