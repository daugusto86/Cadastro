using Cadastro.Core.DomainObjects;
using System.Net;

namespace Cadastro.Api.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //log exception

            var result = new
            {
                sucesso = false,
                erros = "Erro interno de Servidor"
            };

            if (exception is DomainException)
            {
                result = new
                {
                    sucesso = false,
                    erros = exception?.Message
                };
            }
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.WriteAsJsonAsync(result);
        }
    }
}
