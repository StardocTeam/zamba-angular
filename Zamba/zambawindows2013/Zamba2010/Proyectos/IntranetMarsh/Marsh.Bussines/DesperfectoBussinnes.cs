using System;
using System.Configuration;
using Marsh.Bussines;
using Marsh.Data;
using Zamba.Core;
using Zamba.Services;

namespace Marsh.Bussinnes
{    
    public class DesperfectoBussinnes
    {
        private string _usuario;
        private string _lugar;
        private string _descripcion;

        private int _zamba_user_Id;

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Lugar
        {
            get { return _lugar; }
            set { _lugar = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public DesperfectoBussinnes()
        {
            try
            {
                _zamba_user_Id = int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString());
            }
            catch(Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda el desperfecto ingresado en un tipo de documento solicitud de desperfecto
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud()
        {
            bool resul;

            try
            {
                DesperfectoData desperfecto = new DesperfectoData();

                resul = desperfecto.GuardarSolicitud(_usuario, _lugar, _descripcion);
                EnviarNotificacion();                
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                resul = false;
            }
            return resul;
        }

        /// <summary>
        /// Envia un email avisando que se ingreso un nuevo desperfecto
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string body;
            string detalle;
            int idindice;

            EmailNotificacion email = new EmailNotificacion(_zamba_user_Id);

            idindice = int.Parse(ConfigurationSettings.AppSettings["desti_ind_servicios"].ToString());

            to = new DestinatarioBussines().ObtenerDestinatario(idindice);

            body = email.LeerHTML("email_aviso.htm");

            detalle = "Lugar: " + _lugar + "<br>";
            detalle += "Descripcion: " + _descripcion + "<br>";

            body = body.Replace("[USUARIO]", _usuario);
            body = body.Replace("[TIPO_ENVIO]", "un nuevo desperfecto");
            body = body.Replace("[DETALLE]", detalle);
                        
            return email.EnviarNotificacion(to, "Han enviado un nuevo desperfecto", body);
        }
    }
}
