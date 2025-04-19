using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;
using MongoDB.Driver;

namespace CaseStudy.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public void CreateTransaction(Transaction transaction)
    {
        if(transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        _transactionRepository.CreateTransaction(transaction);
    }


    public IEnumerable<Transaction> FilterTransactions(TransactionFilter transactionFilter)
    {
        var filterList = ConvertFilterToQuery(transactionFilter);

        return _transactionRepository.ApplyFilters(filterList);
    }

    private FilterDefinition<Transaction> ConvertFilterToQuery(TransactionFilter transactionFilterModel)
    {
        var transactionFilterBuilder = Builders<Transaction>.Filter;
        var transactionFilter = transactionFilterBuilder.Empty;

        if(transactionFilterModel.CreatedBefore.HasValue)
        {
            transactionFilter &=
                transactionFilterBuilder.Where(t => t.TransactionTime.Date <= transactionFilterModel.CreatedBefore.Value.Date);
        }

        if(transactionFilterModel.CreatedAfter.HasValue)
        {
            transactionFilter &=
                transactionFilterBuilder.Where(t => t.TransactionTime.Date >= transactionFilterModel.CreatedAfter.Value.Date);
        }

        if(transactionFilterModel.CategoryId.HasValue)
        {
            transactionFilter &= transactionFilterBuilder.Where(t =>
                t.Category != null && t.Category.CategoryId == transactionFilterModel.CategoryId);
        }

        return transactionFilter;
    }
}