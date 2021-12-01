using Application.Common.DTOs;
using MediatR;
using System.Collections.Generic;

namespace Application.Tickets.Queries
{
    public class GetUserTicketsQuery : IRequest<IEnumerable<TicketDto>>
    {
        public string UserId { get; set; }
    }
}