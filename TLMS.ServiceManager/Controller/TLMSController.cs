using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NodaTime;
using TLMS.Entity.Dto;
using TLMS.Infrastructure.Domain;
using TLMS.ServiceManager.Repo;


//using DTO = Tocco.Orion.Sentinel.Api.AuthManagement.Dto;
//using ORM = Tocco.Orion.Sentinel.Api.AuthManagement.Orm;

namespace TLMS.ServiceManager.Controller
{
    public class TLMSController
    {
        private readonly ITLMSRepository _tlmsRepository;
        private readonly IAppClock _appClock;
        //private readonly IMapper _mapper;

        public TLMSController(ITLMSRepository tlmsRepository, IAppClock appClock)
        {
            _tlmsRepository = tlmsRepository;
            _appClock = appClock;
            //_mapper = CreateMapper();
        }

        public async Task<HelloResponse> Ping(HelloRequest request)
        {

            //await _tlmsRepository.Create(newUser);


            return new HelloResponse
            {
                Message = "Successful!"
            };
        }

    }
}