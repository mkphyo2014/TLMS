using System;

namespace TLMS.ServiceManager.Controller
{
    public class TLMSException : Exception
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public TLMSException(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
