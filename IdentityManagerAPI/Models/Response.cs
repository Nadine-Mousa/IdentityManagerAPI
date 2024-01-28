using System.ComponentModel.DataAnnotations;

namespace IdentityManagerAPI.Models
{
    public class Response
    {

        
        [MaxLength(30)]
        public string Status { get; set; }
        public string Message { get; set; }
        public Response() { }
        public Response(string status)
        {
            Status = status;
        }
        public Response(string status, string message) : this(status)
        {
            Message = message;
        }
        
    }
}
