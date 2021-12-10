using Application.Abstractions.Messaging;
using Application.Common.DTOs;
using System.Collections.Generic;

namespace Application.Tickets.Queries
{
    public class GetUserTicketsQuery : IQuery<IEnumerable<TicketDto>>
    {
        public string UserId { get; set; }
    }
}