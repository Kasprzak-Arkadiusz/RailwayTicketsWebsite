using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.Routes
{
    public class FindRoutesModel : BasePageModel
    {
        [BindProperty]
        public InputModel Input { get; init; }

        public IList<RouteDto> Routes { get; private set; } = new List<RouteDto>();

        public bool WasFiltered { get; set; }

        public class InputModel
        {
            public string From { get; init; }

            public string To { get; init; }

            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}")]
            [Display(Name = "Departure time")]
            public DateTime DepartureTime { get; init; }
        }

        public async Task OnGetAsync()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string actionPath = "Route";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await httpResponseMessage.Content.ReadFromJsonAsync<IList<RouteDto>>();
            }
        }

        public async Task OnPost(bool showAll)
        {
            if (!ModelState.IsValid || showAll)
            {
                await OnGetAsync();
                return;
            }

            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "Route/search";
            var uriBuilder = new UriBuilder(client.BaseAddress + actionPath);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["startingStationName"] = Input.From;
            query["finalStationName"] = Input.To;
            query["departureTime"] = Input.DepartureTime.ToString("HH:mm");
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var url = uriBuilder.ToString();

            var httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await httpResponseMessage.Content.ReadFromJsonAsync<IList<RouteDto>>();
            }

            WasFiltered = true;
        }
    }
}