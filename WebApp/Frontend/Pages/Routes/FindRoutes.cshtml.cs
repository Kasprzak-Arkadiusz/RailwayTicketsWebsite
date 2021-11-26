using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Frontend.Common;

namespace WebApp.Frontend.Pages.Routes
{
    public class FindRoutesModel : BasePageModel
    {
        [BindProperty]
        public InputModel Input { get; init; }

        public IEnumerable<RouteDto> Routes { get; private set; } = new List<RouteDto>();

        public class InputModel
        {
            [Required]
            public string From { get; init; }

            [Required]
            public string To { get; init; }

            [Required]
            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}")]
            public DateTime DepartureTime { get; init; }
        }

        public async Task OnGetAsync()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string actionPath = "Route";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await client.GetFromJsonAsync<IEnumerable<RouteDto>>(actionPath);
            }
        }

        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            var client = HttpClientFactory.CreateClient("api");
            const string actionPath = "Route/search";
            var uriBuilder = new UriBuilder(client.BaseAddress + actionPath)
            {
                Query = Uri.EscapeUriString(
                    $"startingStationName={Input.From}&finalStationName={Input.To}&departureTime={Input.DepartureTime:O}")
            };

            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await client.GetFromJsonAsync<IEnumerable<RouteDto>>(uriBuilder.Uri);
            }
        }
    }
}