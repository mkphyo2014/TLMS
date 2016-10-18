using ServiceStack.Configuration;
using StructureMap;
using TLMS.Infrastructure;

namespace TLMS.Infrastructure
{
    public class InfrastructureInit : IInit
    {
        public void Init(IContainer container, IAppSettings appSettings)
        {
            container.Configure(c =>
            {
                c.For<IAppClock>().Singleton().Use<AppClock>();
            });
        }
    }
}
