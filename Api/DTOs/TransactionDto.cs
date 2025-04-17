namespace CaseStudy.Api.DTOs;

public class TransactionDto
{
    public DateTime TransactionTime { get; set; }

    public Guid CategoryId { get; set; }

    public Guid UserId { get; set; }

    public int Amount { get; set; }
}