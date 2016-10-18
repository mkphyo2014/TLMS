using System;
using ServiceStack;

namespace TLMS.Infrastructure
{

    public interface IExceptionToResponseStatusMapper<in TException> where TException : Exception
    {
        ResponseStatus Map(TException exception);
    }

}