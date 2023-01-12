using System.Collections.Generic;
using Marsh.Bussines;
using Zamba.Services;
using System.Configuration;

namespace Marsh.Services
{
    public class OtrosServiciosServices
    {
        public OtrosServiciosServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        public List<ServicioBussines> getListaServicios()
        {
            return new OtrosServiciosBussines().getServicios();
        }

        public bool EnviarSolicitud(long idservicio, string usuario, string mensaje)
        {
            OtrosServiciosBussines servicio = new OtrosServiciosBussines();

            servicio.IdServicio = idservicio;
            servicio.Usuario = usuario;
            servicio.Mensaje = mensaje;

            return servicio.GuardarSolicitud();
        }
    }
}
