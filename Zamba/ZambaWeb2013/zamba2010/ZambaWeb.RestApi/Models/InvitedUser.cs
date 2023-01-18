using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Models
{
    public class InvitedUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cliente { get; set; }
        public string Sector { get; set; }
        public string Puesto { get; set; }
        public string Email { get; set; }
        public int Telefono { get; set; }
        public string Interno { get; set; }
        public int Celular { get; set; }
        public string Password { get; set; }
        public string img { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IpSolicitud { get; set; }
        public string Estado { get; set; }

    }
}