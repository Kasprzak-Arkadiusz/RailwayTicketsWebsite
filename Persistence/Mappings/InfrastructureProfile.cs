using Application.Common.Models;
using AutoMapper;
using Infrastructure.Identity;

namespace Infrastructure.Mappings
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<ApplicationUserParams, ApplicationUser>();
        }
    }
}