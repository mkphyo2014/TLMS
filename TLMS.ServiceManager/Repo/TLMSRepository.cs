using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace TLMS.ServiceManager.Repo
{
    public class TLMSRepository : ITLMSRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public TLMSRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public Task<object> Create(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<object> Read(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<object> Update(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<object> Delete(object obj)
        {
            throw new NotImplementedException();
        }
    }
}