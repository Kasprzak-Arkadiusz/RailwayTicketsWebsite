using System.Net.Http;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Application.Common.DTOs;

namespace WebApp.Frontend.Pages.Routes
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RouteDto Route { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Routes.Add(Route);
            await _context.SaveChangesAsync();
            */
            return RedirectToPage("./Index");
        }
    }
}