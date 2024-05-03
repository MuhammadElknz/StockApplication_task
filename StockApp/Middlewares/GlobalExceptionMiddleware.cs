using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockApp.Application.Common.Exceptions;
using System.Net;

namespace StockApp.WebApi.Middlewares
{
    public class GlobalExceptionMiddleware 
    {
 
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next, IHostEnvironment env)
        {
         
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                var errorMessage = string.Empty;

                errorMessage = ex.Message;

                if(ex is BadRequestException )
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                   
                    
                }
                else if(ex is NotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    _logger.LogError(ex.Message);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorMessage = _env.IsDevelopment() ? ex.Message : "Internal Server Error";
                }

                var errorResponse = new
                {
                    Success = false,
                    message = errorMessage
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}
