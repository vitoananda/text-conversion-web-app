using CodingTest.Service.Services;
using Service.Repositories;

namespace Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IConversionService, ConversionService>();
        return services;
    }
}
