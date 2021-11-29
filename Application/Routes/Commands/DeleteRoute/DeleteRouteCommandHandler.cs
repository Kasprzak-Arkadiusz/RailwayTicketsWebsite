using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteRouteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Routes
                .Include(r => r.StartingStation)
                .Include(r => r.FinalStation)
                .Include(r => r.Train)
                .Where(r => r.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException(nameof(Route), request.Id);
            }

            if (!entity.IsOnHold)
            {
                throw new InvalidOperationException("A route cannot be deleted unless it's on hold.");
            }

            if (entity.Train is not null)
            {
                throw new InvalidOperationException("A route cannot be deleted if there are trains assigned to it." +
                                                    " First, mark the route as suspended and unassign the trains to the route.");
            }

            _context.Routes.Remove(entity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}