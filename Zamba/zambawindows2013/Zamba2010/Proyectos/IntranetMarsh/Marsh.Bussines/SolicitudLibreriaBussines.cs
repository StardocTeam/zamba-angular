using System;
using System.Configuration;
using System.Collections.Generic;
using Zamba.Services;
using System.Text;
using Zamba.Core;
using Marsh.Data;

namespace Marsh.Bussines
{
    public class SolicitudLibreriaBussines
    {
        private IList<ArticuloLibreriaBussines> _listaArticulos;
        
        private string _usuario;
        private string _articulos = "";

        private int _zamba_user_Id;

        public IList<ArticuloLibreriaBussines> ListaArticulos
        {
            get { return _listaArticulos; }
            set { _listaArticulos = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public SolicitudLibreriaBussines()
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
        /// Guarda la sugerencia ingresada en un tipo de documento solicitud de libreria
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud()
        {
            bool resul;

            try
            {
                SolicitudLibreriaData lib = new SolicitudLibreriaData();

                resul = lib.GuardarSolicitud(_usuario, ListaProductos());
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
        /// Convierte la lista de articulos en un string
        /// </summary>
        /// <returns></returns>
        private string ListaProductos()
        {           
            StringBuilder sb = new StringBuilder();

            foreach (ArticuloLibreriaBussines arti in _listaArticulos)
            {
                if (arti.Cantidad > 0)
                {
                    sb.Append(arti.Cantidad);

                    if (arti.Unidad == "unidades")
                    {
                        if (arti.Cantidad == 1)
                            sb.Append(" unidad");
                        else
                            sb.Append(" unidades");
                    }
                    else
                    {
                        if (arti.Cantidad == 1)
                            sb.Append(" paquete");
                        else
                            sb.Append(" paquetes");
                    }

                    sb.Append(" de ");
                    sb.Append(arti.Articulo);
                    sb.Append("\n");
                }
           }

            _articulos = sb.ToString();

            return _articulos;
        }

        /// <summary>
        /// Envia un email avisando que se ingreso una nueva solicitud
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string body;
            int idindice;

            EmailNotificacion email = new EmailNotificacion(_zamba_user_Id);

            idindice = int.Parse(ConfigurationSettings.AppSettings["desti_ind_libreria"].ToString());

            to = new DestinatarioBussines().ObtenerDestinatario(idindice);

            body = email.LeerHTML("email_aviso.htm");

            body = body.Replace("[USUARIO]", _usuario);
            body = body.Replace("[TIPO_ENVIO]", "una nueva solicitud de libreria");
            body = body.Replace("[DETALLE]", "Articulos:<br>" + _articulos.Replace("\n", "<br>"));

            return email.EnviarNotificacion(to, "Han enviado una nueva solicitud de libreria", body);
        }
    }
}