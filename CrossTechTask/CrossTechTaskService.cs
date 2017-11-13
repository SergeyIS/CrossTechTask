using System;
using System.Security.AccessControl;
using System.ServiceProcess;

namespace CrossTechTask
{
    public partial class CrossTechTaskService : ServiceBase
    {  
        public CrossTechTaskService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.WriteLog("CrossTechTaskService was started");

            try
            {
                //implementation of paragraph #1 (write key in registry)
                _regController.WriteToLocalMachine("URL", "localhost", null);

                //implementation of paragraph #2 (leave the rights to read the registry key for only one user)
                string user = Environment.UserDomainName + "\\" + Environment.UserName;
                RegistrySecurity rs = new RegistrySecurity();
                rs.AddAccessRule(new RegistryAccessRule(user,
                    RegistryRights.ReadKey | RegistryRights.WriteKey | RegistryRights.WriteKey | RegistryRights.ChangePermissions | RegistryRights.ChangePermissions | RegistryRights.Delete,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow));

                //overwriting of key
                _regController.WriteToLocalMachine("URL", "localhost", rs);
            }
            catch (Exception e)
            {
                _logger.WriteLog("An error was occured in CrossTechTaskService", e);
            }

        }

        protected override void OnStop()
        {
            _logger.WriteLog("CrossTechTaskService was stopped");
        }
    }
}
