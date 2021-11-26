using System;
using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.Routes
{
    public class CreateModel : BasePageModel
    {
        [BindProperty]
        public RouteDto Route { get; set; }

        [BindProperty]
        public IList<SelectListItem> StartingStations { get; set; }

        [BindProperty]
        public IList<SelectListItem> DepartureStations { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string actionPath = "Station";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return Page();

            var stations = await client.GetFromJsonAsync<IList<StationDto>>(actionPath);
            FillStationsDropdown(stations);

            return Page();
        }

        private void FillStationsDropdown(IEnumerable<StationDto> stations)
        {
            StartingStations = stations?.Select(i =>
                new SelectListItem
                {
                    Value = i.Name,
                    Text = i.Name
                }).ToList();

            if (StartingStations == null)
            {
                throw new ArgumentNullException($"List {nameof(StartingStations)} is empty.");
            }

            DepartureStations = new List<SelectListItem>(StartingStations);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Route.StartingStation = Request.Form["From"];
            Route.FinalStation = Request.Form["To"];

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