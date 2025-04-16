using CaseStudy.Core;
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
    
    public void CreateTransaction()
    {
        _transactionRepository.CreateTransaction(new Transaction()
        {
            Category = Category.Food,
            Amount = 100,
            TransactionTime = DateTime.Now,
            User = new User()
            {
                Name = "Test1"
            }
        });
    }
}