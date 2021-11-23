using System.Reflection;
using Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var mapper = AutoMapperConfig.RegisterMappings(services);
            services.AddSingleton(mapper);
            return services;
        }
    }
}