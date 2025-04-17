using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

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
        _transactionRepository.CreateTransaction(transaction);
    }
}