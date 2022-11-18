using MsSensediaTemplate.Application.Exceptions;
using MsSensediaTemplate.Domain.Models;
using Newtonsoft.Json;
using System.Net;

namespace MsSensediaTemplate.API.Helpers
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate next;
        ILogger<ApiExceptionMiddleware> _logger;

        public ApiExceptionMiddleware(ILogger<ApiExceptionMiddleware> logger, RequestDelegate next)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ApiExceptionMiddleware> logger)
        {
            var code = HttpStatusCode.InternalServerError;

            if (exception is DomainException)
            {
                //string json = JsonConvert.SerializeObject(domainException.Message);
                //context.Result = new BadRequestObjectResult(json);
                code = HttpStatusCode.BadRequest;

            }
            else if (exception is ServiceException)
            {
                code = HttpStatusCode.BadRequest;
            }
            //else if (exception is InfrastructureException)
            //{
            //    code = HttpStatusCode.BadRequest;
            //}
            //else  (exception is Exception)
            //{
            //    code = HttpStatusCode.NotFound;
            //}
            else 
            {
                code = HttpStatusCode.NotFound;
            }
            // else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            // else if (exception is MyException)             code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (code == HttpStatusCode.InternalServerError)
                logger.LogError(exception.Message, exception);
            else if (code == HttpStatusCode.BadRequest)
                logger.LogInformation(exception.Message, exception);
            else
                logger.LogWarning(exception.Message, exception);

            return context.Response.WriteAsync(result);
        }
    }
}
