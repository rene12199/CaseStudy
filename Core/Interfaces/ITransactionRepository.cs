using CaseStudy.Core.Models;
using MongoDB.Driver;

namespace CaseStudy.Core.Interfaces;

public interface ITransactionRepository
{
    public void CreateTransaction(Transaction transaction);
    IEnumerable<Transaction> ApplyFilters(FilterDefinition<Transaction> transactionFilter);
}