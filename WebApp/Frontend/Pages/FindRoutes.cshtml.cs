using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Frontend.Pages
{
    public class FindRoutesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FindRoutesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; init; }

        public IEnumerable<RouteDto> Routes { get; private set; } = new List<RouteDto>();

        public class InputModel
        {
            [Required]
            public string From { get; init; }

            [Required]
            public string To { get; init; }

            [DataType(DataType.Time)]
            [DisplayFormat(DataFormatString = "{0:HH:mm}")]
            public DateTime DepartureTime { get; init; }
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("api");

            var httpResponseMessage = await client.GetAsync("Route");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await client.GetFromJsonAsync<IEnumerable<RouteDto>>("Route");
            }
        }

        private bool SearchParamsAreEmpty()
        {
            return Input is null ||
                   Input.From is null &&
                   Input.To is null &&
                   Input.DepartureTime == DateTime.MinValue;
        }

        public async Task OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            var client = _httpClientFactory.CreateClient("api");
            var uriBuilder = new UriBuilder(client.BaseAddress + "Route/search")
            {
                Query = Uri.EscapeUriString(
                    $"startingStationName={Input.From}&finalStationName={Input.To}&departureTime={Input.DepartureTime:O}")
            };

            var httpResponseMessage = await client.GetAsync("Route/search");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await client.GetFromJsonAsync<IEnumerable<RouteDto>>(uriBuilder.Uri);
            }
        }
    }
}