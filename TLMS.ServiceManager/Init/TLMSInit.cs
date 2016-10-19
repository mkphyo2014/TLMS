using ServiceStack.Configuration;
using TLMS.Infrastructure;
using StructureMap;
using TLMS.Infrastructure.Infra;

namespace TLMS.Init
{
    public class TLMSInit : IInit
    {
        public void Init(IContainer container, IAppSettings appSettings)
        {
            //container.Configure(c => c.For<IAuthManagementRepository>().Singleton().Use<AuthManagementRepository>());
            //container.Configure(c => c.For<AuthManagementController>().Singleton().Use<AuthManagementController>());
        }
    }
}

