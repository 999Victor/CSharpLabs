using System.ServiceProcess;

namespace DirectoryMonitorService
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DirectoryMonitorService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
