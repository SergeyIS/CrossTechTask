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
            RegistryKey productKey = null;

            try
            {
                if (security == null)
                {
                    productKey = softwareKey.CreateSubKey(_productname, RegistryKeyPermissionCheck.Default);
                }
                else
                {
                    productKey = softwareKey.CreateSubKey(_productname, RegistryKeyPermissionCheck.Default, security);
                }
                productKey.SetValue(key, value);

                softwareKey.Close();
                productKey.Close();
            }
            catch(Exception e)
            {
                softwareKey.Close();
                productKey.Close();

                throw e;
            }
        }

        public void ChangePermissions(string key, RegistrySecurity security)
        {
            if (security == null || String.IsNullOrEmpty(key))
                throw new ArgumentNullException("security or key is NULL");

            RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("Software", true);

            try
            {
                string keyValue = (string) softwareKey.OpenSubKey(_productname).GetValue(key);
                softwareKey.DeleteSubKey(_productname, true);
                WriteToLocalMachine(key, keyValue, security);
                softwareKey.Close();
            }
            catch (Exception e)
            {
                softwareKey.Close();
                throw e;
            }
        }
    }
}
