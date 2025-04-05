using AutoMapper.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        => services.AddSingleton<IMapper, Mapper>();
}