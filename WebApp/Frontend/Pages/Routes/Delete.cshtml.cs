using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApp.Frontend.Pages.Routes
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RouteDto Route { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient("api");
            var actionPath = $"Route/{id}";
            var httpResponseMessage = await client.GetAsync(actionPath);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Route = await client.GetFromJsonAsync<RouteDto>(actionPath);
            }

            if (Route == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            Route = await _context.Routes.FindAsync(id);

            if (Route != null)
            {
                _context.Routes.Remove(Route);
                await _context.SaveChangesAsync();
            }
            */
            return RedirectToPage("./Index");
        }
    }
}