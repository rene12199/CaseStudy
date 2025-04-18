namespace CaseStudy.Api.DTOs;

public class TransactionSummaryDto
{
    public double Balance { get; set; }
    public double Incomes { get; set; }
    public double Expenses { get; set; }
    public string? Category { get; set; }
}