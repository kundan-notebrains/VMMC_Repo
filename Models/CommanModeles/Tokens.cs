using System.Net;

namespace VMMC.Models.CommanModeles
{
    public class Tokens
    {
        public string? Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
        public string? name { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
