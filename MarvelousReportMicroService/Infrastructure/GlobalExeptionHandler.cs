
using System.Net;
using System.Text.Json;

namespace MarvelousReportMicroService.API.Infrastructure
{
    public class GlobalExeptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExeptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch
            {
                await _next(context);
            }
            

        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, string message)
        {
            var result = JsonSerializer.Serialize(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(result);
        }
    }
}
