using CaseStudy.Core.Models;

namespace CaseStudy.Core.Interfaces;

public interface ITransactionService
{
    void CreateTransaction(Transaction transaction);
    IEnumerable<Transaction> FilterTransactions(TransactionFilter transactionFilter);
}