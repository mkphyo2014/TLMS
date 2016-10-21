using System;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using StructureMap;
using TLMS.Entity.Orm;
using TLMS.Infrastructure.Infra;

namespace TLMS.ServiceManager.Init
{
    public class TLMSPostInit : IPostInit
    {
        public void PostInit(IContainer container, IAppSettings settings)
        {
            var connectionFac = container.GetInstance<IDbConnectionFactory>();
            InitDb(connectionFac);
        }

        private static void InitDb(IDbConnectionFactory connectionFac)
        {
            using (var db = connectionFac.OpenDbConnection())
            {
                db.CreateTableIfNotExists<Allocation>();
                db.CreateTableIfNotExists<Course>();
                db.CreateTableIfNotExists<Faculty>();
                db.CreateTableIfNotExists<Term>();
                
            }
        }
    }
}
