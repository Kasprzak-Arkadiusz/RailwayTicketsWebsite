using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper RegisterMappings(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
            });
            return config.CreateMapper();
        }
    }
}