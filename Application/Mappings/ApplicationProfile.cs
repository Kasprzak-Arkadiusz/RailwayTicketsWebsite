using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Station, StationDto>();
        }
    }
}