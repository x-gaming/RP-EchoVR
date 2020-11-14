using System.ServiceProcess;

namespace EchoRPService {
    static class MainService {
        static void Main() {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new EchoRPService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
