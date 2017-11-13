using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace CrossTechTask
{
    [RunInstaller(true)]
    public partial class CrossTechInstaller : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public CrossTechInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "CrossTechTaskService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
