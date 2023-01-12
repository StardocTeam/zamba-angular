using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Zamba.Servers;
using System.Data.SqlClient;

namespace Zamba.Services
{
    public class SZQuery : IService
    {
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Feeds;
        }

        /// <summary>
        /// Esto llega de Zamba.OptionsFields.js
        /// </summary>
        /// <param name="query"></param>
        /// <param name="objParams"></param>
        /// <param name="paramNames"></param>
        /// <returns></returns>
        public DataSet FillSource(string query, object[] objParams, string[] paramNames)
        {
            if (objParams != null && paramNames != null)
            {
                if (objParams.Length != paramNames.Length)
                    throw new Exception("No coincide los nombres de parametros con los valores");

                List<IDbDataParameter> parameters = new List<IDbDataParameter>();
                IDbDataParameter param;
                int paramCount = objParams.Length;
                for (int i = 0; i < paramCount; i++)
                {
                    if (Server.isOracle)
                        throw new Exception("No implementado");
                    else
                        param = new SqlParameter();

                    param.Value = objParams[i];
                    param.ParameterName = paramNames[i];

                    parameters.Add(param);
                }

                return Server.get_Con().ExecuteDataset(CommandType.Text, query, parameters.ToArray());
            }
            else
            {
                return Server.get_Con().ExecuteDataset(CommandType.Text, query, null);
            }
        }
    }
}
