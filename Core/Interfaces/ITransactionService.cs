using CaseStudy.Core.Models;

namespace CaseStudy.Core.Interfaces;

public interface ITransactionService
{
    public void CreateTransaction(Transaction transaction);
}