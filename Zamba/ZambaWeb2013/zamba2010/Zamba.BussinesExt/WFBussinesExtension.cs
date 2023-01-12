using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Zamba.DataExt;
using Zamba.Core;
using Zamba.Services;

namespace Zamba.BussinesExt
{
    public class WFBussinesExtension
    {
        //Formato: recividos|procesados|registro con error|error
        const string RETURNVALUE_FORMAT = "{0}|{1}|{2}|{3}";

        public static string BeginWFProcess(long processID, DataSet paramenters, IWFServiceZvarConfig[] paramConfiguration, long userID)
        {
            try
            {
                IUser user = new SRights().ValidateLogIn((int)userID, ClientType.Service);

                if (user != null)
                {
                    switch (processID)
                    {
                        //Ultima fecha de ejecucion
                        case 781:
                            string lastEDate = GetLastExecutionDate();
                            if (string.IsNullOrEmpty(lastEDate))
                            {
                                return "01-01-0001";
                            }
                            return GetLastExecutionDate();
                        //Envio de muestras
                        case 789:
                            return InsertRecivedData(paramenters, paramConfiguration);
                        default:
                            return "El proceso " + processID.ToString() + " aun no ha sido implementado";
                    }
                }
                else
                {
                    return "Usuario no valido";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return "Error no esperado";
            }
        }

        private static string InsertRecivedData(DataSet paramenters, IWFServiceZvarConfig[] paramConfiguration)
        {
            long successCounter = 0;
            long incomingCounter = 0;
            
            StringBuilder sbReturnValue = new StringBuilder();
            try
            {
                Dictionary<string,IWFServiceZvarConfig> config = GetZVarConfig(paramConfiguration);

                DataTable recivedDT = paramenters.Tables[config["ListaMuestras"].TableName];
                if (recivedDT == null|| recivedDT.Rows.Count == 0)
                {
                    throw new Exception("La tabla: " + config["ListaMuestras"].TableName + " esta vacia.");
                }

                incomingCounter = recivedDT.Rows.Count;
                
                foreach (DataRow row in recivedDT.Rows)
                {
                    //Muestra(row[0])no puede ser vacio.
                    if (row[0] == null || row[0] is DBNull)
                    {
                        return sbReturnValue.AppendFormat(RETURNVALUE_FORMAT, incomingCounter, 0, "Muestra vacio, Componente:" + row[8], "Muestra esta vacio").ToString();
                    }
                    //Punto(row[6]) no puede ser vacio.
                    if (row[6] == null || row[6] is DBNull)
                    {
                        return sbReturnValue.AppendFormat(RETURNVALUE_FORMAT, incomingCounter, 0, "Muestra: "+ row[0] +", Componente:" + row[8], "Punto esta vacio").ToString();
                    }

                    successCounter++;
                }

                if (successCounter == recivedDT.Rows.Count)
                {
                    long transactionID = InsertExecutionDates(ref paramenters, ref config);

                    WFFactoryExtension.InsertIncomingRows(ref recivedDT,transactionID);

                    return sbReturnValue.AppendFormat(RETURNVALUE_FORMAT, incomingCounter, successCounter, string.Empty, string.Empty).ToString();
                }

                return sbReturnValue.AppendFormat(RETURNVALUE_FORMAT, incomingCounter, 0, string.Empty, "Error no esperado").ToString();
            }
            catch (Exception ex )
            {
                ZClass.raiseerror(ex);
                return sbReturnValue.AppendFormat(RETURNVALUE_FORMAT,incomingCounter,0,string.Empty,"Error no esperado").ToString();
            }
        }

        private static long InsertExecutionDates(ref DataSet paramenters, ref Dictionary<string, IWFServiceZvarConfig> config)
        {
            string recivedHasta = config["FechasHasta"].ValueToAssign.ToString();

            if (string.IsNullOrEmpty(recivedHasta))
            {
                throw new Exception("No encontrada fechas desde y hasta");
            }

            string recivedDesde = config["FechasDesde"].ValueToAssign.ToString();

            if (string.IsNullOrEmpty(recivedDesde))
            {
                throw new Exception("No encontrada fecha desde");
            }

            WFFactoryExtension.InsertExecutionDates(recivedHasta, recivedDesde);
            return WFFactoryExtension.GetLastTransaction();
        }

        private static Dictionary<string, IWFServiceZvarConfig> GetZVarConfig(IWFServiceZvarConfig[] paramConfiguration)
        {
            Dictionary<string, IWFServiceZvarConfig> returnDictionary = new Dictionary<string, IWFServiceZvarConfig>();

            foreach (IWFServiceZvarConfig item in paramConfiguration)
            {
                returnDictionary.Add(item.ZvarName, item);
            }

            return returnDictionary;
        }

        private static string GetLastExecutionDate()
        {
            return WFFactoryExtension.GetLastExecutionDate();
        }
    }
}
