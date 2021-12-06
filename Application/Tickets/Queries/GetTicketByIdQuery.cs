using Application.Common.DTOs;
using MediatR;

namespace Application.Tickets.Queries
{
    public class GetTicketByIdQuery : IRequest<TicketDto>
    {
        public int Id { get; set; }
    }
}