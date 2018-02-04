using System;
namespace QuantConnect.DesktopServer.WebServer.Routes.Models
{
    public class AlgorithmModel
    {
        public AlgorithmModel(string classTypeName)
        {
            ClassTypeName = classTypeName;
        }

        public string ClassTypeName
        {
            get;
            protected set;
        }
    }
}
