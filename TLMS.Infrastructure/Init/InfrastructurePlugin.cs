using System.IO;
using NodaTime;
using NodaTime.Serialization.ServiceStackText;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Text;
using ServiceStack.Validation;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
using TLMS.Infrastructure.Infra;

namespace TLMS.Infrastructure.Init
{
    public partial class InfrastructurePlugin : IPlugin, IPreInitPlugin
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(InfrastructurePlugin).FullName);

        public void Register(IAppHost appHost)
        {
            RegisterPlugins(appHost);
            RegisterJsonConfigs();


            var appSettings = appHost.GetContainer().Resolve<IAppSettings>();

            
            // using structuremap IoC
            var structureMapIoc = new Container();
            structureMapIoc.Configure(conf =>
            {
                conf.AddRegistry(new InitRegistry());
                conf.For<IAppSettings>().Singleton().Use(appSettings);
                conf.For<ICacheClient>().Singleton().UseInstance(new ObjectInstance(new MemoryCacheClient()));
                conf.For<IContainer>().Singleton().Use(structureMapIoc);

            });
            appHost.GetContainer().Adapter = new StructureMapContainerAdapter(structureMapIoc);

            PrepareInfrastructureStuff(structureMapIoc, appSettings);
            RegisterExceptionMappers(appHost, structureMapIoc);

            // ensure mapping is valid
            //Mapper.AssertConfigurationIsValid();
        }




        private void PrepareInfrastructureStuff(Container structureMapIoc, IAppSettings settings)
        {
            using (var nestedContainer = structureMapIoc.GetNestedContainer())
            {
                // Allow Components to register to structureMap IoC
                var inits = nestedContainer.GetAllInstances<IInit>();
                _logger.Info("Inits...");
                foreach (var init in inits)
                {
                    init.Init(structureMapIoc, settings);
                    _logger.Info(init.GetType().Name);
                }
                
                

                // Execute IIocPostConfig classes
                var postInits = nestedContainer.GetAllInstances<IPostInit>();
                _logger.Info("PostInits...");
                foreach (var postInit in postInits)
                {
                    postInit.PostInit(structureMapIoc, settings);
                    _logger.Info(postInit.GetType().Name);
                }

                //_logger.Debug(nestedContainer.WhatDoIHave());
            }
        }

        private static void RegisterPlugins(IAppHost appHost)
        {
            // add validators
            appHost.LoadPlugin(new ValidationFeature());

            // add postman
            appHost.LoadPlugin(new PostmanFeature());

            // add CORS 
            appHost.LoadPlugin(new CorsFeature("*", "GET, POST, PUT, DELETE, OPTIONS", "Content-Type, Authorization, X-AUTH-TOKEN", true));
        }

        private static void RegisterJsonConfigs()
        {
            JsConfig.DateHandler = DateHandler.ISO8601;
            JsConfig.TimeSpanHandler = TimeSpanHandler.StandardFormat;
            JsConfig.IncludeNullValues = false;
            // JsConfig.EmitCamelCaseNames = true;
        }

        private class InitRegistry : Registry
        {
            private static readonly ILog _initRegistrylogger = LogManager.GetLogger(typeof(InitRegistry).FullName);
            public InitRegistry()
            {
                try
                {
                    Scan(scan =>
                    {
                        scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("TLMS") && a.FullName.Contains("Api"));
                        //scan.AssembliesFromApplicationBaseDirectory();
                        scan.AddAllTypesOf<IInit>();
                        scan.AddAllTypesOf<IPostInit>();
                        scan.ConnectImplementationsToTypesClosing(typeof(IExceptionToResponseStatusMapper<>));
                    });
                }
                catch (FileNotFoundException fnfe)
                {
                    _initRegistrylogger.Error(fnfe.FileName);
                }
            }
        }

        public void Configure(IAppHost appHost)
        {
            // this line is also required to do SQL conversion for ZonedDateTime. See NodaTimeSqlConverters.cs
            DateTimeZoneProviders.Tzdb.CreateDefaultSerializersForNodaTime().ConfigureSerializersForNodaTime();

            // register NodaTime to Sql Server converters
            NodaTimeSqlConverters.RegisterNodaTimeSqlConverters();



            // to hook up StructureMap and SignalR's dependency injection
            // with compliments from this link http://stackoverflow.com/questions/20705937/how-do-you-resolve-signalr-v2-0-with-structuremap-v2-6/
            // see HubActivator.cs also
            //GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new HubActivator(apiHost.Container));
        }

        
    }
}
