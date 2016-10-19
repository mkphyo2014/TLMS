using ServiceStack.Configuration;
using StructureMap;

namespace TLMS.Infrastructure.Infra
{
    /// <summary>
    /// Register your concrete classes for the container
    /// </summary>
    public interface IInit
    {
        void Init(IContainer container, IAppSettings appSettings);
    }
}