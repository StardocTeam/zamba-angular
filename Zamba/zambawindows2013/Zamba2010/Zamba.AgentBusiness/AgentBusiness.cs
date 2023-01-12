using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Zamba.Core;

namespace Zamba.AgentBusiness
{
    public class AgentBusiness
    {
        public Boolean RegisterUCMActivity(String Client, ref String Result)
        {

            //mandarlo al admin y volar esta parte
            try
            {
                Zamba.Servers.IConnection con1 = Zamba.Servers.Server.get_Con();
                string insertquery = " select 1 from [UCMCLIENTSSet]";
                con1.ExecuteScalar(CommandType.Text, insertquery);

            }
            catch (Exception exc)
            {
                try
                {
                    if (exc.ToString().Contains("no es válido"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE [UCMCLIENTSSet](	[USER_ID] [numeric](18, 0) NOT NULL, 	[C_TIME] [datetime] NULL, 	[U_TIME] [datetime] NULL, 	[WINUSER] [nvarchar](200) NOT NULL, 	[WINPC] [nvarchar](200) NOT NULL, 	[CON_ID] [numeric](18, 0) NOT NULL, 	[TIME_OUT] [numeric](18, 0) NULL, 	[TYPE] [numeric](18, 0) NOT NULL, 	[Client] [nvarchar](100) NOT NULL, 	[UpdateDate] [datetime] NOT NULL, 	[Server] [nvarchar](100) NULL , 	[Base] [nvarchar](100) NULL)";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else if (exc.ToString().Contains("Invalid object name"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE [UCMCLIENTSSet](	[USER_ID] [numeric](18, 0) NOT NULL, 	[C_TIME] [datetime] NULL, 	[U_TIME] [datetime] NULL, 	[WINUSER] [nvarchar](200) NOT NULL, 	[WINPC] [nvarchar](200) NOT NULL, 	[CON_ID] [numeric](18, 0) NOT NULL, 	[TIME_OUT] [numeric](18, 0) NULL, 	[TYPE] [numeric](18, 0) NOT NULL, 	[Client] [nvarchar](100) NOT NULL, 	[UpdateDate] [datetime] NOT NULL, 	[Server] [nvarchar](100) NULL , 	[Base] [nvarchar](100) NULL)";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else if (exc.ToString().Contains("table or view does not exist"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE UCMCLIENTSSet(USER_ID number(18, 0) NOT NULL, [C_TIME] [date] NULL, 	[U_TIME] [date] NULL, 	[WINUSER] [varchar2](200) NOT NULL, 	[WINPC] [varchar2](200) NOT NULL, 	[CON_ID] [number](18, 0) NOT NULL, 	[TIME_OUT] [number](18, 0) NULL, 	[TYPE] [number](18, 0) NOT NULL, 	[Client] [varchar2](100) NOT NULL, 	[UpdateDate] [date] NOT NULL, 	[Server] [varchar2](100) NULL , 	[Base] [varchar2](100) NULL)";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else
                    {
                        ZClass.raiseerror(exc);
                    }
                }
                catch (Exception exc2)
                {
                    ZClass.raiseerror(exc2);
                }
            }

                  try
            {
                Zamba.Servers.IConnection con1 = Zamba.Servers.Server.get_Con();
                string insertquery = " select 1 from [ILMCLIENTSet]";
                con1.ExecuteScalar(CommandType.Text, insertquery);

                try
                {
                    String currentFlagReStartILMClientSet = Zamba.Core.ZOptBusiness.GetValue("ReStartILMClientSet");
                    if (currentFlagReStartILMClientSet == null || currentFlagReStartILMClientSet == "")
                    {
                        Zamba.Core.ZOptBusiness.Insert("ReStartILMClientSet", "False");
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string deletequery = " delete from [ILMCLIENTSet]";
                        con3.ExecuteNonQuery(CommandType.Text, deletequery);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }


            }
            catch (Exception exc)
            {
                try
                {

                    if (exc.ToString().Contains("no es válido"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE [ILMClientSet](	[UserId] [numeric](18, 0) NOT NULL,	[UserName] [nvarchar](500) NOT NULL,	[Year] [numeric](18, 0) NOT NULL,	[Month] [numeric](18, 0) NOT NULL,	[Day] [numeric](18, 0) NOT NULL,	[Hour] [numeric](18, 0) NOT NULL,	[Type] [numeric](18, 0) NOT NULL,	[UpdateDate] [datetime] NOT NULL,	[CrDate] [datetime] NOT NULL,	[Client] [nvarchar](150) NOT NULL, [Server] [nvarchar](150) NOT NULL,	[Base] [nvarchar](150) NOT NULL,	[CodigoMail] [nvarchar](500) NOT NULL,	[doc_id] [numeric](18, 0) NOT NULL,	[DocTypeId] [numeric](18, 0) NOT NULL) ";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else if (exc.ToString().Contains("Invalid object name"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE [ILMClientSet](	[UserId] [numeric](18, 0) NOT NULL,	[UserName] [nvarchar](500) NOT NULL,	[Year] [numeric](18, 0) NOT NULL,	[Month] [numeric](18, 0) NOT NULL,	[Day] [numeric](18, 0) NOT NULL,	[Hour] [numeric](18, 0) NOT NULL,	[Type] [numeric](18, 0) NOT NULL,	[UpdateDate] [datetime] NOT NULL,	[CrDate] [datetime] NOT NULL,	[Client] [nvarchar](150) NOT NULL, [Server] [nvarchar](150) NOT NULL,	[Base] [nvarchar](150) NOT NULL,	[CodigoMail] [nvarchar](500) NOT NULL,	[doc_id] [numeric](18, 0) NOT NULL,	[DocTypeId] [numeric](18, 0) NOT NULL) ";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else if (exc.ToString().Contains("table or view does not exist"))
                    {
                        Zamba.Servers.IConnection con3 = Zamba.Servers.Server.get_Con();
                        string createquery = "CREATE TABLE [ILMClientSet](	[UserId] [number](18, 0) NOT NULL,	[UserName] [varchar2](500) NOT NULL,	[Year] [number](18, 0) NOT NULL,	[Month] [number](18, 0) NOT NULL,	[Day] [number](18, 0) NOT NULL,	[Hour] [number](18, 0) NOT NULL,	[Type] [number](18, 0) NOT NULL,	[UpdateDate] [Date] NOT NULL,	[CrDate] [Date] NOT NULL,	[Client] [varchar2](150) NOT NULL, [Server] [varchar2](150) NOT NULL,	[Base] [varchar2](150) NOT NULL,	[CodigoMail] [varchar2](500) NOT NULL,	[doc_id] [number](18, 0) NOT NULL,	[DocTypeId] [number](18, 0) NOT NULL) ";
                        con3.ExecuteNonQuery(CommandType.Text, createquery);

                    }
                    else
                    {
                        ZClass.raiseerror(exc);
                    }
                }
                catch (Exception exc1)
                {
                    ZClass.raiseerror(exc1);
                }
            }



            try
            {

                Trace.WriteLine("Registrando Actividad");
         
                String query1 = "select nombres, apellido, Name,Id, Type as TipoLicencia, Q as Cantidad from (SELECT distinct user_id, type, count(1) as Q FROM UCM group by user_id, type) as Sub inner join usrtable u on Sub.user_id = u.id  where Sub.Q > 1";

                String query2 = "SELECT count(1) as Cantidad,CASE WHEN Type = 0 THEN 'Documental' WHEN  Type = 1 then 'Workflow' else 'Otro' END as [TipoLicencia] FROM UCM as LicenciasxTipogroup group by type";
                String query3 = "select count(1) as Cantidad,CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia] FROM UCM group by type,user_id) as LicenciasxTipoSinUSuariosDup group by [TipoLicencia]";
                String query4 = "select count(1) as Cantidad,CASE WHEN [TipoLicencia] = 0 THEN 'Documental' WHEN  [TipoLicencia] = 1 then 'Workflow' else 'Otro' END from (SELECT count(1) as Cantidad,TYPE as [TipoLicencia] FROM UCM group by type,user_id, winuser) as LicenciasxTipoSinUSuariosDupxPC group by [TipoLicencia]";

                String query5 = "select count(1) as CantidadDeUsuarios from (SELECT distinct user_id FROM UCM) as CentidadUsuarios";
                String query6 = "select count(1) as CantidadDePCs from (SELECT distinct winpc FROM UCM) as CantidadPcs";
                String query7 = "select count(1) as CantidadDeUsuarioWindows from (SELECT distinct winuser FROM UCM) as CantidadUsuariosWindows";



                String query8 = "select nombres, apellido, Name,ID,C_TIME,U_TIME,WINUSER,WINPC,CON_ID,TIME_OUT,TYPE from ucm c inner join usrtable u on c.user_id = u.id";

                //////--Usuarios a Verificar, o Colgados. No deberia haber un usuario mas de una vez.
                //DataSet Ds1 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query1);
                //Ds1.Tables[0].TableName = "UsuariosConMultiplesConexiones";
                //////--Cantidad de Licencias por Tipo
                //DataSet Ds2 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query2);
                //Ds2.Tables[0].TableName = "LicenciasXTipo";
                //////--Cantidad de Licencias x Tipo, Sin Duplicados de Usuarios
                //DataSet Ds3 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query3);
                //Ds3.Tables[0].TableName = "LicenciasXTipoSinUsuariosDuplicados";
                //////--Cantidad de Licencias x Tipo, Sin Duplicados de Usuarios, Si estan en diferente PC
                //DataSet Ds4 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query4);
                //Ds4.Tables[0].TableName = "LicenciasXTipoSinUsuariosDuplicadosxPC";
                //////--Cantidad de usuarios
                //DataSet Ds5 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query5);
                //Ds5.Tables[0].TableName = "CantidadUsuarios";
                //////--Cantidad de PCs
                //DataSet Ds6 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query6);
                //Ds6.Tables[0].TableName = "CantidadPcs";
                //////--Cantidad de UsuariosWindows
                //DataSet Ds7 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query7);
                //Ds7.Tables[0].TableName = "CantidadUsuariosWindows";
                ////--Detalle UCM
                DataSet Ds8 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query8);
                Ds8.Tables[0].TableName = "DetalleConexiones";



                String QueryILM1 = "";
                String QueryILM2 = "";
                DataSet ILMDs1 = null;
                DataSet ILMDs2 = null;

                try
                {

                    switch (Client)
                    {
                        case "Boston":
                            QueryILM1 = "select distinct top 5000  u.ID,u.name, YEAR(i.crdate) as Year,MONTH(i.crdate)as Month,DAY(i.crdate)as DAY, datepart(hh,i.crdate) as Hour,i.crdate,t.doc_id,t.doc_type_id,i.i1145 as CodigoMail  from USRTABLE u inner join doc_t1011 t on t.PLATTER_ID = u.ID inner join DOC_I1011 i on t.doc_id = i.doc_id where DOC_FILE like '%.msg%' and YEAR(i.crdate) > 2011  and i1145 <> ''  and t.doc_id not in (select doc_id from ilmclientset)  ";
                            QueryILM2 = "select distinct top 5000  u.ID,u.name, YEAR(i.crdate) as Year,MONTH(i.crdate)as Month,DAY(i.crdate)as DAY, datepart(hh,i.crdate) as Hour,i.crdate,t.doc_id,t.doc_type_id,i.i1145 as CodigoMail  from USRTABLE u inner join doc_t1021 t on t.PLATTER_ID = u.ID inner join DOC_I1021 i on t.doc_id = i.doc_id where DOC_FILE like '%.msg%' and YEAR(i.crdate) > 2011  and i1145 <> ''  and t.doc_id not in (select doc_id from ilmclientset) ";

                            ILMDs1 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryILM1);
                            ILMDs2 = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, QueryILM2);


                            try
                            {
                                Zamba.Servers.IConnection con1 = Zamba.Servers.Server.get_Con();
                                String LocalILMInsert1 = "INSERT INTO [ILMClientSet] ([UserId],[UserName] ,[Year] ,[Month],[Day] ,[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId])( select distinct top 5000  u.ID,u.name,YEAR(i.crdate),Month(i.crdate),Day(i.crdate),datepart(hh,i.crdate),82,getdate(),i.crdate,'Boston','','',i.i1145,t.doc_id,1011 from USRTABLE u inner join doc_t1011 t on t.PLATTER_ID = u.ID inner join DOC_I1011 i on t.doc_id = i.doc_id  where DOC_FILE like '%.msg%' and YEAR(i.crdate) > 2011 and i1145 <> '' and t.doc_id not in (select doc_id from ilmclientset) )";
                                con1.ExecuteNonQuery(CommandType.Text, LocalILMInsert1);
                                Zamba.Servers.IConnection con2 = Zamba.Servers.Server.get_Con();
                                String LocalILMInsert2 = "INSERT INTO [ILMClientSet] ([UserId],[UserName] ,[Year] ,[Month],[Day] ,[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId])( select distinct top 5000  u.ID,u.name,YEAR(i.crdate),Month(i.crdate),Day(i.crdate),datepart(hh,i.crdate),82,getdate(),i.crdate,'Boston','','',i.i1145,t.doc_id,1021 from USRTABLE u inner join doc_t1021 t on t.PLATTER_ID = u.ID inner join DOC_I1021 i on t.doc_id = i.doc_id  where DOC_FILE like '%.msg%' and YEAR(i.crdate) > 2011 and i1145 <> '' and t.doc_id not in (select doc_id from ilmclientset) )";
                                con2.ExecuteNonQuery(CommandType.Text, LocalILMInsert2);
                            }
                            catch (Exception exc)
                            {
                                ZClass.raiseerror(exc);
                                Trace.WriteLine(exc.ToString());
                            }

                            break;
                        case "Marsh":
                            break;
                        case "HDI":
                            break;
                        case "HDIProduccion":
                            break;
                        case "Parana":
                            break;
                        case "AysaGDI":
                            break;
                        case "AysaGEC":
                            break;
                        case "AysaDAL":
                            break;
                        case "GDI":
                            break;
                        case "GEC":
                            break;
                        case "DAL":
                            break;
                        case "BPN":
                            break;
                        case "Duke":
                            break;
                        case "Liberty":
                            break;
                    }


                }
                catch (Exception exc)
                {
                    ZClass.raiseerror(exc);
                    Trace.WriteLine(exc.ToString());
                }







                try
                {
                    foreach (DataRow R in Ds8.Tables[0].Rows)
                    {

                        Int64 Userid = Int64.Parse(R["ID"].ToString());
                        DateTime ctime = DateTime.Parse(R["C_TIME"].ToString());
                        DateTime utime = DateTime.Parse(R["U_TIME"].ToString());
                        string wuser = R["WINUSER"].ToString();
                        string wpc = R["WINPC"].ToString();
                        Int64 cid = Int64.Parse(R["CON_ID"].ToString());
                        Int64 tout = Int64.Parse(R["TIME_OUT"].ToString());
                        int type = int.Parse(R["TYPE"].ToString());

                        Zamba.Servers.IConnection con1 = Zamba.Servers.Server.get_Con();
                        string insertquery = " INSERT INTO [UCMCLIENTSSet] ([USER_ID]           ,[C_TIME]           ,[U_TIME]           ,[WINUSER]           ,[WINPC]           ,[CON_ID]           ,[TIME_OUT]           ,[TYPE]           ,[Client], Server, Base ,[UpdateDate])      VALUES (" + Userid + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(ctime.ToString()) + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(utime.ToString()) + ",'" + wuser + "','" + wpc + "'," + cid + "," + tout + "," + type + ",'" + Client + "','" + Zamba.Servers.Server.AppConfig.SERVER + "','" + Zamba.Servers.Server.AppConfig.DB + "'," + Zamba.Servers.Server.get_Con().ConvertDateTime(DateTime.Now.ToString()) + ")";
                        con1.ExecuteNonQuery(CommandType.Text, insertquery);
                    }
                }
                catch (Exception exc)
                {
                    ZClass.raiseerror(exc);
                    Trace.WriteLine(exc.ToString());
                }



                String WSResult = string.Empty;
                String WSResult1 = string.Empty;
                String WSResult2 = string.Empty;

                try
                {
                    //                Url = @"http://localhost:56524/ws/AgentService.svc?wsdl";
                    AgentServiceReference.AgentServiceClient ASC = new AgentServiceReference.AgentServiceClient();
                    using (ASC)
                    {
                        WSResult = ASC.SaveUCMDataSet(Ds8, Client, DateTime.Now, Zamba.Servers.Server.AppConfig.SERVER, Zamba.Servers.Server.AppConfig.DB);

                        try
                        {
                            WSResult = ASC.SaveILMDataSet(ILMDs1, Client, DateTime.Now, Zamba.Servers.Server.AppConfig.SERVER, Zamba.Servers.Server.AppConfig.DB);
                            WSResult = ASC.SaveILMDataSet(ILMDs2, Client, DateTime.Now, Zamba.Servers.Server.AppConfig.SERVER, Zamba.Servers.Server.AppConfig.DB);

                        }
                        catch (Exception exc)
                        {
                            ZClass.raiseerror(exc);
                            Trace.WriteLine(exc.ToString());
                        }
                    }

                    if (WSResult.ToLower().Contains("error")) throw new Exception(WSResult);

                    Trace.WriteLine("WS: " + WSResult);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);

                    Int64 RowsCount = 0;
                    Int64 RowsCount1 = 0;
                    Int64 RowsCount2 = 0;

                    try
                    {
                        foreach (DataRow R in Ds8.Tables[0].Rows)
                        {
                            RowsCount++;
                            Int64 Userid = Int64.Parse(R["ID"].ToString());
                            DateTime ctime = DateTime.Parse(R["C_TIME"].ToString());
                            DateTime utime = DateTime.Parse(R["U_TIME"].ToString());
                            string wuser = R["WINUSER"].ToString();
                            string wpc = R["WINPC"].ToString();
                            Int64 cid = Int64.Parse(R["CON_ID"].ToString());
                            Int64 tout = Int64.Parse(R["TIME_OUT"].ToString());
                            int type = int.Parse(R["TYPE"].ToString());

                            Zamba.Servers.IConnection con = Zamba.Servers.Server.get_Con(0, "www.stardoc.com.ar,1437", "ZambaStardoc", "sa", "doc", true, true);
                            string insertquery = " INSERT INTO [UCMCLIENTSSet] ([USER_ID]           ,[C_TIME]           ,[U_TIME]           ,[WINUSER]           ,[WINPC]           ,[CON_ID]           ,[TIME_OUT]           ,[TYPE]           ,[Client], Server, Base ,[UpdateDate])      VALUES (" + Userid + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(ctime.ToString()) + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(utime.ToString()) + ",'" + wuser + "','" + wpc + "'," + cid + "," + tout + "," + type + ",'" + Client + "','" + Zamba.Servers.Server.AppConfig.SERVER + "','" + Zamba.Servers.Server.AppConfig.DB + "'," + Zamba.Servers.Server.get_Con().ConvertDateTime(DateTime.Now.ToString()) + ")";
                            con.ExecuteNonQuery(CommandType.Text, insertquery);
                        }
                        WSResult = "Registros Insertados: " + RowsCount;



                        try
                        {

                            // QueryILM1 = " u.ID,u.name, YEAR(i.crdate) as Year,MONTH(i.crdate)as Month,DAY(i.crdate)as DAY 
                            //([UserId],[UserName] ,[Year] ,[Month],[Day] ,[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId])(
                            //distinct u.ID,u.name,YEAR(i.crdate),Month(i.crdate),Day(i.crdate),datepart(hh,i.crdate),82,getdate(),i.crdate,'Boston','','',i.i1145,t.doc_id,1011

                            foreach (DataRow R in ILMDs1.Tables[0].Rows)
                            {
                                RowsCount1++;
                                Int64 Userid = Int64.Parse(R["ID"].ToString());
                                string wuser = R["name"].ToString();
                                String Year = R["Year"].ToString();
                                String Month = R["Month"].ToString();
                                String Day = R["Day"].ToString();
                                String Hour = R["Hour"].ToString();
                                DateTime crdate = DateTime.Parse(R["crdate"].ToString());
                                String CodigoMail = R["CodigoMail"].ToString();
                                String Doc_Id = R["doc_id"].ToString();
                                String Doc_Type_Id = R["doc_type_id"].ToString();

                                Zamba.Servers.IConnection con = Zamba.Servers.Server.get_Con(0, "www.stardoc.com.ar,1437", "ZambaStardoc", "sa", "doc", true, true);
                                string insertquery = "INSERT INTO [ILMClientSet] ([UserId],[UserName] ,[Year] ,[Month],[Day] ,[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId]) values (" + Userid + ",'" + wuser + "'," + Year + "," + Month + "," + Day + "," + Hour + "," + 82 + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(DateTime.Now.ToString()) + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(crdate.ToString()) + ",'" + Client + "','" + Zamba.Servers.Server.AppConfig.SERVER + "','" + Zamba.Servers.Server.AppConfig.DB + "','" + CodigoMail + "'," + Doc_Id + "," + Doc_Type_Id + ")";
                                con.ExecuteNonQuery(CommandType.Text, insertquery);
                            }
                            WSResult1 = "Registros Insertados: " + RowsCount1;

                            foreach (DataRow R in ILMDs2.Tables[0].Rows)
                            {
                                RowsCount2++;
                                Int64 Userid = Int64.Parse(R["ID"].ToString());
                                string wuser = R["name"].ToString();
                                String Year = R["Year"].ToString();
                                String Month = R["Month"].ToString();
                                String Day = R["Day"].ToString();
                                String Hour = R["Hour"].ToString();
                                DateTime crdate = DateTime.Parse(R["crdate"].ToString());
                                String CodigoMail = R["CodigoMail"].ToString();
                                String Doc_Id = R["doc_id"].ToString();
                                String Doc_Type_Id = R["doc_type_id"].ToString();

                                Zamba.Servers.IConnection con = Zamba.Servers.Server.get_Con(0, "www.stardoc.com.ar,1437", "ZambaStardoc", "sa", "doc", true, true);
                                string insertquery = "INSERT INTO [ILMClientSet] ([UserId],[UserName] ,[Year] ,[Month],[Day] ,[Hour],[Type],[UpdateDate],[CrDate],[Client],[Server],[Base],[CodigoMail],[doc_id],[DocTypeId]) values (" + Userid + ",'" + wuser + "'," + Year + "," + Month + "," + Day + "," + Hour + "," + 82 + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(DateTime.Now.ToString()) + "," + Zamba.Servers.Server.get_Con().ConvertDateTime(crdate.ToString()) + ",'" + Client + "','" + Zamba.Servers.Server.AppConfig.SERVER + "','" + Zamba.Servers.Server.AppConfig.DB + "','" + CodigoMail + "'," + Doc_Id + "," + Doc_Type_Id + ")";
                                con.ExecuteNonQuery(CommandType.Text, insertquery);
                            }
                            WSResult2 = "Registros Insertados: " + RowsCount2;


                        }
                        catch (Exception exc)
                        {
                            ZClass.raiseerror(exc);
                            Trace.WriteLine(exc.ToString());
                        }


                    }
                    catch (Exception exc)
                    {
                        ZClass.raiseerror(exc);
                        Trace.WriteLine(exc.ToString());
                    }

                }

                //            ASC.SaveUCMCount(Client, LicenceType, LicenceCount, DateTime.Now);

                Result =  WSResult + WSResult1 + WSResult2;
                return true;
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                Result = ex.ToString();
                return false;
            }
        }

        public Boolean RegisterBadDesignActivity(String Client, ref String Result)
        {
            try
            {
                Zamba.Servers.IConnection con1 = Zamba.Servers.Server.get_Con();
                string insertquery = "select * from wfrules where1 from [UCMCLIENTSSet]";
                con1.ExecuteScalar(CommandType.Text, insertquery);

            }
            catch (Exception exc)
            {
         
            }
        }
    
    }
}
