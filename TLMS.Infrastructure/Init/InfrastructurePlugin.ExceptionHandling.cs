using System.Collections.Generic;
using ServiceStack;
using StructureMap;
using TLMS.Infrastructure;

namespace TLMS.Infrastructure
{
    public partial class InfrastructurePlugin
    {
        private void RegisterExceptionMappers(IAppHost appHost, Container structureMapIoc)
        {
            appHost.ServiceExceptionHandlers.Add((req, dto, ex) =>
            {
                var exceptionType = ex.GetType();
                var exceptionMapper = structureMapIoc.TryGetInstance(typeof(IExceptionToResponseStatusMapper<>).MakeGenericType(exceptionType));

                ResponseStatus rs;
                if (exceptionMapper != null)
                {

                    var method = exceptionMapper.GetType().GetMethod("Map");
                    if (method != null)
                    {
                        rs = (ResponseStatus)method.Invoke(exceptionMapper, new object[] { ex });
                    }
                    else
                    {
                        rs = ex.ToResponseStatus();
                        rs.Errors = new List<ResponseError>
                        {
                            new ResponseError
                            {
                                Message = ex.Message
                            }
                        };
                    }

                }
                else
                {
                    rs = ex.ToResponseStatus();
                    rs.Errors = new List<ResponseError>
                    {
                        new ResponseError
                        {
                            Message = ex.Message
                        }
                    };
                }

#if DEBUG
                rs.StackTrace = ex.StackTrace;
#endif
                return DtoUtils.CreateErrorResponse(req, ex, rs);
            });
        }
    }
}
