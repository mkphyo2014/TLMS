using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NodaTime;
using TLMS.Entity.Dto;
using TLMS.Entity.Orm;
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

            
            string s = request.InputMessage;

            return new HelloResponse
            {
                Message = "Successful! Your input was " + s
            };
        }

        public async Task<TLMSCreateCourseResponse> Create(TLMSCreateCourseRequest request)
        {
            var sub = Guid.NewGuid().ToString();
            var newCourse = new Course
            {
                AllocationSub = sub,
                CourseCd = request.CourseCd,
                CourseTitle = request.CourseTitle,
                School = request.School,
                ProgrammeLevel = request.ProgrammeLevel,
                Programme = request.Programme,
                CourseType = request.CourseType,
                CourseArea = request.CourseArea,
                CourseUnit = request.CourseUnit,
                Remarks = request.Remarks
            };

            await _tlmsRepository.Create(newCourse);

            return new TLMSCreateCourseResponse
            {
                AllocationSub = sub,
                CourseCd = newCourse.CourseCd,
                CourseTitle = newCourse.CourseTitle,
                School = newCourse.School,
                ProgrammeLevel = newCourse.ProgrammeLevel,
                Programme = newCourse.Programme,
                CourseType = newCourse.CourseType,
                CourseArea = newCourse.CourseArea,
                CourseUnit = newCourse.CourseUnit,
                Remarks = newCourse.Remarks
            };
           
        }
    }
}