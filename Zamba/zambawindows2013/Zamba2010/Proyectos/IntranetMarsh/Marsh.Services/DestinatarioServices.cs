using System.Configuration;
using Marsh.Bussines;
using Zamba.Services;

namespace Marsh.Services
{
    public class DestinatarioServices
    {
        public DestinatarioServices()
        {
            Rights.ValidateLogIn(int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString()));
        }

        /// <summary>
        /// Lee la direccion de email de un destinatario. Los mismos estan
        /// guardados en un documento de Zamba.
        /// </summary>
        /// <param name="indice">Numero de indice donde se guarda la direccion</param>
        /// <returns></returns>
        public string ObtenerDestinatario(int indice)
        {
            return new DestinatarioBussines().ObtenerDestinatario(indice);
        }
    }
}
