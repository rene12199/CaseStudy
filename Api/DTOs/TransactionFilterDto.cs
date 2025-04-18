namespace CaseStudy.Api.DTOs;

public class TransactionFilterDto
{
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public Guid? CategoryId { get; set; }
}