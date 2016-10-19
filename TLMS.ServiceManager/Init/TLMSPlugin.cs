using ServiceStack;
using ServiceStack.Validation;
using TLMS.ServiceManager.ApiServices;

namespace TLMS.ServiceManager.Init
{
    public class TLMSPlugin : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.RegisterService<TLMSService>();

            appHost.GetContainer().RegisterValidators(typeof(TLMSPlugin).Assembly);
        }
    }
}
