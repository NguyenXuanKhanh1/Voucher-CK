using ChargeHub.SharedKernel.Interfaces;
using System.Net;
using VoucherCK.SharedKernel.Interfaces;

namespace VoucherCK.SharedKernel.Error
{
    public class NotFoundError : ErrorBase, IResponseError
    {

        public static NotFoundError Error(NotFoundErrorEnum error)
        {
            return new NotFoundError(error);
        }

        public static NotFoundError Error(NotFoundErrorEnum error, object e)
        {
            return new NotFoundError(error);
        }

        public NotFoundError(NotFoundErrorEnum error)
        {
            Code = (int)error;
            Message = error.ToString();
            _status = (int)HttpStatusCode.BadRequest;
        }

        public NotFoundError(NotFoundErrorEnum error, string field, string className)
        {
            Code = (int)error;
            Message = error.ToString();
            _field = field;
            _class = className;
            _status = (int)HttpStatusCode.BadRequest;
        }

        public string GetName() => Enum.GetName(typeof(NotFoundErrorEnum), Code);

        public int GetStatus() => _status;

        public int GetCode() => Code;
        public string GetMessage() => Message;
    }
}
