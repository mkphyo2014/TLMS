using System;
using System.Configuration;
using System.Linq;
using System.Web;
using NLog.Config;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.Text;
using LogManager = ServiceStack.Logging.LogManager;
using NLOG = NLog;

namespace TLMS.Api
{
    public class Global : HttpApplication
    {
        private static ILog _logger;

        protected void Application_Start(object sender, EventArgs e)
        {
            var appSettings = GetAppSettings();
            

            // configure NLog logging
            NLOG.LogManager.Configuration = PrepareXmlLoggingConfiguration(appSettings);
            LogManager.LogFactory = new NLogFactory();

            _logger = LogManager.GetLogger(typeof(Global).FullName);
            _logger.Info(appSettings.GetAll().Dump());

            _logger.Info("Starting app...");

            var apiName = appSettings.Get("app.apiName", "TLMS Api");
            var apiHost = new ApiHost(apiName, appSettings);
            apiHost.Init();

            _logger.Info("Starting app...DONE!");
        }

        private IAppSettings GetAppSettings()
        {
            // read settings from web.config
            var agentId = ConfigurationManager.AppSettings["agentId"];
            var env = ConfigurationManager.AppSettings["environment"];
            var textFilePath = AppDomain.CurrentDomain.RelativeSearchPath + $"\\AppSettings\\{agentId}.{env}.txt";
            var csDic = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().ToDictionary(x => x.Name, x => x.ConnectionString);
            var csDicSettings = new DictionarySettings(csDic);
            var appSettings = new MultiAppSettings(csDicSettings, new TextFileSettings(textFilePath), new AppSettings());
            return appSettings;
        }

        private static XmlLoggingConfiguration PrepareXmlLoggingConfiguration(IAppSettings appSettings)
        {
            var env = appSettings.GetString("environment");
            var storageConnString = appSettings.GetString("azure.storageConnectionString");
            var containerName = appSettings.GetString("azure.logContainerName");
            var config = new XmlLoggingConfiguration(AppDomain.CurrentDomain.RelativeSearchPath + $"\\NLogConfigs\\NLog.{env}.config", false);

            //if (!string.IsNullOrEmpty(storageConnString) && !string.IsNullOrEmpty(containerName))
            //{
            //    var asyncTargets = config.AllTargets.OfType<AsyncTargetWrapper>();
            //    var azureTargets = asyncTargets.Select(at => at.WrappedTarget).OfType<AzureAppendBlobTarget>();
            //    foreach (var azureTarget in azureTargets)
            //    {
            //        azureTarget.ConnectionString = storageConnString;
            //        azureTarget.Container = containerName;
            //    }
            //}
            return config;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}