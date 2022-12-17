using System.Net;
using VoucherCK.Application;
//using VoucherCK.Resource;
using VoucherCK.SharedKernel.Responses;
using VoucherCK.Utility;

namespace VoucherCK.Api.ExceptionHandler
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public async Task Handle(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var response = Response<Exception>.BadRequest("", exception);
        await context.Response.WriteAsync(response.ToJson());
    }
}
}
