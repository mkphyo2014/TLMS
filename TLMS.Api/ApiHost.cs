using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;

namespace TLMS.Api
{
    // same piece of code as UnitTestAppHost
    public class ApiHost : AppHostBase
    {
        private readonly IAppSettings _appSettings;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ApiHost).FullName);


        public ApiHost(string apiName, IAppSettings appSettings) : base(apiName, typeof(ApiHost).Assembly)
        {
            _appSettings = appSettings;
        }

        public override void Configure(Container funqContainer)
        {
            funqContainer.Register(_appSettings);
            var appPlugins = GetAppPlugins();
            Plugins.AddRange(appPlugins);
        }


        private IEnumerable<IPlugin> GetAppPlugins()
        {
            string executingFolder;
            if (string.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath))
            {
                executingFolder = AppDomain.CurrentDomain.BaseDirectory; //exe folder for WinForms, Consoles, Windows Services
            }
            else
            {
                executingFolder = AppDomain.CurrentDomain.RelativeSearchPath; //bin folder for Web Apps 
            }

            var dlls = Directory.EnumerateFiles(executingFolder, "*.dll");
            var assemblies = dlls.Select(Assembly.LoadFrom);
            var tlmsAssemblies = assemblies.Where(a => a.FullName.StartsWith("TLMS"));

            var ipluginTypes = tlmsAssemblies
                .SelectMany(a => a.GetTypes()).Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(IPlugin)));
            var selectedPluginsConfig = _appSettings.Get<List<string>>("app.plugins");

            var selectedPlugins = ipluginTypes.Where(x => selectedPluginsConfig.Any(y => x.Name.Contains(y)));

            _logger.Info("Plugins loaded");
            selectedPlugins.ToList().ForEach(a => _logger.Info(a.Name));

            var iplugins = selectedPlugins.Select(p => Activator.CreateInstance(p) as IPlugin);
            return iplugins;

        }
    }
}