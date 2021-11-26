using System.Threading.Tasks;
using Application.Trains.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Backend.Controllers
{
    public class TrainController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllTrainsQuery()));
        }
    }
}