using System.Text.Json.Serialization;

namespace VoucherCK.SharedKernel.Error
{
    public class ErrorBase
    {
        protected const string NOT_DEFINED = "NOT_DEFINED";
        [JsonPropertyName("code")]
        public int Code;
        [JsonPropertyName("message")]
        public string Message;
        protected int _status;
        protected string _field;
        protected string _class;
    }
}
