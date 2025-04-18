using CaseStudy.Core;

namespace CaseStudy.Api.DTOs;

public class CreateTransactionDto
{
    public DateTime TransactionTime { get; set; }

    public Guid CategoryId { get; set; }

    public Guid UserId { get; set; }

    public double Amount { get; set; }

    public ExpenseType Type { get; set; }
}