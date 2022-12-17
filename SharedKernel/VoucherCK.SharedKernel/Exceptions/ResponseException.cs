//using VoucherCK.Resource;
using VoucherCK.Resource;
using VoucherCK.SharedKernel.Interfaces;

namespace VoucherCK.SharedKernel.Exceptions
{
    public class ResponseException : Exception
    {
        public IResponseError? Error {get; set;}

        private object[] Param;

        public object DetailData;
        private int _status;    

        public string? Message;


        public int GetStatus() => _status;


        public ResponseException(IResponseError error)
        {
            _status = error.GetStatus();
            Error = error;
            //Return default language
            Message = ErrorMessageFactory.GetNamedMessage(error.GetName(), Thread.CurrentThread.CurrentUICulture);
        }



        public ResponseException(IResponseError error, object details)
        {
            _status = error.GetStatus();
            Error = error;
            if (details is string message)
            {
                Message = message;
            }
            else
            {
                DetailData = details;
                //Return default language
                Message = ErrorMessageFactory.GetNamedMessage(error.GetName(), Thread.CurrentThread.CurrentUICulture);
            }
        }

    }
}