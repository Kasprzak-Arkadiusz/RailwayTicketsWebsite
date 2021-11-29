using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;

namespace WebApp.Frontend.Pages.Routes
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public RouteDto Route { get; set; }

        [BindProperty]
        public IList<SelectListItem> StartingStations { get; set; }

        [BindProperty]
        public IList<SelectListItem> FinalStations { get; set; }

        [BindProperty]
        public IList<SelectListItem> TrainIds { get; set; }

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

            TrainIds = DropdownFiller.FillTrainIdsDropdown(trains);
            StartingStations = DropdownFiller.FillStationsDropdown(stations);
            FinalStations = new List<SelectListItem>(StartingStations);

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            //TODO is this needed?
            Route.StartingStation = Request.Form["From"];
            Route.FinalStation = Request.Form["To"];
            Route.TrainId = short.Parse(Request.Form["TrainId"]);
            Route.IsSuspended = bool.Parse(Request.Form["IsSuspended"]);

            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "Route/";

            var json = JsonConvert.SerializeObject(Route);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PostAsync(actionPath, content);

            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToPage("/Routes/FindRoutes");
        }
    }
}