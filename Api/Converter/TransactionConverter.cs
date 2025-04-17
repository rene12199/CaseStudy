using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Api.Converter;

public class TransactionConverter : IConverter<TransactionDto, Transaction>
{
    private readonly ICategoryRepository _transactionRepository;

    public TransactionConverter(ICategoryRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;

    }

    public Transaction Convert(TransactionDto source)
    {
        if(source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        //todo resolve User
        var user = new User
        {
            Name = "Test1",
            UserId = source.UserId
        };

        var category = _transactionRepository.GetCategoryById(source.CategoryId);

        return new Transaction
        {
            Amount = source.Amount,
            Category = category,
            TransactionTime = source.TransactionTime,
            User = user
        };
    }

    public TransactionDto ReverseConvert(Transaction categories)
    {
        throw new NotImplementedException();
    }
}