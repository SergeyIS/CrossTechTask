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
                _logger.WriteLog("wrote key in register: URL=localhost");

                //implementation of paragraph #2 (leave the rights to read the registry key for only one user)
                RegistrySecurity rs = new RegistrySecurity();
                rs.AddAccessRule(new RegistryAccessRule(Environment.UserName,
                    RegistryRights.ReadKey | RegistryRights.WriteKey | RegistryRights.Delete | RegistryRights.ChangePermissions,
                    InheritanceFlags.None,
                    PropagationFlags.None,
                    AccessControlType.Allow));

                //overwriting of the key
                _regController.ChangePermissions("URL", rs);
                _logger.WriteLog("changed permission for key");
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
