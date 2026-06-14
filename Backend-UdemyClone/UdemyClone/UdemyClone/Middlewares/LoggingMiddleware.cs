namespace UdemyClone.WebApi.Middlewares
{
    public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {

            logger.LogInformation("➡️  Petición entrante: {Method} {Path}",
                context.Request.Method,
                context.Request.Path);

            var inicio = DateTime.UtcNow;

            await next(context);

            var duracion = DateTime.UtcNow - inicio;

            logger.LogInformation("⬅️  Respuesta enviada: {StatusCode} en {Duracion}ms",
                context.Response.StatusCode,
                duracion.TotalMilliseconds);
        }
    }

}
