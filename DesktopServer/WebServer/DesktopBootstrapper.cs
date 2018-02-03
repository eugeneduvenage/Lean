using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QuantConnect.DesktopServer.WebServer
{
    public class DesktopBootstrapper : DefaultNancyBootstrapper
    {
        private ISharedServerData _desktopServerData;
        public DesktopBootstrapper(ISharedServerData desktopServerData)
        {
            if(desktopServerData == null) 
            {
                throw new ArgumentNullException(nameof(desktopServerData), "No DesktopServerData provided, DesktopBootstrapper cannot continue");
            }

            _desktopServerData = desktopServerData;
        }

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            container.Register<ISharedServerData>(_desktopServerData);
        }

        protected override System.Collections.Generic.IEnumerable<Func<System.Reflection.Assembly, bool>> AutoRegisterIgnoredAssemblies
        {
            get
            {
                return base.AutoRegisterIgnoredAssemblies
                           .Concat(new Func<Assembly, bool>[]
                {
                    assembly => !assembly.FullName.StartsWith("QuantConnect.DesktopServer", StringComparison.OrdinalIgnoreCase)
                });
            }
        }

        protected override void ConfigureApplicationContainer(Nancy.TinyIoc.TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CustomJsonSerializer>();
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/assets", "Assets"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/docs", "Assets/openapi"));
            base.ConfigureConventions(nancyConventions);
        }

        protected override IRootPathProvider RootPathProvider
        {
            get { return new CustomRootPathProvider(); }
        }
    }

    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            var rootPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../"));
            return rootPath;
        }
    }

    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            this.Formatting = Formatting.Indented;
        }
    }
}
