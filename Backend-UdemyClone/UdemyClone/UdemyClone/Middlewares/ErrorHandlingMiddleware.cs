using System.Net;
using System.Text.Json;

namespace UdemyClone.WebApi.Middlewares
{
    public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await next(context);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";


            var (statusCode, mensaje) = ex switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                InvalidOperationException => (HttpStatusCode.Conflict, ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "No autorizado"),
                _ => (HttpStatusCode.InternalServerError, "Ocurrió un error interno en el servidor")
            };

            context.Response.StatusCode = (int)statusCode;

            var respuesta = new
            {
                StatusCode = (int)statusCode,
                Mensaje = mensaje,

                Detalle = ex.Message
            };

            var json = JsonSerializer.Serialize(respuesta, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }

}
