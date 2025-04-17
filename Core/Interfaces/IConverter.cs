namespace CaseStudy.Core.Interfaces;

public interface IConverter<TSource, TDestination>
{
    public TDestination Convert(TSource source);
}