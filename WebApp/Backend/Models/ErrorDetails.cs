using System.Collections.Generic;
using System.Text.Json;

namespace WebApp.Backend.Models
{
    public class ErrorDetails
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Details { get; set; }
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
