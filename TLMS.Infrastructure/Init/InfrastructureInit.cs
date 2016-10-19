using ServiceStack.Configuration;
using StructureMap;
using TLMS.Infrastructure.Domain;
using TLMS.Infrastructure.Infra;

namespace TLMS.Infrastructure.Init
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
