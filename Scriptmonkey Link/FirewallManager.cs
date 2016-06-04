using System;
using NetFwTypeLib;

namespace Scriptmonkey_Link
{
    internal static class FirewallManager
    {
        private const string CLSID_FIREWALL_MANAGER = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";
        // ProgID for the AuthorizedApplication object
        private const string PROGID_AUTHORIZED_APPLICATION = "HNetCfg.FwAuthorizedApplication";
        private const string PROGID_OPEN_PORT = "HNetCfg.FWOpenPort";
        
        private static INetFwMgr GetFirewallManager()
        {
            Type objectType = Type.GetTypeFromCLSID(
                  new Guid(CLSID_FIREWALL_MANAGER));
            return Activator.CreateInstance(objectType)
                  as INetFwMgr;
        }

        internal static bool IsFirewallEnabled()
        {
            INetFwMgr manager = GetFirewallManager();
            bool isFirewallEnabled =
                manager.LocalPolicy.CurrentProfile.FirewallEnabled;
            if (isFirewallEnabled == false)
                manager.LocalPolicy.CurrentProfile.FirewallEnabled = true;
            return isFirewallEnabled;
        }

        internal static bool AuthorizeApplication(string title, string applicationPath,
            NET_FW_SCOPE_ scope/*, NET_FW_IP_VERSION_ ipVersion*/, int port, NET_FW_IP_PROTOCOL_ protocol)
        {
            // Create the type from prog id
            Type type = Type.GetTypeFromProgID(PROGID_AUTHORIZED_APPLICATION);
            INetFwAuthorizedApplication auth = Activator.CreateInstance(type)
                as INetFwAuthorizedApplication;
            auth.Name = title;
            auth.ProcessImageFileName = applicationPath;
            auth.Scope = scope;
            //auth.IpVersion = ipVersion; // Not implemented
            auth.Enabled = true;
            
            INetFwMgr manager = GetFirewallManager();
            try
            {
                manager.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(auth);
            }
            catch (Exception)
            {
                return false;
            }
            return GloballyOpenPort(title, port, scope, protocol, manager);
        }
        
        private static bool GloballyOpenPort(string title, int portNo,
            NET_FW_SCOPE_ scope, NET_FW_IP_PROTOCOL_ protocol/*,
            NET_FW_IP_VERSION_ ipVersion*/, INetFwMgr manager)
        {
            Type type = Type.GetTypeFromProgID(PROGID_OPEN_PORT);
            INetFwOpenPort port = Activator.CreateInstance(type)
                as INetFwOpenPort;
            port.Name = title;
            port.Port = portNo;
            port.Scope = scope;
            port.Protocol = protocol;
            //port.IpVersion = ipVersion;
            
            try
            {
                manager.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(port);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
