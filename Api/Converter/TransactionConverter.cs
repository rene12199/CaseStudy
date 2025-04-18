using CaseStudy.Api.DTOs;
using CaseStudy.Core;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Api.Converter;

public class TransactionConverter : ITransactionConverter

{
    private readonly ICategoryRepository _transactionRepository;

    public TransactionConverter(ICategoryRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;

    }

    public Transaction Convert(CreateTransactionDto source)
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
            User = user,
            Type = source.Type
        };
    }

    public TransactionViewDto Convert(Transaction source)
    {
        return new TransactionViewDto
        {
            Amount = source.Amount,
            Category = source.Category?.Name,
            TransactionTime = source.TransactionTime,
            Type = source.Type

        };
    }

    public TransactionSummaryDto Convert(IEnumerable<Transaction> source)
    {

        var transactions = source.ToList();
        var income = transactions.Where(t => t.Type == ExpenseType.Income).Sum(c => c.Amount);
        var expense = transactions.Where(t => t.Type == ExpenseType.Expense).Sum(c => c.Amount);
        return new TransactionSummaryDto
        {
            Balance = income - expense,
            Category = transactions.FirstOrDefault()?.Category?.Name,
            Incomes = income,
            Expenses = expense
        };
    }
}

public interface ITransactionConverter :
    IConverter<CreateTransactionDto, Transaction>,
    IConverter<Transaction, TransactionViewDto>,
    IConverter<IEnumerable<Transaction>, TransactionSummaryDto>
{
}