namespace AutoMapper.Interfaces;

public interface IMapper
{
    IEnumerable<IEnumerable<TDestination>> Map<TSource, TDestination>(IEnumerable<IEnumerable<TSource>> sourceList)
        where TDestination : new();
    List<TDestination> Map<TSource, TDestination>(List<TSource> sourceList) where TDestination : new();
    TDestination Map<TSource, TDestination>(TSource source) where TDestination : new();
    void Map<TSource, TDestination>(TSource source, TDestination destination);
}