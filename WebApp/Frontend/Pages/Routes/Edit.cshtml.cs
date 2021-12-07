using Application.Common.DTOs;
using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApp.Backend.Middleware.Authorization;
using WebApp.Backend.Models;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;

namespace WebApp.Frontend.Pages.Routes
{
    [AuthorizeByRole(Role.Employee, Role.Admin)]
    public class EditModel : BasePageModel
    {
        [BindProperty]
        public RouteDto Route { get; set; }

        [BindProperty]
        public IList<SelectListItem> StartingStations { get; set; }

        [BindProperty]
        public IList<SelectListItem> FinalStations { get; set; }

        [BindProperty]
        public IList<SelectListItem> TrainIds { get; set; }

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

            Route = await client.GetFromJsonAsync<RouteDto>(routePath);

            const string stationsPath = "Station";
            var stationResponseMessage = await client.GetAsync(stationsPath);

            const string trainsPath = "Train";
            var trainResponseMessage = await client.GetAsync(trainsPath);

            if (!stationResponseMessage.IsSuccessStatusCode || !trainResponseMessage.IsSuccessStatusCode)
                return Page();

            var stations = await stationResponseMessage.Content.ReadFromJsonAsync<IList<StationDto>>();
            var trains = await trainResponseMessage.Content.ReadFromJsonAsync<IList<TrainDto>>();

            TrainIds = DropdownFiller.FillTrainIdsDropdown(trains);
            StartingStations = DropdownFiller.FillStationsDropdown(stations);
            FinalStations = DropdownFiller.FillStationsDropdown(stations);

            var selectedTrain = TrainIds.First(i => i.Value == Route.TrainId.ToString());
            selectedTrain.Selected = true;

            var selectedFrom = StartingStations.First(i => i.Value == Route.StartingStation);
            selectedFrom.Selected = true;

            var selectedTo = FinalStations.First(i => i.Value == Route.FinalStation);
            selectedTo.Selected = true;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Route.StartingStation = Request.Form["From"];
            Route.FinalStation = Request.Form["To"];
            Route.TrainId = short.Parse(Request.Form["TrainId"]);
            var test = Request.Form["Route.IsSuspended"];
            Route.IsSuspended = bool.Parse(test);

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Route/{Route.Id}";

            var json = JsonConvert.SerializeObject(Route);
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