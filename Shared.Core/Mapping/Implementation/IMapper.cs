﻿namespace Shared.Core.Mapping.Implementation;

public interface IMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    List<TDestination> MapList<TSource, TDestination>(List<TSource> source);
}
