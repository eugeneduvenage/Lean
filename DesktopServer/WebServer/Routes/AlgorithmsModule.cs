using Nancy;
using Nancy.Responses;
using System.Linq;
using QuantConnect.DesktopServer.WebServer.Routes.Models;

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
                return _sharedServerData.GetAlgorithms().Select(x => new AlgorithmModel(x)).ToArray();
            };

            Get["/api/algorithms/{algorithmid}/backtests"] = parameters =>
            {
                string algorithmClassName = parameters["algorithmid"];
                if (!_sharedServerData.HasAlgorithm(algorithmClassName))
                {
                    return HttpStatusCode.BadRequest;
                }
                return _sharedServerData.GetBacktests(algorithmClassName).Select(x=> new BacktestModel(x));
            };

            Get["/api/algorithms/{algorithmid}/backtests/{backtestid}"] = parameters =>
            {
                string algorithmClassName = parameters["algorithmid"];
                if (!_sharedServerData.HasAlgorithm(algorithmClassName))
                {
                    return HttpStatusCode.BadRequest;
                }
                string backtestId = parameters["backtestid"];
                if(!_sharedServerData.HasBacktest( algorithmClassName, backtestId))
                {
                    return HttpStatusCode.BadRequest;                    
                }
                return new BacktestModel(_sharedServerData.GetBacktest(algorithmClassName, backtestId));
            };

            Get["/api/algorithms/{algorithmid}/backtests/{backtestid}/charts"] = parameters =>
            {
                string algorithmClassName = parameters["algorithmid"];
                if (!_sharedServerData.HasAlgorithm(algorithmClassName))
                {
                    return HttpStatusCode.BadRequest;
                }
                string backtestId = parameters["backtestid"];
                if (!_sharedServerData.HasBacktest(algorithmClassName, backtestId))
                {
                    return HttpStatusCode.BadRequest;
                }
                return new BacktestChartsDictionaryModel(_sharedServerData.GetBacktestResult(algorithmClassName, backtestId).Charts);
            };

            Get["/api/algorithms/{algorithmid}/backtests/{backtestid}/orders"] = parameters =>
            {
                string algorithmClassName = parameters["algorithmid"];
                if (!_sharedServerData.HasAlgorithm(algorithmClassName))
                {
                    return HttpStatusCode.BadRequest;
                }
                string backtestId = parameters["backtestid"];
                if (!_sharedServerData.HasBacktest(algorithmClassName, backtestId))
                {
                    return HttpStatusCode.BadRequest;
                }
                return new BacktestOrdersDictionaryModel(_sharedServerData.GetBacktestResult(algorithmClassName, backtestId).Orders);
            };

            Get["/api/algorithms/{algorithmid}/backtests/{backtestid}/statistics"] = parameters =>
            {
                string algorithmClassName = parameters["algorithmid"];
                if (!_sharedServerData.HasAlgorithm(algorithmClassName))
                {
                    return HttpStatusCode.BadRequest;
                }
                string backtestId = parameters["backtestid"];
                if (!_sharedServerData.HasBacktest(algorithmClassName, backtestId))
                {
                    return HttpStatusCode.BadRequest;
                }
                return new StatisticsModel(_sharedServerData.GetBacktestResult(algorithmClassName, backtestId).Statistics);
            };
        }
    }

}
