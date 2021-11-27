﻿using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;

namespace WebApp.Frontend.Pages.Routes
{
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
            Route.IsOnHold = bool.Parse(Request.Form["IsOnHold"]);

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Route/{Route.Id}";

            var json = JsonConvert.SerializeObject(Route);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PutAsync(actionPath, content);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return Page();

            return RedirectToPage("/FindRoutes");
        }
    }
}