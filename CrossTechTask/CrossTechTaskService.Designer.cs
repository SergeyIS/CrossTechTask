using System;
using System.ComponentModel;
using CrossTechTask.BusinessLogic;
using CrossTechTask.EventLogger;
using CrossTechTask.DataContracts;

namespace CrossTechTask
{
    partial class CrossTechTaskService
    {
        private IRegistryController _regController = null;
        private IContainer _components = null;
        private ILogger _logger = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            ServiceName = "CrossTechTaskService";
            _logger = new ELogger("CrossTechTaskService");
            _regController = new RegistryController("CrossTechTaskService");

        } 
    }
}
