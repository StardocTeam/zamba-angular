using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Zamba.Services;
using System.Text;
using Marsh.Services;
using Marsh.Bussines;

namespace IntranetMarshMVC.Models
{
    public class SolicitudLibreria
    {
        private IList<ArticuloLibreria> _listaArticulos;
        
        private string _usuario;
        private string _articulos = "";

        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_productos;

        private int _zamba_user_Id;

        public IList<ArticuloLibreria> ListaArticulos
        {
            get { return _listaArticulos; }
            set { _listaArticulos = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public SolicitudLibreria()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["solic_lib_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_usuario"].ToString());
                _ind_productos = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_productos"].ToString());

                _zamba_user_Id = int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString());
            }
            catch(Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda la sugerencia ingresada en un tipo de documento solicitud de libreria
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud()
        {
            try
            {
                Rights.ValidateLogIn(_zamba_user_Id);
                Zamba.Core.Results_Business.InsertResult Resultado;

                Zamba.Core.NewResult Documento = Results_Business.GetNewNewResult(_id_doc);

                foreach (Zamba.Core.IIndex _index in Documento.Indexs)
                {
                    if (_index.ID == _ind_fecha)
                        _index.DataTemp = DateTime.Now.ToString();

                    if (_index.ID == _ind_usuario)
                        _index.DataTemp = _usuario;

                    if (_index.ID == _ind_productos)
                        _index.DataTemp = ListaProductos();
                }

                Resultado = Results_Business.Insert(ref Documento, false, false, false, false, true, false, false);

                if (Resultado != Zamba.Core.Results_Business.InsertResult.Insertado)
                    return false;

                EnviarNotificacion();

                return true;
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return false;
            }  
        }

        /// <summary>
        /// Convierte la lista de articulos en un string
        /// </summary>
        /// <returns></returns>
        private string ListaProductos()
        {           
            StringBuilder sb = new StringBuilder();

            foreach (ArticuloLibreria arti in _listaArticulos)
            {
                if (arti.Unidades > 0)
                {
                    sb.Append(arti.Unidades);

                    if(arti.Unidades == 1)
                        sb.Append(" unidad");
                    else
                        sb.Append(" unidades");

                    sb.Append(" de ");
                    sb.Append(arti.Articulo);
                    sb.Append("\n");
                }

                if (arti.Paquetes > 0)
                {
                    sb.Append(arti.Paquetes);

                    if (arti.Paquetes == 1)
                        sb.Append(" paquete");
                    else
                        sb.Append(" paquetes");

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
            string subject;
            string body;

            to = ConfigurationSettings.AppSettings["mail_from"].ToString();

            subject = "Han enviado una nueva solicitud de libreria";
            body = _usuario + " ha enviado una nueva solicitud de libreria: " + _articulos.Replace(@"\n", "<br>");

            return new EmailNotificacion(_zamba_user_Id).EnviarNotificacion(to, subject, body);
        }
    }
}