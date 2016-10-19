using System.Collections.Generic;
using ServiceStack;
using TLMS.Infrastructure.Infra;
using TLMS.ServiceManager.Controller;

namespace TLMS.ServiceManager.Init
{
    public class TLMSExceptionMapper : IExceptionToResponseStatusMapper<TLMSException>
    {
        public ResponseStatus Map(TLMSException exception)
        {
            var rs = new ResponseStatus
            {
                Errors = new List<ResponseError>
                {
                    new ResponseError
                    {
                        Message = exception.ErrorCode
                    },
                    new ResponseError
                    {
                        Message = exception.ErrorMessage
                    }
                },
                Message = exception.ErrorMessage
            };

            return rs;
        }
    }
}
