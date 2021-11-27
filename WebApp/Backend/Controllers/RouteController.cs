using Application.Routes.Commands;
using Application.Routes.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Routes.Commands.CreateRoute;
using Microsoft.AspNetCore.Http;

namespace WebApp.Backend.Controllers
{
    public class RouteController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllRoutesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetRouteByIdQuery() { Id = id }));
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByParameters(string startingStationName,
            string finalStationName, DateTime departureTime)
        {
            return Ok(await Mediator.Send(new GetRoutesByParametersQuery
            {
                StartingStation = startingStationName,
                FinalStation = finalStationName,
                DepartureTime = departureTime
            }));
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}