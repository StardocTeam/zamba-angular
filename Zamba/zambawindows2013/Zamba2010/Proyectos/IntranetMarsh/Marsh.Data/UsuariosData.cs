using System;
using System.Text;
using System.Data;
using Zamba.Core;

namespace Marsh.Data
{
    public class UsuariosData
    {
        /// <summary>
        /// Obtiene una lista de usuarios cuyo apellido comience con el string indicado
        /// </summary>
        /// <param name="buscar">String a buscar como comiezo del apellido</param>
        /// <param name="buscaren">Especifica en que campo realizar la busqueda</param>
        /// <returns>Dataset con los datos de los usuarios encontradoss</returns>
        public DataSet searchUsuarios(string buscar, string buscaren)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * FROM ZVW_INTRANET_100_USUARIOS_ZMB ");

                if (buscaren.ToLower() == "apenom")
                    sb.Append(" WHERE LOWER(nombrecompleto) LIKE '%" + buscar.ToLower().Trim() + "%'");

                if (buscaren.ToLower() == "sector")
                    sb.Append(" WHERE LOWER(puesto) LIKE '%" + buscar.ToLower().Trim() + "%'");

                if (buscaren.ToLower() == "interno")
                    sb.Append(" WHERE LOWER(telefono) LIKE '%" + buscar.ToLower().Trim() + "%'");

                sb.Append(" ORDER BY nombrecompleto ASC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene una lista de usuarios cuya inicial del apellido coincida con el char indicado
        /// </summary>
        /// <param name="inicial">Inicial del apellido a buscar</param>
        /// <returns>Dataset con los datos de los usuarios encontradoss</returns>
        public DataSet getUsuariosPorInicial(char incicial)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * FROM ZVW_INTRANET_100_USUARIOS_ZMB  ");
                sb.Append(" WHERE LOWER(nombrecompleto) LIKE '" + incicial.ToString().ToLower() + "%'");
                sb.Append(" ORDER BY nombrecompleto ASC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene los datos completos de un usuario
        /// </summary>
        /// <param name="nombre_apellido">Nombre y apellido del usuario a buscar</param>
        public DataSet getUserData(string nombre_apellido)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * FROM ZVW_INTRANET_100_USUARIOS_ZMB ");
                sb.Append(" WHERE LOWER(nombrecompleto) = '" + nombre_apellido.ToString().ToLower() + "'");
                sb.Append(" ORDER BY nombrecompleto ASC ");

                ds = Zamba.Servers.Server.get_Con(false, true, false).ExecuteDataset(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return ds;
        }

        /// <summary>
        /// Obtiene todos los usuarios de la base
        /// </summary>
        /// <returns></returns>
        public DataSet getUsersList()
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append(" SELECT * FROM ZVW_INTRANET_100_USUARIOS_ZMB ");
                sb.Append(" ORDER BY TRIM(nombrecompleto) ASC ");

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