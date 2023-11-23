using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace CryptoTracker_backend.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            try
            {
                await _next(context);
            }catch(Exception error)
            {
                Console.WriteLine(error.Message, error);

                var result =  JsonSerializer.Serialize(new
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An internal server error has occurred"
                });

                await response.WriteAsync(result);

            }

        }
    }
}
