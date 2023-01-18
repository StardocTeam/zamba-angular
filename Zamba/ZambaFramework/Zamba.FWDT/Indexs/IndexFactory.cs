using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using Zamba.Servers;

namespace Zamba.FWDT
{
    public class IndexFactory
    {

    //        ''' -----------------------------------------------------------------------------
    //''' <summary>
    //''' Obtiene el Nombre del Indice en base a su ID
    //''' </summary>
    //''' <param name="IndexId">Id del indice que se desea conocer el nombre</param>
    //''' <returns></returns>
    //''' <remarks>
    //''' </remarks>
    //''' <history>
    //''' 	[Hernan]	26/05/2006	Created
    //''' </history>
    //''' -----------------------------------------------------------------------------

            public static string GetIndexName(Int64 IndexId)
        {
            StringBuilder strSelect = new StringBuilder();
            string indexName = string.Empty;
            try
            {
                strSelect.Append("Select Index_Name from Doc_Index where(Index_Id = ");
                strSelect.Append(IndexId);
                strSelect.Append(")");
                indexName =Convert.ToString(Server.get_Con().ExecuteScalar(CommandType.Text, strSelect.ToString()));

                if (string.IsNullOrWhiteSpace(indexName))
                {
                    indexName = string.Empty;
                }

            }catch(Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return indexName;
        }
    }
}
