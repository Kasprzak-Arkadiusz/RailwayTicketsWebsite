using Application.Common.DTOs;
using Application.Routes.Commands.UpdateRoute;
using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
    [AuthorizeByRole(Role.Employee, Role.Admin)]
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public CreateRouteViewModel Route { get; set; }

        public IList<string> Errors { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");

            var routePath = $"Route/{id}";
            var routeResponseMessage = await client.GetAsync(routePath);

            if (!routeResponseMessage.IsSuccessStatusCode)
                return NotFound();

            Route = await client.GetFromJsonAsync<CreateRouteViewModel>(routePath);

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
            Route.FinalStations = DropdownFiller.FillStationsDropdown(stations);

            var selectedTrain = Route.TrainIds.First(i => i.Value == Route.TrainId.ToString());
            selectedTrain.Selected = true;

            var selectedFrom = Route.StartingStations.First(i => i.Value == Route.StartingStation);
            selectedFrom.Selected = true;

            var selectedTo = Route.FinalStations.First(i => i.Value == Route.FinalStation);
            selectedTo.Selected = true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return await OnGetAsync(id);

            var command = new UpdateRouteCommand
            {
                StartingStation = Route.StartingStation,
                FinalStation = Route.FinalStation,
                TrainId = Route.TrainId,
                DepartureTime = Route.DepartureTime,
                ArrivalTime = Route.ArrivalTime,
                IsSuspended = Route.IsSuspended,
                Id = Route.Id
            };

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Route/{Route.Id}";

            var json = JsonConvert.SerializeObject(command);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PutAsync(actionPath, content);

            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("./FindRoutes");

            var postResponse = await httpResponseMessage.Content.ReadFromJsonAsync<ErrorDetails>();

            if (postResponse?.Errors == null)
            {
                if (postResponse != null)
                    Errors.Add(postResponse.Details);
                return await OnGetAsync(Route.Id);
            }

            foreach (var (_, value) in postResponse.Errors)
                Errors.Add(string.Join("\n", value));

            return await OnGetAsync(Route.Id);
        }
    }
}