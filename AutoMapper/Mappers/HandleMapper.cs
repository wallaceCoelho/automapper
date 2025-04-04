namespace AutoMapper.Mappers;

public class HandleMapper : BaseMapper
{
    public List<TDestination> Map<TSource, TDestination>(List<TSource> sourceList) where TDestination : new()
        => sourceList.Select(Map<TSource, TDestination>).ToList();
    
    public IEnumerable<IEnumerable<TDestination>> Map<TSource, TDestination>(IEnumerable<IEnumerable<TSource>> sourceList) where TDestination : new()
        => sourceList.Select(subList => 
            subList.Select(source => Map<TSource, TDestination>(source!)));
    
    public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        TDestination destination = new();
        Map(source, destination);
        return destination;
    }

    public void Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        if (source == null || destination == null) return;

        var key = (typeof(TSource), typeof(TDestination));
        var mapFunc = MapCache.GetOrAdd(key, _ => BuildMapAction(typeof(TSource), typeof(TDestination)));
        mapFunc(source, destination);
    }
}
