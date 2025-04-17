namespace CaseStudy.Core.Interfaces;

public interface IConverter<TSource, TDestination>
{
     TDestination Convert(TSource source);
     TSource ReverseConvert(TDestination categories);
}