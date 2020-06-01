using System.ServiceProcess;

namespace WindowsServiceGismeteoHost
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new GismeteoServiceHost()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
