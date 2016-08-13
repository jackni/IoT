using Autofac;
using Autofac.Integration.WebApi;
using IoTDemoWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace IoTDemoWeb
{
    public class AutofacConfig
    {
        public static void Config(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            builder.Register(s => new EnvSensorDataService()).As<IEnvSensorDataService>().InstancePerRequest();
            
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}