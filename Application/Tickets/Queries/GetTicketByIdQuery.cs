using Application.Abstractions.Messaging;
using Application.Common.DTOs;

namespace Application.Tickets.Queries
{
    public class GetTicketByIdQuery : IQuery<TicketDto>
    {
        public int Id { get; set; }
    }
}