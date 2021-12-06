using Application.Routes.Commands.CreateRoute;
using Application.Routes.Commands.DeleteRoute;
using Application.Routes.Commands.UpdateRoute;
using Application.Routes.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Backend.Controllers
{
    public class RouteController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllRoutesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRouteByIdQuery() { Id = id }, cancellationToken);

            if (result is null)
                return NotFound("Requested route couldn't be found.");

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByParameters(string startingStationName,
            string finalStationName, DateTime departureTime, bool suspended, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetRoutesByParametersQuery
            {
                StartingStation = startingStationName,
                FinalStation = finalStationName,
                DepartureTime = departureTime,
                Suspended = suspended
            }, cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteRouteCommand() { Id = id }));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}