using Org.BouncyCastle.Bcpg;
using System.Net;

namespace CryptoTracker_backend.DTOs
{
    public class ApiErrorDTO
    {
        public string Message { get; set; } = string.Empty;

        public string ErrorType { get; set; } = string.Empty;

        public HttpStatusCode StatusCode = HttpStatusCode.BadRequest;

        public ApiErrorDTO() { }

        public ApiErrorDTO(string message, string errorType, HttpStatusCode statusCode)
        {
            Message = message;
            ErrorType = errorType;
            StatusCode = statusCode;
        }
    }
}
