using VoucherCK.Application;
using VoucherCK.SharedKernel.Exceptions;
using VoucherCK.SharedKernel.Responses;
using VoucherCK.Utility;

namespace VoucherCK.Api.ExceptionHandler
{
    public class ResponseExceptionHandler : IExceptionHandler
    {
        public async Task Handle(HttpContext context, Exception exception)
        {
            var resException = (ResponseException)exception;
            context.Response.StatusCode = resException.GetStatus();
            var response = Response<ResponseException>.FailedRequest(resException.Message, resException);
            await context.Response.WriteAsync(response.ToJson());
        }
    }
}
