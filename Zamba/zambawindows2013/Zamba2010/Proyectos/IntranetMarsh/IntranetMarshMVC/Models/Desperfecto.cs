using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using System.Net.Mail;
using Marsh.Services;
using Marsh.Bussines;
using Zamba.Core;

namespace IntranetMarshMVC.Models
{    
    public class Desperfecto
    {
        private string _usuario;
        private string _lugar;
        private string _descripcion;

        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_lugar;
        private long _ind_descr;

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

        public Desperfecto()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["solic_serv_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["solic_serv_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["solic_serv_ind_usuario"].ToString());
                _ind_lugar = long.Parse(ConfigurationSettings.AppSettings["solic_serv_ind_lugar"].ToString());
                _ind_descr = long.Parse(ConfigurationSettings.AppSettings["solic_serv_ind_desc"].ToString());

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
            try
            {
                //Rights.ValidateLogIn(_zamba_user_Id);
                Results_Business.InsertResult Resultado;

                NewResult Documento = Results_Business.GetNewNewResult(_id_doc);

                foreach (Zamba.Core.IIndex _index in Documento.Indexs)
                {
                    if (_index.ID == _ind_fecha)
                        _index.DataTemp = DateTime.Now.ToString();

                    if (_index.ID == _ind_usuario)
                        _index.DataTemp = _usuario;

                    if (_index.ID == _ind_lugar)
                        _index.DataTemp = _lugar;

                    if (_index.ID == _ind_descr)
                        _index.DataTemp = _descripcion;
                }

                Resultado = Results_Business.Insert(ref Documento, false,false, false, false, true, false, false);

                if (Resultado != Results_Business.InsertResult.Insertado)
                    return false;

                EnviarNotificacion();

                return true;                
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }            
        }

        /// <summary>
        /// Envia un email avisando que se ingreso un nuevo desperfecto
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string subject;
            string body;

            to = ConfigurationSettings.AppSettings["mail_from"].ToString();

            subject = "Han enviado un nuevo desperfecto";
            body = _usuario + " ha enviado un nuevo desperfecto: " + _descripcion;

            return new EmailNotificacion(_zamba_user_Id).EnviarNotificacion(to, subject, body);
        }
    }
}
