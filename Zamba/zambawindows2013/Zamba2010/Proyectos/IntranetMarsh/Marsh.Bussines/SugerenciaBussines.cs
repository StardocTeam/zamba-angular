using System;
using System.Configuration;
using Zamba.Services;
using Marsh.Data;
using Zamba.Core;

namespace Marsh.Bussines
{
    public class SugerenciaBussines
    {
        private string _usuario;
        private string _mensaje;

        private int _zamba_user_Id;

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Mensaje 
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public SugerenciaBussines()
        {
            try
            {
                int.TryParse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString(), out _zamba_user_Id);
            }
            catch(Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda la sugerencia ingresada en un tipo de documento sugerencia
        /// </summary>
        /// <returns></returns>
        public bool GuardarSugerencia()
        {
            bool resul;

            try
            {                
                SugerenciaData sug = new SugerenciaData();

                resul = sug.GuardarSugerencia(_usuario, _mensaje);
                EnviarNotificacion();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                resul = false;
            }
            return resul;
        }

        /// <summary>
        /// Envia un email avisando que se ingreso una nueva sugerencia
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string body;
            int idindice;

            EmailNotificacion email = new EmailNotificacion(_zamba_user_Id);

            idindice = int.Parse(ConfigurationSettings.AppSettings["desti_ind_sugerencia"].ToString());

            to = new DestinatarioBussines().ObtenerDestinatario(idindice);

            body = email.LeerHTML("email_aviso.htm");

            body = body.Replace("[USUARIO]", _usuario);
            body = body.Replace("[TIPO_ENVIO]", "una nueva sugerencia");
            body = body.Replace("[DETALLE]", "<i>" + _mensaje + "</i>");

            return email.EnviarNotificacion(to, "Han enviado una nueva sugerencia", body);
        }
    }
}