using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TLMS.ServiceManager.Repo
{
    public interface ITLMSRepository
    {
        Task<Object> Create(Object obj);
        Task<Object> Read(Object obj);
        Task<Object> Update(Object obj);
        Task<Object> Delete(Object obj);
    }
}