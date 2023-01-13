using Marsh.Bussines;
using Zamba.Services;
using System.Configuration;

namespace Marsh.Services
{
    public class SugerenciaServices
    {
        public SugerenciaServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        public bool Enviar(string usuario, string mensaje)
        {
            SugerenciaBussines suge = new SugerenciaBussines();

            suge.Usuario = usuario;
            suge.Mensaje = mensaje;

            return suge.GuardarSugerencia();
        }
    }
}
