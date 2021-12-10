using Application.Routes.Commands.CreateRoute;
using Application.Routes.Commands.DeleteRoute;
using Application.Routes.Commands.UpdateRoute;
using Application.Routes.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Frontend.ViewModels;

namespace WebApp.Backend.Controllers
{
    public class RouteController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetAllRoutesQuery(), cancellationToken);

            var routeViewModel = Mapper.Map<IEnumerable<RouteViewModel>>(result);

            return Ok(routeViewModel);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRouteByIdQuery() { Id = id }, cancellationToken);

            if (result is null)
                return NotFound("Requested route couldn't be found.");

            var routeViewModel = Mapper.Map<RouteViewModel>(result);

            return Ok(routeViewModel);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByParameters(string startingStationName,
            string finalStationName, DateTime departureTime, bool suspended, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRoutesByParametersQuery
            {
                StartingStation = startingStationName,
                FinalStation = finalStationName,
                DepartureTime = departureTime,
                Suspended = suspended
            }, cancellationToken);

            var routeViewModels =  Mapper.Map<IEnumerable<RouteViewModel>>(result);

            return Ok(routeViewModels);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteRouteCommand { Id = id });

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            await Mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}