using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings.Profiles
{
    public class RouteToRouteDtoProfile : Profile
    {
        public RouteToRouteDtoProfile()
        {
            CreateMap<Route, RouteDto>()
                .ForMember(dto => dto.FinalStation, conf => conf.MapFrom(r => r.FinalStationNavigation.Name))
                .ForMember(dto => dto.StartingStation, conf => conf.MapFrom(r => r.StartingStationNavigation.Name))
                .ForMember(dto => dto.ArrivalTime, conf => conf.MapFrom(r => r.ArrivalTimeInMinutesPastMidnight))
                .ForMember(dto => dto.DepartureTime, conf => conf.MapFrom(r => r.DepartureTimeInMinutesPastMidnight))
                .ForMember(dto => dto.IsOnHold, conf => conf.MapFrom(r => r.IsOnHold));
        }
    }
}