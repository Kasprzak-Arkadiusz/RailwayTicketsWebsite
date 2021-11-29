using Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Frontend.Pages.Tickets
{
    public class BuyTicketModel : PageModel
    {
        [BindProperty]
        public TicketDto Ticket { get; set; }

        public void OnGet(int id)
        {
        }
    }
}