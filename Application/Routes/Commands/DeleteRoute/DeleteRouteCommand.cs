using MediatR;

namespace Application.Routes.Commands.DeleteRoute
{
    public class DeleteRouteCommand : IRequest
    {
        public int Id { get; set; }
    }
}