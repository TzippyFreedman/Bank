using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Data.Exceptions;
using UserService.Services.Exceptions;

namespace UserService.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private async static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Log.Error(ex, "Exception caught in ErrorHandlingMiddleware");

            HttpStatusCode code;
            string message;

            switch (ex)
            {
                case DataNotFoundException dataNotFoundException:
                    code = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;
                case IncorrectPasswordException incorrectPasswordException:
                    code = HttpStatusCode.Unauthorized;
                    message = ex.Message;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    message = "something went wrong";
                    break;

            }

            /*            if (ex is DataNotFoundException)
                        {
                            code = HttpStatusCode.NotFound;
                            message = ex.Message;
                        }
                        if (ex is BadRequestException)
                        {
                            code = HttpStatusCode.BadRequest;
                            message = ex.Message;
                        }
                        if (ex is IncorrectPasswordException)
                        {
                            code = HttpStatusCode.Unauthorized;
                            message = ex.Message;
                        }*/

            string result = JsonSerializer
            .Serialize(new { errorMessage = message, statusCode = code });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }

    }
    public static class ErrorHandlingMiddlewareExtentions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

