using System;
using System.IO;
using Nancy;
using Nancy.Responses;

namespace QuantConnect.DesktopServer.WebServer
{
    public class IndexModule : NancyModule
    {
        public IndexModule(IRootPathProvider pathProvider)
        {
            Get["/"] = _ =>
            {
                var p = pathProvider.GetRootPath();
                Console.WriteLine(p);
                return Response.AsFile("Assets/html/index.html", "text/html");
            };
        }
    }
}
