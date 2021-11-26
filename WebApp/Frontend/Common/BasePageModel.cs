using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace WebApp.Frontend.Common
{
    public abstract class BasePageModel : PageModel
    {
        private IHttpClientFactory _httpClientFactory;

        protected IHttpClientFactory HttpClientFactory =>
            _httpClientFactory ??= HttpContext.RequestServices.GetService<IHttpClientFactory>();
    }
}