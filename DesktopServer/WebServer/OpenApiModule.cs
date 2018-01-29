using System;
using Nancy;

namespace QuantConnect.DesktopServer.WebServer
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
                return Response.AsFile("WebServer/open-api.yaml", "text/yaml");
            };
        }
    }
}
