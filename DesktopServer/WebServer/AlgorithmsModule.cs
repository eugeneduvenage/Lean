using Nancy;

namespace QuantConnect.DesktopServer.WebServer
{
    public class AlgorithmsModule : NancyModule
    {
        private IDesktopLeanManager _desktopLeanManager;
        public AlgorithmsModule(IDesktopLeanManager desktopLeanManager)
        {
            _desktopLeanManager = desktopLeanManager;
            Get["/api/algorithms"] = _ =>
            {
                return Negotiate.WithModel(new AlgorithmResult(_desktopLeanManager.CurrentAlgorithm.Name));
            };
        }
    }

    public class AlgorithmResult 
    {
        public AlgorithmResult(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }
    }
}
