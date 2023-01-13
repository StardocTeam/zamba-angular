using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services;
using Marsh.Data;
using System.Configuration;
using System.Data;

namespace Marsh.Bussines
{
    public class OtrosServiciosBussines
    {
        private string _usuario;
        private string _mensaje = "";
        private long _idServicio;

        private int _zamba_user_Id;

        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public long IdServicio
        {
            get { return _idServicio; }
            set { _idServicio = value; }
        }

        public OtrosServiciosBussines()
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
        /// Obtiene los servicios asociados al indice servicios
        /// </summary>
        /// <returns></returns>
        public List<ServicioBussines> getServicios()
        {
            List<ServicioBussines> lista = new List<ServicioBussines>();
            DataSet ds;

            ds = new OtrosServiciosData().getServicios();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ServicioBussines ser = new ServicioBussines();

                ser.Id = int.Parse(row["codigo"].ToString());
                ser.Descripcion = row["descripcion"].ToString();
                
                lista.Add(ser);
            }

            return lista;
        }

        /// <summary>
        /// Guarda el pedido de servicios ingresado en un tipo de documento "otros servicios"
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud()
        {
            bool resul;

            try
            {
                OtrosServiciosData serv = new OtrosServiciosData();

                resul = serv.GuardarSolicitud(_usuario, _mensaje, _idServicio);
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

            idindice = int.Parse(ConfigurationSettings.AppSettings["desti_ind_otros_servicios"].ToString());

            to = new DestinatarioBussines().ObtenerDestinatario(idindice);

            body = email.LeerHTML("email_aviso.htm");

            detalle = "Servicio solicitado: " + new OtrosServiciosData().getServicioById(_idServicio).Tables[0].Rows[0]["descripcion"].ToString();
            detalle += "<br>Mensaje: " + _mensaje;

            body = body.Replace("[USUARIO]", _usuario);
            body = body.Replace("[TIPO_ENVIO]", "una nueva solicitud de servicios");
            body = body.Replace("[DETALLE]", detalle);

            return email.EnviarNotificacion(to, "Han enviado una nueva solicitud de servicios", body);
        }
    }
}