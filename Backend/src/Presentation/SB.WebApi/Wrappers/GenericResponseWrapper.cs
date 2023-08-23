using Newtonsoft.Json;
using System.Net;

namespace SB.WebApi.Wrappers
{
    public class GenericResponseWrapper
    {
        public string EmissionDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessStatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
