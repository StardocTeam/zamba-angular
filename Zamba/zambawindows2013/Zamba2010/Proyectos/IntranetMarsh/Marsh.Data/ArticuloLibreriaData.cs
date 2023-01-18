using System;
using System.Text;
using Zamba.Core;
using System.Data;
using System.Configuration;

namespace Marsh.Data
{
    public class ArticuloLibreriaData
    {
        private long _ind_productos;

        public ArticuloLibreriaData()
        {
            try
            {
                _ind_productos = long.Parse(ConfigurationSettings.AppSettings["solic_lib_ind_productos_libreria"].ToString());
            }
            catch(Exception ex)
            {
                ZClass.raiseerror(ex);
                return;
            }
        }

        public DataSet Listar()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" * ");
                sb.Append(" FROM ");
                sb.Append(" SLST_S" + _ind_productos);
                sb.Append(" ORDER BY Descripcion ASC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }
    }
}