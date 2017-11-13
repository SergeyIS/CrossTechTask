using Microsoft.Win32;
using System;
using System.Security.AccessControl;
using CrossTechTask.DataContracts;

namespace CrossTechTask.BusinessLogic
{
    public class RegistryController : IRegistryController
    {
        private string _productname;
        public RegistryController(String productName)
        {
            if (String.IsNullOrEmpty(productName))
                throw new ArgumentNullException("productName has NULL value");

            _productname = productName;
        }

        public void WriteToLocalMachine(string key, string value, RegistrySecurity security)
        {
            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software", true);
            RegistryKey productNameKey = null;

            try
            {
                if (security == null)
                {
                    productNameKey = softwareKey.CreateSubKey(_productname, RegistryKeyPermissionCheck.Default);
                }
                else
                {
                    productNameKey = softwareKey.CreateSubKey(_productname, RegistryKeyPermissionCheck.Default, security);
                }
                productNameKey.SetValue(key, value);
            }
            catch(Exception e)
            {
                softwareKey.Close();
                productNameKey.Close();

                throw e;
            }
        }
    }
}
