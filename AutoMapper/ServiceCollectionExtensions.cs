using AutoMapper.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        => services.AddScoped<IMapper, Mapper>();
    public static IServiceCollection AddAutoMapperSingleton(this IServiceCollection services)
        => services.AddSingleton<IMapper, Mapper>();
    public static IServiceCollection AddAutoMapperTransient(this IServiceCollection services)
        => services.AddTransient<IMapper, Mapper>();
}