using System.Configuration;
using Marsh.Bussinnes;
using Zamba.Services;

namespace Marsh.Services
{
    public class DesperfectoServices
    {
        public DesperfectoServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        public bool Enviar(string usuario, string lugar, string descripcion)
        {
            DesperfectoBussinnes Desp = new DesperfectoBussinnes();

            Desp.Usuario = usuario;
            Desp.Lugar = lugar;
            Desp.Descripcion = descripcion;

            return Desp.GuardarSolicitud();
        }
    }
}
