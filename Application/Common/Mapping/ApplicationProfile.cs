using Application.Common.DTOs;
using Application.Routes.Commands.UpdateRoute;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Station, StationDto>();
            CreateMap<Train, TrainDto>();
            CreateMap<Seat, SeatDto>();
            CreateMap<Route, RouteDto>()
                .ForMember(dest => dest.StartingStation, opt => opt.MapFrom(src => src.StartingStation.Name))
                .ForMember(dest => dest.FinalStation, opt => opt.MapFrom(src => src.FinalStation.Name))
                .ForMember(dest => dest.TrainId, opt => opt.MapFrom(src => src.Train.TrainId));
            CreateMap<ReturnedTicket, ReturnedTicketDto>();

            CreateMap<RouteDto, UpdateRouteCommand>();
        }
    }
}