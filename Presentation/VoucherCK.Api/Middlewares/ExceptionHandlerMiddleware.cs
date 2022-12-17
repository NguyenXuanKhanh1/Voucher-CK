using VoucherCK.Api.ExceptionHandler;
using VoucherCK.Application;
using VoucherCK.SharedKernel.Exceptions;

namespace VoucherCK.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private IExceptionHandler _responseExceptionHandler;
        private IExceptionHandler _defaultExceptionHandler;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _responseExceptionHandler = Activator.CreateInstance(typeof(ResponseExceptionHandler)) as IExceptionHandler;
            _defaultExceptionHandler = Activator.CreateInstance(typeof(DefaultExceptionHandler)) as IExceptionHandler;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var exceptionType = exception.GetType().Name;

            if (exceptionType.Equals(nameof(ResponseException)))
            {
                await _responseExceptionHandler.Handle(context, exception);
            }
            else
            {
                await _defaultExceptionHandler.Handle(context, exception);
            }
        }
    }
}
