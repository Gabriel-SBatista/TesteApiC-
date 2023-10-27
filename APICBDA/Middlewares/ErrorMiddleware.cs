using APICBDA.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace APICBDA.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorMiddleware(RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorResponse errorResponse = new ErrorResponse(HttpStatusCode.InternalServerError.ToString(), "Ocorreu um problema ao tratar sua solitação");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
