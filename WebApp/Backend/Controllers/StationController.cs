using Application.Stations.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApp.Backend.Controllers
{
    public class StationController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllStationsQuery()));
        }
    }
}