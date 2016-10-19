using System;
using ServiceStack;

namespace TLMS.Infrastructure.Infra
{

    public interface IExceptionToResponseStatusMapper<in TException> where TException : Exception
    {
        ResponseStatus Map(TException exception);
    }

}