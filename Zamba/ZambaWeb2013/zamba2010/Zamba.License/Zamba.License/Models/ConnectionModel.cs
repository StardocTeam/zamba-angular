using System;
using System.ComponentModel.DataAnnotations;
using Zamba.LicenseCore;

namespace Zamba.License.Models
{
    public class ConnectionModel: IConnection
    {
        [Display(Name = "Usuario Zamba")]
        public string ZambaUser { get; set; }

        [Display(Name = "Usuario Windows")]
        public string WindowsUser { get; set; }

        [Display(Name = "Puesto")]
        public string Host { get; set; }

        [Display(Name = "Licencia")]
        public string License { get; set; }

        [Display(Name = "Timeout")]
        public int TimeOut { get; set; }

        [Display(Name = "Creada")]
        public DateTime Created { get; set; }

        [Display(Name = "Actualizada")]
        public DateTime Updated { get; set; }

        public ConnectionModel(IConnection c)
        {
            ZambaUser = c.ZambaUser;
            WindowsUser = c.WindowsUser;
            Host = c.Host;
            License = c.License;
            TimeOut = c.TimeOut;
            Created = c.Created;
            Updated = c.Updated;
        }
    }
}