using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Zamba.Services;
using Marsh.Services;
using Marsh.Bussines;

namespace IntranetMarshMVC.Models
{
    public class Sugerencia
    {
        private string _usuario;
        private string _mensaje;

        private int _zamba_user_Id;
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_sugerencia;

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

        public Sugerencia()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["sugerencia_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_usuario"].ToString());
                _ind_sugerencia = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_sugerencia"].ToString());

                _zamba_user_Id = int.Parse(ConfigurationSettings.AppSettings["zamba_user_id"].ToString());
            }
            catch(Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda la sugerencia ingresada en un tipo de documento sugerencia
        /// </summary>
        /// <returns></returns>
        public bool GuardarSugerencia()
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

                    if (_index.ID == _ind_sugerencia)
                        _index.DataTemp = _mensaje;
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
        /// Envia un email avisando que se ingreso una nueva sugerencia
        /// </summary>
        /// <returns></returns>
        private bool EnviarNotificacion()
        {
            string to;
            string subject;
            string body;

            to = ConfigurationSettings.AppSettings["mail_from"].ToString();

            subject = "Han enviado una nueva sugerencia";
            body = _usuario + " ha enviado una nueva sugerencia: " + _mensaje;

            return new EmailNotificacion(_zamba_user_Id).EnviarNotificacion(to, subject, body);
        }
    }
}