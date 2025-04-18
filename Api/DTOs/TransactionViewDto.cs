using CaseStudy.Core;

namespace CaseStudy.Api.DTOs;

public class TransactionViewDto
{
    public DateTime TransactionTime { get; set; }

    public string? Category { get; set; }
    
    public double Amount { get; set; }

    public ExpenseType Type { get; set; }
}