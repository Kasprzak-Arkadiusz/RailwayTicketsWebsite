using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Backend.Middleware.ExceptionHandling;
using WebApp.Frontend.Common;
using WebApp.Frontend.Utils;
using WebApp.Frontend.ViewModels;

namespace WebApp.Frontend.Pages.Routes
{
    [AuthorizeByRole(Role.Employee)]
    public class DeleteModel : BasePageModel
    {
        [BindProperty]
        public RouteViewModel Route { get; set; }

        public IList<string> Errors { get; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Route/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
                Route = await httpResponseMessage.Content.ReadFromJsonAsync<RouteViewModel>();

            if (Route == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var client = HttpClientFactory.CreateClient("api");
            var actionPath = $"Route/{Route.Id}";

            var httpResponseMessage = await client.DeleteAsync(actionPath);
            if (httpResponseMessage.IsSuccessStatusCode)
                return RedirectToPage("/FindRoutes");

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