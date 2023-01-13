using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marsh.Data;
using Zamba.Services;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Bussines
{
    public class TarjetasComercialesBussines
    {
        private string _usuario;
        private string _cargo;
        private string _sector;
        private string _telefono;
        private string _email;

        private int _zamba_user_Id;

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public TarjetasComercialesBussines()
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
        /// Guarda la solicitud de tarjetas comerciales
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud()
        {
            bool resul;

            try
            {
                TarjetasComercialesData tarj = new TarjetasComercialesData();

                resul = tarj.GuardarSolicitud(_usuario, _cargo, _sector, _telefono, _email);
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
        /// Envia un email avisando que se ingreso un nuevo pedido de servicios
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string body;
            string detalle;
            int idindice;

            EmailNotificacion email = new EmailNotificacion(_zamba_user_Id);

            idindice = int.Parse(ConfigurationSettings.AppSettings["desti_ind_tarj_comerciales"].ToString());

            to = new DestinatarioBussines().ObtenerDestinatario(idindice);

            body = email.LeerHTML("email_aviso.htm");

            detalle = "Apellido y nombre: " + _usuario;
            detalle += "<br>Cargo: " + _cargo;
            detalle += "<br>Sector: " + _sector;
            detalle += "<br>Telefono: " + _telefono;
            detalle += "<br>Email: " + _email;

            body = body.Replace("[USUARIO]", _usuario);
            body = body.Replace("[TIPO_ENVIO]", "una nueva solicitud de tarjeta comercial con la siguiente informacion");
            body = body.Replace("[DETALLE]", detalle);

            return email.EnviarNotificacion(to, "Han enviado una nueva solicitud de tarjeta comercial", body);
        }
    }
}