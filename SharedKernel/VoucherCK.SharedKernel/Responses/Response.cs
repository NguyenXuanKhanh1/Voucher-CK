using VoucherCK.SharedKernel.Exceptions;
using VoucherCK.SharedKernel.Interfaces;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace VoucherCK.SharedKernel.Responses
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Response<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("data")]
        public T? Data { get; set; }
        [JsonProperty("message")]
        public object? Message { get; set; }
        [JsonProperty("errors")]
        public object Errors { get; set; }
        
        [JsonProperty("details")]
        public object? Details { get; set; }

        public static Response<object> BadRequest(string? message, IEnumerable<ValidationFailure> errors)
        {
            return new Response<object>
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
        
        public static Response<object> BadRequest(string? message)
        {
            return new Response<object>
            {
                Success = false,
                Message = message,
            };
        }

        public static Response<object> BadRequest(string message, Exception ex)
        {
            return new Response<object>
            {
                Success = false,
                Message = message,
                Errors = ex.Message
            };
        }
        
        public static Response<object> FailedRequest(string? message, ResponseException ex)
        {
            return new Response<object>
            {
                Success = false,
                Message = message,
                Errors = new List<IResponseError?> { ex.Error },
                //TODO 
                //Disable before launching PRD
                Details = ex?.DetailData is Exception exception? $"Message: {exception.Message}\nStack: {exception.StackTrace}" : ex?.DetailData
            };
        }
    }
}
