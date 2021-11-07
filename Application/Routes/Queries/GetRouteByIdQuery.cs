using System.Linq;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Routes.Queries
{
    public class GetRouteByIdQuery : IRequest<Route>
    {
        public int Id { get; set; }
    }

    public class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, Route>
    {
        private readonly IApplicationDbContext _context;

        public GetRouteByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Route> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Routes
                .Where(r => r.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}