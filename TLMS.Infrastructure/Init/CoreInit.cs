using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using StructureMap;
using TLMS.Infrastructure.Infra;

namespace TLMS.Infrastructure.Init
{
    public class CoreInit : IInit
    {
        public void Init(IContainer container, IAppSettings appSettings)
        {

            var defaultDbConnectionString = appSettings.GetString("connectionString.default");
            var connectionFactory = CreateDbConnectionFactory(defaultDbConnectionString);
            if (connectionFactory != null) container.Configure(c => c.For<IDbConnectionFactory>().Singleton().Use(connectionFactory));

            var auditDbConnectionString = appSettings.GetString("connectionString.audit");
            var connectionFactory2 = CreateDbConnectionFactory(auditDbConnectionString);
            if (connectionFactory2 != null) container.Configure(c => c.For<IDbConnectionFactory>().Singleton().Add(connectionFactory2).Named("connectionString.audit"));

        }

        private OrmLiteConnectionFactory CreateDbConnectionFactory(string connectionString)
        {
            if (connectionString.IsNullOrEmpty()) return null;

            OrmLiteConnectionFactory connectionFactory;
            if (connectionString == ":memory:")
            {
                connectionFactory = new OrmLiteConnectionFactory(connectionString, SqliteDialect.Provider);
                connectionFactory.DialectProvider.GetStringConverter().UseUnicode = true;
                using (var db = connectionFactory.OpenDbConnection())
                {
                    // to have Sqlite have foreign key relationship
                    db.ExecuteNonQuery("PRAGMA foreign_keys = ON");
                }
            }
            else
            {
                connectionFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
                connectionFactory.DialectProvider.GetStringConverter().UseUnicode = true;

            }

            return connectionFactory;
        }
    }
}
