using Application.Common.DTOs;
using AutoMapper;
using WebApp.Frontend.ViewModels;

namespace WebApp.Backend.Common.Mapping
{
    public class BackendProfile : Profile
    {
        public BackendProfile()
        {
            CreateMap<TicketDto, ReturningTicketViewModel>();
            CreateMap<TicketDto, TicketViewModel>();
            CreateMap<ReturnedTicketDto, ReturnedTicketViewModel>()
                .ForMember(r => r.TrainIdentifier, opt => opt.MapFrom(dto => dto.TrainId));
            CreateMap<RouteDto, RouteViewModel>();
        }
    }
}