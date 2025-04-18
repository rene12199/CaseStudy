using CaseStudy.Api.DTOs;
using CaseStudy.Core.Interfaces;
using CaseStudy.Core.Models;

namespace CaseStudy.Api.Converter;

public class FilterConverter : IConverter<TransactionFilterDto, TransactionFilter>
{
    public TransactionFilter Convert(TransactionFilterDto source)
    {
        if(source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        
        return new TransactionFilter
        {
            CategoryId = source.CategoryId,
            CreatedAfter = source.CreatedAfter,
            CreatedBefore = source.CreatedBefore
        };
    }

    public TransactionFilterDto ReverseConvert(TransactionFilter destination)
    {
        throw new NotImplementedException();
    }
}