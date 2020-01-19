using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace WindowsNative
{
    public static partial class Macros
    {
        // If you incorporate this code into a DLL, be sure to demand FullTrust.
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        public static void Impersonate(string username, string password, string domain = "")
        {
            try
            {
                // Get the user token for the specified user, domain, and password using the 
                // unmanaged LogonUser method. 
                // The local machine name can be used for the domain name to impersonate a user on this machine.
                if (string.IsNullOrEmpty(domain))
                    domain = System.Environment.MachineName;

                const int LOGON32_PROVIDER_DEFAULT = 0;
                //This parameter causes LogonUser to create a primary token. 
                const int LOGON32_LOGON_INTERACTIVE = 2;

                // Call LogonUser to obtain a handle to an access token. 
                WinBase.SafeTokenHandle safeTokenHandle;
                var returnValue = AdvApi32.LogonUser(username, domain, password,
                                                      LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                                                      out safeTokenHandle);

                Console.WriteLine("LogonUser called.");

                if (false == returnValue)
                {
                    var ret = Marshal.GetLastWin32Error();
                    Console.WriteLine("LogonUser failed with error code : {0}", ret);
                    throw new System.ComponentModel.Win32Exception(ret);
                }
                using (safeTokenHandle)
                {
                    Console.WriteLine("Did LogonUser Succeed? " + (returnValue ? "Yes" : "No"));
                    Console.WriteLine("Value of Windows NT token: " + safeTokenHandle);

                    // Check the identity.
                    Console.WriteLine("Before impersonation: "
                                      + WindowsIdentity.GetCurrent().Name);
                    // Use the token handle returned by LogonUser. 
                    using (var newId = new WindowsIdentity(safeTokenHandle.DangerousGetHandle()))
                    {
                        using (var impersonatedUser = newId.Impersonate())
                        {

                            // Check the identity.
                            Console.WriteLine("After impersonation: "
                                              + WindowsIdentity.GetCurrent().Name);
                        }
                    }
                    // Releasing the context object stops the impersonation 
                    // Check the identity.
                    Console.WriteLine("After closing the context: " + WindowsIdentity.GetCurrent().Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred. " + ex.Message);
            }


        }
        public static void Impersonate2()
        {
            // => privilege = new Privilege("SeCreateGlobalPrivilege");
            var privilegeType = Type.GetType("System.Security.AccessControl.Privilege");
            var privilege = Activator.CreateInstance(privilegeType, "SeCreateTokenPrivilege");

            // => privilege.Enable();
            privilegeType.GetMethod("Enable").Invoke(privilege, null);



            // =>  privilege.Revert();
            privilegeType.GetMethod("Revert").Invoke(privilege, null);
        }
    }

}
