using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TLMS.Entity.Orm;

namespace TLMS.ServiceManager.Repo
{
    public interface ITLMSRepository
    {
        Task<Course> Create(Course obj);
        Task<Object> Read(Object obj);
        Task<Object> Update(Object obj);
        Task<Object> Delete(Object obj);
    }
}