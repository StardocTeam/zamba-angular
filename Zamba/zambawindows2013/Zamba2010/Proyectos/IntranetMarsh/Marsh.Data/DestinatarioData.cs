using System;
using System.Text;
using System.Data;
using Zamba.Core;
using System.Configuration;

namespace Marsh.Data
{
    public class DestinatarioData
    {
        /// <summary>
        /// Obtiene la direccion de email de un destinatario
        /// </summary>
        /// <param name="indice">Numero de indice donde se guarda la direccion de email</param>
        /// <returns></returns>
        public string ObtenerDestinatario(int indice)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            int _iddoc;

            _iddoc = int.Parse(ConfigurationSettings.AppSettings["desti_doc_id"].ToString());

            string destinatario = "";

            try
            {
                sb.Append(" SELECT ");
                sb.Append(" DOC.I" + indice);
                sb.Append(" FROM DOC" + _iddoc + " DOC ");

                destinatario = (string)Zamba.Servers.Server.get_Con(false, true, false).ExecuteScalar(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            return destinatario;
        }
    }
}
