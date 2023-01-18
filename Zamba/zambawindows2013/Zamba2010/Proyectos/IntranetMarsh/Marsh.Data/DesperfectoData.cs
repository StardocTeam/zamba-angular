using System;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class DesperfectoData
    {
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_lugar;
        private long _ind_descr;

        public DesperfectoData()
        {
            try
            {
                long.TryParse(ConfigurationSettings.AppSettings["solic_serv_doc_id"].ToString(), out _id_doc);
                long.TryParse(ConfigurationSettings.AppSettings["solic_serv_ind_fecha"].ToString(), out _ind_fecha);
                long.TryParse(ConfigurationSettings.AppSettings["solic_serv_ind_usuario"].ToString(), out _ind_usuario);
                long.TryParse(ConfigurationSettings.AppSettings["solic_serv_ind_lugar"].ToString(), out _ind_lugar);
                long.TryParse(ConfigurationSettings.AppSettings["solic_serv_ind_desc"].ToString(), out _ind_descr);
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
        public bool GuardarSolicitud(string usuario, string lugar, string descripcion)
        {
            try
            {
                InsertResult Resultado;

                NewResult Documento = Results_Business.GetNewNewResult(_id_doc);

                foreach (IIndex _index in Documento.Indexs)
                {
                    if (_index.ID == _ind_fecha)
                        _index.DataTemp = DateTime.Now.ToString();

                    if (_index.ID == _ind_usuario)
                        _index.DataTemp = usuario;

                    if (_index.ID == _ind_lugar)
                        _index.DataTemp = lugar;

                    if (_index.ID == _ind_descr)
                        _index.DataTemp = descripcion;
                }

                Resultado = Results_Business.Insert(ref Documento, false, false, false, false, true, false, false,false);

                if (Resultado != InsertResult.Insertado)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }
    }
}
