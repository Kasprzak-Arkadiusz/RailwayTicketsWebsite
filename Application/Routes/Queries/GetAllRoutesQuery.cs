using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Queries
{
    public class GetAllRoutesQuery : IRequest<IEnumerable<Route>> { }

    public class GetAllRoutesQueryHandler : IRequestHandler<GetAllRoutesQuery, IEnumerable<Route>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRoutesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Route>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = await _context.Routes.ToListAsync(cancellationToken);
            return routes?.AsReadOnly();
        }
    }
}