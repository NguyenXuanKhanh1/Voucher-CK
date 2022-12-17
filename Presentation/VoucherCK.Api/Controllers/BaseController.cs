using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VoucherCK.SharedKernel.Responses;

namespace VoucherCK.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Response<T> Ok<T>(string msg, T obj) where T : class
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new Response<T>
            {
                Success = true,
                Message = msg,
                Data = obj
            };
        }

        protected Response<T> Ok<T>(T obj) where T : class
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new Response<T>
            {
                Success = true,
                Message = string.Empty,
                Data = obj
            };
        }

        protected Response<T> OK<T>(T obj) where T : struct
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new Response<T>
            {
                Success = true,
                Message = string.Empty,
                Data = obj
            };
        }

        protected Response<T> OK<T>(string msg, T obj) where T : struct
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new Response<T>
            {
                Success = true,
                Message = msg,
                Data = obj
            };
        }

        protected Response<object> Ok<T>(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return new Response<object>
            {
                Success = true,
                Message = msg,
            };
        }
    }
}
