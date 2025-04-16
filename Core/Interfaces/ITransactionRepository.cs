using CaseStudy.Core.Models;

namespace CaseStudy.Core.Interfaces;

public interface ITransactionRepository
{
    public void CreateTransaction(Transaction transaction);
}