using CaseStudy.Core.Interfaces;

namespace CaseStudy.Core.Models;

public class TransactionFilter 
{
    public DateTime? CreatedAfter { get; set; }
    public DateTime? CreatedBefore { get; set; }
    public Guid? CategoryId { get; set; }
}