using Application.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using WebApp.Frontend.Common;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.Routes
{
    [Authorize]
    public class FindRoutesModel : BasePageModel
    {
        [BindProperty]
        public FindRoutesViewModel Input { get; init; }

        public IList<RouteViewModel> Routes { get; private set; } = new List<RouteViewModel>();

        public bool WasFiltered { get; set; }

        public async Task OnGetAsync()
        {
            var client = HttpClientFactory.CreateClient("api");

            const string actionPath = "Route";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await httpResponseMessage.Content.ReadFromJsonAsync<IList<RouteViewModel>>();
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
            query["suspended"] = Input.Suspended.ToString();
            query["departureTime"] = Input.DepartureTime == null
                ? DateTime.MinValue.ToString(CultureInfo.CurrentCulture)
                : Input.DepartureTime.Value.ToString("HH:mm");

            uriBuilder.Query = query.ToString() ?? string.Empty;
            var url = uriBuilder.ToString();

            var httpResponseMessage = await client.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Routes = await httpResponseMessage.Content.ReadFromJsonAsync<IList<RouteViewModel>>();
            }

            WasFiltered = true;
        }
    }
}