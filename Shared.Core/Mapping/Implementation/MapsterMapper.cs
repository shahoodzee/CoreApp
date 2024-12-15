using Mapster;

namespace Shared.Core.Mapping.Implementation;

public class MapsterMapper : IMapper
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        if (source == null) throw new ArgumentNullException("source");
        return source.Adapt<TDestination>();
    }

    public List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
    {

        if (source == null) throw new ArgumentNullException("source");
        return source.Adapt<List<TDestination>>();
    }
}
