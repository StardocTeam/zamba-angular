using System;
using System.Data;
using System.Text;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class OtrosServiciosData
    {
        private long _id_doc;
        private long _ind_fecha;
        private long _ind_usuario;
        private long _ind_mensaje;
        private long _ind_servicio;

        public OtrosServiciosData()
        {
            try
            {
                _id_doc = long.Parse(ConfigurationSettings.AppSettings["otros_serv_doc_id"].ToString());
                _ind_fecha = long.Parse(ConfigurationSettings.AppSettings["otros_serv_ind_fecha"].ToString());
                _ind_usuario = long.Parse(ConfigurationSettings.AppSettings["otros_serv_ind_usuario"].ToString());
                _ind_mensaje = long.Parse(ConfigurationSettings.AppSettings["otros_serv_ind_desc"].ToString());
                _ind_servicio = long.Parse(ConfigurationSettings.AppSettings["otros_serv_ind_servicio"].ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        /// <summary>
        /// Obtiene los servicios asociados al indice servicios
        /// </summary>
        /// <returns></returns>
        public DataSet getServicios()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * ");
                sb.Append(" FROM ");
                sb.Append(" SLST_S" + _ind_servicio);
 
                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene un servicio
        /// </summary>
        /// <returns></returns>
        public DataSet getServicioById(long id)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * ");
                sb.Append(" FROM ");
                sb.Append(" SLST_S" + _ind_servicio);
                sb.Append(" WHERE ");
                sb.Append(" CODIGO = " + id);

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Guarda el pedido de servicios ingresado en un tipo de documento "otros servicios"
        /// </summary>
        /// <returns></returns>
        public bool GuardarSolicitud(string usuario, string mensaje, long servicio)
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

                    if (_index.ID == _ind_mensaje)
                        _index.DataTemp = mensaje;

                    if (_index.ID == _ind_servicio)
                        _index.DataTemp = servicio.ToString();
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
