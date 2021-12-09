using Application.Common.DTOs;
using Application.Routes.Commands.CreateRoute;
using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApp.Backend.Middleware.ExceptionHandling;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.Routes
{
    [AuthorizeByRole(Role.Employee)]
    public class CreateModel : BasePageModel
    {
        [BindProperty] public CreateRouteViewModel Route { get; set; } = new();
        public IList<string> Errors { get; } = new List<string>();

        public async Task<IActionResult> OnGet()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string stationsPath = "Station";
            var stationResponseMessage = await client.GetAsync(stationsPath);

            const string trainsPath = "Train";
            var trainResponseMessage = await client.GetAsync(trainsPath);

            if (!stationResponseMessage.IsSuccessStatusCode || !trainResponseMessage.IsSuccessStatusCode)
                return Page();

            var stations = await stationResponseMessage.Content.ReadFromJsonAsync<IList<StationDto>>();
            var trains = await trainResponseMessage.Content.ReadFromJsonAsync<IList<TrainDto>>();

            Route.TrainIds = DropdownFiller.FillTrainIdsDropdown(trains);
            Route.StartingStations = DropdownFiller.FillStationsDropdown(stations);
            Route.FinalStations = new List<SelectListItem>(Route.StartingStations);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return await OnGet();
            }

            var command = new CreateRouteCommand
            {
                StartingStation = Route.StartingStation,
                FinalStation = Route.FinalStation,
                TrainId = Route.TrainId,
                IsSuspended = Route.IsSuspended,
                DepartureTime = Route.DepartureTime,
                ArrivalTime = Route.ArrivalTime
            };

            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "Route";

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PostAsync(actionPath, content);

            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("/Routes/FindRoutes");

            var postResponse = await httpResponseMessage.Content.ReadFromJsonAsync<ErrorDetails>();

            if (postResponse?.Errors == null)
            {
                if (postResponse != null)
                    Errors.Add(postResponse.Details);
                return await OnGet();
            }

            foreach (var (_, value) in postResponse.Errors)
                Errors.Add(string.Join("\n", value));

            return await OnGet();
        }
    }
}