using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace ClubeAss.Domain.Commands
{
    public class BaseResponse
    {
        public BaseResponse(HttpStatusCode statusCode, object content = null, IEnumerable<string> message = null)
        {
            StatusCode = statusCode;
            Content = content;
            Message = message;
        }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        public object Content { get; set; }
        public IEnumerable<string> Message { get; set; }
    }
}
