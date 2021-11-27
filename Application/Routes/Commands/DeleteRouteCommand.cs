using System;
using System.Linq;
using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Routes.Commands
{
    public class DeleteRouteCommand : IRequest
    {
        public int Id { get; set; }
    }

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

            _context.Routes.Remove(entity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}