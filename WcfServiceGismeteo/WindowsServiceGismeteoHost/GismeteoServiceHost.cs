using System.ServiceModel;
using System.ServiceProcess;

namespace WindowsServiceGismeteoHost
{
    public partial class GismeteoServiceHost : ServiceBase
    {
        private ServiceHost _host;

        public GismeteoServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _host = new ServiceHost(typeof(WcfServiceGismeteo.GismeteoService));
            _host.Open();
        }

        protected override void OnStop()
        {
            _host?.Close();
        }
    }
}
