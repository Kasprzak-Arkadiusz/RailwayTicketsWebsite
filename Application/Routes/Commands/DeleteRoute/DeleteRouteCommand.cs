using Application.Abstractions.Messaging;
using MediatR;

namespace Application.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommand : ICommand<Unit>
    {
        public int Id { get; set; }
    }
}