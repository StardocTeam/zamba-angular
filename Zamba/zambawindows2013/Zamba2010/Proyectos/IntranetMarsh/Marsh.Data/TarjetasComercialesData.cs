using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class TarjetasComercialesData
    {
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_sector;
        private long _ind_cargo;
        private long _ind_telefono;
        private long _ind_email;

        public TarjetasComercialesData()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["tarjetas_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_usuario"].ToString());
                _ind_sector = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_sector"].ToString());
                _ind_cargo = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_cargo"].ToString());
                _ind_telefono = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_telefono"].ToString());
                _ind_email = long.Parse(ConfigurationSettings.AppSettings["tarjetas_ind_email"].ToString());
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
        public bool GuardarSolicitud(string usuario, string cargo, string sector, string telefono, string email)
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

                    if (_index.ID == _ind_sector)
                        _index.DataTemp = sector;

                    if (_index.ID == _ind_cargo)
                        _index.DataTemp = cargo;

                    if (_index.ID == _ind_telefono)
                        _index.DataTemp = telefono;

                    if (_index.ID == _ind_email)
                        _index.DataTemp = email;
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