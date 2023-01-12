using System;
using System.Text;
using System.Data;
using Zamba.Core;

namespace Marsh.Data
{
    public class FormulariosData
    {
        private long _iddoc;
        private long _ind_categ;

        public FormulariosData(long iddoc, long ind_categ)
        {
            _iddoc = iddoc;
            _ind_categ = ind_categ;
        }
     
        /// <summary>
        /// Obtiene un formulario
        /// </summary>
        /// <param name="id">Id del formulario a obtener</param>
        /// <returns></returns>
        public DataSet getFormularioById(int id)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" INNER JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
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
        /// Obtiene todas los formularios y solicitudes publicados
        /// </summary>
        /// <returns></returns>
        public DataSet getFormularios()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" INNER JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
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

        /// <summary>
        /// Busca formularios
        /// </summary>
        /// <param name="buscar">String a buscar en el indice del formulario</param>
        /// <param name="indice">Indice en el cual realizar la busqueda</param>
        /// <returns></returns>
        public DataSet Buscar(string buscar, string categ, long indice)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" INNER JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
                sb.Append(" INNER JOIN SLST_S" + _ind_categ + " CAT ON CAT.Codigo = DOC.I" + _ind_categ);
                sb.Append(" WHERE LOWER(I" + indice + ") LIKE '%" + buscar.Trim().ToLower() + "%'");

                if (categ != "")
                    sb.Append(" AND LOWER(CAT.Descripcion) = '" + categ.ToLower() + "'");

                sb.Append(" ORDER BY DOC.DOC_ID DESC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene una lista filtrada de formularios
        /// </summary>
        /// <param name="categ">Categoria por la cual filtrar</param>
        /// <param name="_ind_categ">Indice donde se guarda la categoria</param>
        /// <returns></returns>
        public DataSet Filtrar(string categ)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.*, VOL.*, ");
                sb.Append(" CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" INNER JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
                sb.Append(" INNER JOIN SLST_S" + _ind_categ + " CAT ON CAT.Codigo = DOC.I" + _ind_categ);
                sb.Append(" WHERE LOWER(CAT.Descripcion) = '" + categ.Trim().ToLower() + "'");
                sb.Append(" ORDER BY DOC.DOC_ID DESC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene una lista con todas las categorias de los formularios
        /// </summary>
        /// <returns></returns>
        public DataSet ListarCategorias()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DISTINCT CAT.Descripcion AS Categoria ");
                sb.Append(" FROM DOC" + _iddoc + " DOC ");
                sb.Append(" INNER JOIN DISK_VOLUME VOL ON DOC.VOL_ID = VOL.DISK_VOL_ID ");
                sb.Append(" INNER JOIN SLST_S" + _ind_categ + " CAT ON CAT.Codigo = DOC.I" + _ind_categ);
                sb.Append(" ORDER BY CAT.Descripcion ASC ");

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
