using Microsoft.Win32;
using System;
using System.Security.AccessControl;

namespace CrossTechTask.DataContracts
{
    public interface IRegistryController
    {
        void WriteToLocalMachine(string key, string value, RegistrySecurity security);
        void ChangePermissions(string key, RegistrySecurity security);
    }
}
