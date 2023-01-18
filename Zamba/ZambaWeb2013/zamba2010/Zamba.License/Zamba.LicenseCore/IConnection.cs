using System;

namespace Zamba.LicenseCore
{
    public interface IConnection
    {
        string ZambaUser { get; set; }
        string WindowsUser { get; set; }
        string Host { get; set; }
        string License { get; set; }
        int TimeOut { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
