using System;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class SolicitudLibreriaData
    {
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_productos;

        public SolicitudLibreriaData()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["solic_lib_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_usuario"].ToString());
                _ind_productos = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_productos"].ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda la sugerencia ingresada en un tipo de documento solicitud de libreria
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud(string usuario, string productos)
        {
            try
            {
                InsertResult Resultado;

                NewResult Documento = Results_Business.GetNewNewResult(_id_doc);

                foreach (Zamba.Core.IIndex _index in Documento.Indexs)
                {
                    if (_index.ID == _ind_fecha)
                        _index.DataTemp = DateTime.Now.ToString();

                    if (_index.ID == _ind_usuario)
                        _index.DataTemp = usuario;

                    if (_index.ID == _ind_productos)
                        _index.DataTemp = productos;
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
