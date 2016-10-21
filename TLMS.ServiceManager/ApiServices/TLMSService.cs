using System.Threading.Tasks;
using ServiceStack;
using TLMS.Entity.Dto;
using TLMS.ServiceManager.Controller;

namespace TLMS.ServiceManager.ApiServices
{
    public class TLMSService : Service
    {
        public TLMSController TLMSController { get; set; }

        public async Task<HelloResponse> Post(HelloRequest request)
        {
            return await TLMSController.Ping(request);
        }

        public async Task<TLMSCreateCourseResponse> Post(TLMSCreateCourseRequest request)
        {
            return await TLMSController.Create(request);
        }

    }

    
}