namespace MeetingRoomBooking.Middleware
{
    public class ExceptionMiddleware
    {
            private readonly RequestDelegate _next;

            public ExceptionMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            // THIS METHOD MUST EXIST AND BE PUBLIC
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";

                var statusCode = exception switch
                {
                    AppException appEx => appEx.StatusCode,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;

                var response = new
                {
                    message = exception.Message,
                    statusCode = statusCode
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }