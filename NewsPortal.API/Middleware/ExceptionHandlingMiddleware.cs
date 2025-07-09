using FluentValidation;

namespace NewsPortal.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                switch(e)
                {
                    case ValidationException validationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                        await context.Response.WriteAsJsonAsync(new { Errors = errors });
                        return;
                    case EntityDoesNotExistException:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        break;
                    case ArticleAlreadyPublishedException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    case UniqueConstraintViolationException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                await context.Response.WriteAsJsonAsync(new { Error = e.Message });
            }
        }
    }
}
