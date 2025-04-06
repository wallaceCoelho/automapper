using AutoMapper.Interfaces;
using AutoMapper.Mappers;

namespace AutoMapper;

public class Mapper : IMapper
{
    private readonly MappingHandler _handler = new();
    
    public void Map<TSource, TDestination>(TSource source, TDestination destination)
        => _handler.Map(source, destination);

    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        => _handler.Map<TSource, TDestination>(source);

    public List<TDestination> Map<TSource, TDestination>(List<TSource> sourceList) where TDestination : new()
        => _handler.Map<TSource, TDestination>(sourceList);

    public IEnumerable<IEnumerable<TDestination>> Map<TSource, TDestination>(IEnumerable<IEnumerable<TSource>> sourceList) where TDestination : new()
        => _handler.Map<TSource, TDestination>(sourceList);
}