using System;
using System.Text;
using System.Data;
using Zamba.Core;

namespace Marsh.Data
{
    public class NoticiasData
    {
        private long _iddoc;
        private long _ind_categ;

        public NoticiasData(long iddoc, long idcateg)
        {
            _iddoc = iddoc;
            _ind_categ = idcateg;
        }

        /// <summary>
        /// Obtiene una noticia en particular
        /// </summary>
        /// <param name="id">Id de la noticia a obtener</param>
        /// <returns></returns>
        public DataSet getNoticiaById(int id)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" LEFT  JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
                sb.Append(" INNER JOIN SLST_S" + _ind_categ + " CAT ON CAT.Codigo = DOC.I" + _ind_categ);
                sb.Append(" WHERE DOC_ID = " + id);

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene todas las noticias publicadas
        /// </summary>
        /// <returns></returns>
        public DataSet getNoticias()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" LEFT  JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
                sb.Append(" INNER JOIN SLST_S" + _ind_categ + " CAT ON CAT.Codigo = DOC.I" + _ind_categ);
                sb.Append(" ORDER BY DOC.DOC_ID DESC ");

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
