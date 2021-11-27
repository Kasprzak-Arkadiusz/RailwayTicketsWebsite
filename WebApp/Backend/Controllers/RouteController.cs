using Application.Routes.Commands;
using Application.Routes.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApp.Backend.Controllers
{
    public class RouteController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRouteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteRouteCommand() { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRouteCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }
    }
}