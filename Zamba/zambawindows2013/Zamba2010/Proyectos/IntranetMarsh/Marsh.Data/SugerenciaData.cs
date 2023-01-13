using System;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class SugerenciaData
    {
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_sugerencia;

        public SugerenciaData()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["sugerencia_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_usuario"].ToString());
                _ind_sugerencia = long.Parse(ConfigurationSettings.AppSettings["sugerencia_ind_sugerencia"].ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Guarda la sugerencia ingresada en un tipo de documento sugerencia
        /// </summary>
        /// <returns></returns>
        public bool GuardarSugerencia(string usuario, string sugerencia)
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

                    if (_index.ID == _ind_sugerencia)
                        _index.DataTemp = sugerencia;
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
