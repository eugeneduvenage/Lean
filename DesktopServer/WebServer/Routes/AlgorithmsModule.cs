using Nancy;
using Nancy.Responses;
using System.Linq;

namespace QuantConnect.DesktopServer.WebServer.Routes
{
    public class AlgorithmsModule : NancyModule
    {
        private ISharedServerData _sharedServerData;
        public AlgorithmsModule(ISharedServerData desktopLeanManager)
        {
            _sharedServerData = desktopLeanManager;

            Get["/api/algorithms"] = _ =>
            {
                var algos = _sharedServerData.GetAlgorithms().Select(x => new { name = x }).ToArray();
                return algos;
                    //.WithModel(algos);
            };

            Get["/api/algorithms/{algorithmid}/backtests"] = parameters =>
            {
                string algorithmclassname = parameters["algorithmid"];
                var algos = _sharedServerData.GetBacktests(algorithmclassname).Select(x => new { name = x }).ToArray();
                return algos;
                //.WithModel(algos);
            };
        }
    }

}
