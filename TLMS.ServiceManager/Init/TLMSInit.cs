using ServiceStack.Configuration;
using StructureMap;
using TLMS.Infrastructure.Infra;
using TLMS.ServiceManager.Controller;
using TLMS.ServiceManager.Repo;

namespace TLMS.ServiceManager.Init
{
    public class TLMSInit : IInit
    {
        public void Init(IContainer container, IAppSettings appSettings)
        {
            container.Configure(c => c.For<ITLMSRepository>().Singleton().Use<TLMSRepository>());
            container.Configure(c => c.For<TLMSController>().Singleton().Use<TLMSController>());
        }
    }
}

