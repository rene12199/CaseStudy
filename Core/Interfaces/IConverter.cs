namespace CaseStudy.Core.Interfaces;

public interface IConverter<TSource, TDestination> where TSource : class where TDestination : class
{
     TDestination Convert(TSource source);
}