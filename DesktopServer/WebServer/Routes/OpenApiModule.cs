using System;
using Nancy;

namespace QuantConnect.DesktopServer.WebServer.Routes
{
    public class OpenApiModule : NancyModule
    {
        public OpenApiModule()
        {
            Get["/docs"] = _ =>
            {
                return Response.AsFile("Assets/openapi/index.html", "text/html");
            };

            Get["/docs/yaml"] = _ =>
            {
                return Response.AsFile("WebServer/open-api.yaml", "text/yaml")
                               .WithHeader("Cache-Control", "no-cache, no-store, must-revalidate")
                               .WithHeader("Pragma", "no-cache")
                               .WithHeader("Expires", "0");
            };
        }
    }
}
