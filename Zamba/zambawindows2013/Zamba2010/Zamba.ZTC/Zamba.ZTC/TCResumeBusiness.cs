using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.CoreExt;

namespace Zamba.ZTC
{
    internal class TCResumeBusiness
    {
        #region ResumeMode enum

        public enum ResumeMode
        {
            ListTC = 1,
            PrepareExecution = 2,
            Full = 3
        }

        #endregion

        private string TCEXHeaderTemplate = "Templates\\TestCaseEXHeader.htm";
        private string TCEXRowTemplate = "Templates\\TestCaseEXRow.htm";
        private string TCResumeTemplate = "Templates\\CasosdePrueba.htm";
        private string TCStatisticRowErrorsTable = "Templates\\TestCaseStatisticsErrorTable.htm";
        private string TCStatisticRowErrorsTemplate = "Templates\\TestCaseStatisticRowErrorTemplate.htm";
        private string TCStatisticRowNotExecutedTable = "Templates\\TestCaseStatisticNEXTable.htm";
        private string TCStatisticRowNotExecutedTemplate = "Templates\\TestCaseStatisticRowNEXTemplate.htm";
        private string TCStatistic_Count = "Templates\\TestCaseStatistics_Counts.htm";
        private string TCStepRowTemplate = "Templates\\TestCaseSteprow.htm";
        private string TCStepRowTemplateExecution = "Templates\\TestCaseSteprowExecution.htm";
        private string TCStepTableTemplate = "Templates\\TestCaseStepTable.htm";
        private string TCStepTableTemplateExecution = "Templates\\TestCaseStepTableExecution.htm";
        private string TCTemplateHeader = "Templates\\TestCaseHeader.htm";
        private string TCTemplateHeaderExecution = "Templates\\TestCaseHeaderExecution.htm";
        private string TCZTCStyle = "Templates\\ZTCStylesheet.htm";
        private IUser User;

        private Int64 projectId { get; set; }

        public string ToHtml(string str)
        {
            string a = string.Empty;
            bool bandera = false;

            if (str.Contains("ñ") || str.Contains("Ñ"))
            {
                bandera = true;
                a = string.Join(string.Empty,
                                str.ToCharArray().Select(c => (int) c > 127 ? "&" + c + "tilde;" : c.ToString()).ToArray
                                    ());
            }
            if (bandera == false)
            {
                a = string.Join(string.Empty,
                                str.ToCharArray().Select(c => (int) c > 127 ? "&" + c + "acute;" : c.ToString()).ToArray
                                    ());
            }


            const string conSignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇ";
            const string sinSignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUcC";

            string textoSinAcentos = string.Empty;

            foreach (char caracter in a)
            {
                int indexConAcento = conSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (sinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }
            return textoSinAcentos;
        }

        public void PrintResume(IUser user, Int64 ProjectId, Int64 ObjectTypeId, String ResumeFile, ResumeMode Mode)
        {
            if (Mode == ResumeMode.Full)
            { 
                PrintFullResume( user,  ProjectId,  ObjectTypeId,  ResumeFile,  Mode); 
            }
            else
            {
                var TCOkList = new ArrayList();
                var TCErrorList = new ArrayList();
                var TCNoExecutedCount = new ArrayList();
                Int32 TestEXOKCount = 0;
                Int32 TestEXErrorCount = 0;
                var SB = new StringBuilder();
                var dbTools = new DBToolsExt();
                string schema = dbTools.DataBaseSchema;

                SB.Append(GetTCResumeTemplate());
                //agregar estilos
                SB.Replace("Zamba.Styles", GetTCZTCStyle());

                IProject Project = ProjectBussines.GetProjectByID(ProjectId);
                SB.Replace("Zamba.Proyecto", Project.Name);
                SB.Replace("Zamba.FechaActual", DateTime.Now.ToShortDateString());
                SB.Replace("Zamba.Version", "");
                //reemplazar imagenes
                SB.Replace("Zamba.LogoCliente", ZOptBusiness.GetValue("IconClientLogoOnTestCaseTemplate"));
                SB.Replace("Zamba.LogoZamba", ZOptBusiness.GetValue("IconZambaLogoOnTestCaseTemplate"));

                ZTrace.WriteLineIf(ZTrace.IsInfo, " se cargo el Proyecto: " + Project.Name);


                try
                {
                    User = user;
                    projectId = ProjectId;

                    TCEntities dbContext1;
                    TCEntities dbContext2;
                    TCEntities dbContext3;

                    if (dbTools.UseWindowsAuthentication)
                    {
                        dbContext1 = new TCEntities(ControlsFactory.EntityConnectionString);
                        dbContext2 = new TCEntities(ControlsFactory.EntityConnectionString);
                        dbContext3 = new TCEntities(ControlsFactory.EntityConnectionString);
                    }
                    else
                    {
                        dbContext1 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                        dbContext2 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                        dbContext3 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                    }

                    var query = (from p in dbContext1.ZTC_CT
                                 join u in dbContext1.USRTABLE on p.Author equals u.ID
                                 join o in dbContext1.OBJECTTYPES on p.ObjectTypeID equals o.OBJECTTYPESID
                                 join c in dbContext1.ZTC_CT on p.ParentNode equals c.TestCaseId
                                 join pro in dbContext1.PRJ_R_O on p.ObjectTypeID equals pro.OBJTYP
                                 where p.NodeType == 2
                                       && pro.PRJID == (decimal)projectId
                                       && pro.OBJTYP == (decimal)ObjectTypeId
                                 select new
                                            {
                                                Id = p.TestCaseId,
                                                Categoria = c.NodeName,
                                                Nombre = p.NodeName,
                                                Descripcion = p.NodeDescription,
                                                Creado = p.CreateDate,
                                                Modificado = p.UpdateDate,
                                                Autor = u.NOMBRES + " " + u.APELLIDO,
                                                Tipo = o.OBJECTTYPES1,
                                                ObjectTypeID = o.OBJECTTYPESID,
                                                ObjectId = p.ObjectID,
                                            }).Distinct();

                    var tstable = new StringBuilder();

                    foreach (var x in query)
                    {
                        string ObjectName = TCBusiness.GetObjectName((Int64)x.ObjectTypeID, (Int64)x.ObjectId);
                        ZTrace.WriteLineIf(ZTrace.IsInfo, " recorriendo: " + ObjectName);

                    

                        if (Mode == ResumeMode.PrepareExecution)
                        {
                            tstable.Append(GetTCHeaderTemplateExecution());
                        }
                        else
                        {
                            tstable.Append(GetTCHeaderTemplate());
                        }


                        tstable.Replace("Zamba.TCTitle", ToHtml(x.Nombre));
                        tstable.Replace("Zamba.TCCategory", ToHtml(x.Categoria));
                        tstable.Replace("Zamba.TCDescription", ToHtml(x.Descripcion));
                        tstable.Replace("Zamba.TCCreateDate", x.Creado.ToShortDateString());
                        tstable.Replace("Zamba.TCUpdateDate", x.Modificado.ToShortDateString());
                        tstable.Replace("Zamba.TCAuthor", ToHtml(x.Autor));
                        tstable.Replace("Zamba.TCObjectType", ToHtml(x.Tipo));
                        tstable.Replace("Zamba.TCObjectName", ToHtml(ObjectName));


                        var queryTS = from ts in dbContext2.ZTC_TS
                                      join st in dbContext2.ZTC_ST
                                          on ts.StepTypeID equals st.StepTypeID
                                      where ts.TestCaseID == x.Id
                                      select
                                          new
                                              {
                                                  ts.StepId,
                                                  ts.StepDescription,
                                                  StepComments = ts.StepObservation,
                                                  StepType = st.StepTypeName
                                              };


                        if (Mode == ResumeMode.PrepareExecution)
                        {
                            tstable.Replace("Zamba.TestCaseTable", GetSteptableTemplateExecution());
                        }
                        else
                        {
                            tstable.Replace("Zamba.TestCaseTable", GetSteptableTemplate());
                        }


                        foreach (var ts in queryTS)
                        {
                            var tsrow = new StringBuilder();
                            if (Mode == ResumeMode.PrepareExecution)
                            {
                                tsrow.Append(GetStepRowTemplateExecution());
                            }
                            else
                            {
                                tsrow.Append(GetStepRowTemplate());
                            }
                            tsrow.Replace("TsStepId", ts.StepId.ToString());
                            tsrow.Replace("TsDescription", ToHtml(ts.StepDescription));
                            tsrow.Replace("TsComments", ToHtml(ts.StepComments));
                            tsrow.Replace("TsType", ToHtml(ts.StepType));


                            //if (Mode == ResumeMode.PrepareExecution)
                            //{
                            //    tsrow.Replace("TsType", ts.StepType);
                            //    tsrow.Replace("TsType", ts.StepType);
                            //}

                            tstable.Replace("NewRow", tsrow.ToString());
                        }

                        tstable.Replace("EXCommentsText", "");
                        tstable.Replace("NewRow", "");


                    

                        String LastResult = string.Empty;

                        if (Mode == ResumeMode.ListTC)
                        {
                            var queryEX = from EX in dbContext3.ZTC_EX
                                          join RS in dbContext3.ZTC_RS
                                              on EX.ExecutionResultID equals RS.ExecutionResultID
                                          join U in dbContext3.USRTABLE
                                              on EX.UserId equals U.ID
                                          where EX.TestCaseID == x.Id
                                          orderby EX.ExecutionDate
                                          select new
                                                     {
                                                         Id = EX.ExecutionId,
                                                         EX.Comment,
                                                         Date = EX.ExecutionDate,
                                                         LastStep = EX.LastStepExecutionID,
                                                         Result = RS.ExecutionResultName,
                                                         User = U.NAME + " " + U.APELLIDO
                                                     };

                            var extable = new StringBuilder();
                            extable.Append(GetEXHeaderTemplate());


                            foreach (var EX in queryEX)
                            {
                                var EXrow = new StringBuilder();
                                EXrow.Append(GetEXRowTemplate());

                                EXrow.Replace("EXDate", EX.Date.ToString("dd/MM/yyyy HH:mm"));
                                EXrow.Replace("EXUser", EX.User);
                                EXrow.Replace("EXResult", EX.Result);
                                EXrow.Replace("EXLastStep", EX.LastStep.ToString());
                              

                                tstable.Replace("EXCommentsText", "");
                                tstable.Replace("Zamba.TCResult", "");

                                extable.Replace("NewEXRow", EXrow.ToString());

                                LastResult = EX.Result;

                                if (LastResult == "Correcto")
                                {
                                    TestEXOKCount++;
                                }
                                else
                                {
                                    TestEXErrorCount++;
                                }
                            }

                            extable.Replace("NewEXRow", "");


                            SB.Replace("Zamba.TestCasesEX", extable.ToString());
                        }
                        if (LastResult == "Correcto")
                        {
                            TCOkList.Add(x);
                        }
                        else
                        {
                            TCErrorList.Add(x);
                        }


                        if (LastResult == String.Empty)
                        {
                            TCNoExecutedCount.Add(x);
                        }


                        LastResult = "";
                    }
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " fin foreach ");

                    SB.Replace("Zamba.TestCasesDetails", tstable.ToString());
                    SB.Replace("Zamba.TestCasesEX", "");
                    SB.Replace("Zamba.TestCasesDetails", "");


                    //estadisticas
                    var StatisticsCounts_table = new StringBuilder();

                    StatisticsCounts_table.Append(GetTCStatistic_Count());

                    StatisticsCounts_table.Replace("Zamba.TCCasesOK", TCOkList.Count.ToString());
                    StatisticsCounts_table.Replace("Zamba.TCCasesError", TCErrorList.Count.ToString());
                    StatisticsCounts_table.Replace("Zamba.TCNonExecuted", TCNoExecutedCount.Count.ToString());
                    StatisticsCounts_table.Replace("Zamba.TCCasesTotal",
                                                   (TCOkList.Count + TCErrorList.Count + TCNoExecutedCount.Count).ToString());

                    StatisticsCounts_table.Replace("Zamba.EXCasesOK", TestEXOKCount.ToString());
                    StatisticsCounts_table.Replace("Zamba.EXCasesError", TestEXErrorCount.ToString());
                    StatisticsCounts_table.Replace("Zamba.EXCasesTotal", (TestEXOKCount + TestEXErrorCount).ToString());

                    SB.Replace("Zamba.TestStatistics", StatisticsCounts_table.ToString());


                    //obtener tabla
                    var tcErrorTable = new StringBuilder();
                    tcErrorTable.Append(GetTestCaseStatisticsErrorsTable());


                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio de Recorriendo de Tabla de errores," + tcErrorTable);
                    foreach (object error_row in TCErrorList)
                    {
                        //iterar rows
                        var tcErrorRow = new StringBuilder();
                        //obtener template para completar rows
                        tcErrorRow.Append(TestCaseStatisticRowErrorsTemplate());

                        //completar valores
                        string[] values = error_row.ToString().Split(Convert.ToChar(61));

                        string Nombre = values[3].Split(Convert.ToChar(44))[0];
                        string categoria = values[2].Split(Convert.ToChar(44))[0];
                        string descripcion = values[4].Split(Convert.ToChar(44))[0];
                        string tipo = values[8].Split(Convert.ToChar(44))[0];

                        tcErrorRow.Replace("TcName", ToHtml(Nombre));
                        tcErrorRow.Replace("TcType", ToHtml(tipo));
                        tcErrorRow.Replace("TcDescription", ToHtml(descripcion));
                        tcErrorRow.Replace("TcCategory", ToHtml(categoria));

                        //generar row
                        tcErrorTable.Replace("NewRow", tcErrorRow.ToString());
                    }
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin De Recorriendo Tabla de errores");
                    tcErrorTable.Replace("NewRow", "");

                    //obtener tabla
                    var tcNEXTable = new StringBuilder();
                    tcNEXTable.Append(GetTestCaseStatisticsNotExecutedTable());

                    ZTrace.WriteLineIf(ZTrace.IsInfo, " Inicio de recorrido: " + tcNEXTable);
                    foreach (object tcNEXrow in TCNoExecutedCount)
                    {
                        //iterar rows
                        var tcNEXRowTemplate = new StringBuilder();
                        //obtener template para completar rows
                        tcNEXRowTemplate.Append(TestCaseStatisticRowNotExecutedTemplate());

                        //completar valores
                        string[] values = tcNEXrow.ToString().Split(Convert.ToChar(61));

                        string Nombre = values[3].Split(Convert.ToChar(44))[0];
                        string categoria = values[2].Split(Convert.ToChar(44))[0];
                        string descripcion = values[4].Split(Convert.ToChar(44))[0];
                        string tipo = values[8].Split(Convert.ToChar(44))[0];

                        tcNEXRowTemplate.Replace("TcName", ToHtml(Nombre));
                        tcNEXRowTemplate.Replace("TcDescription", ToHtml(descripcion));
                        tcNEXRowTemplate.Replace("TcType", ToHtml(tipo));
                        tcNEXRowTemplate.Replace("TcCategory", ToHtml(categoria));
                        //generar row
                        tcNEXTable.Replace("NewRow", tcNEXRowTemplate.ToString());
                    }
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " fin foreach  tcNEXTable ");

                    tcNEXTable.Replace("NewRow", "");

                    SB.Replace("Zamba.TCErrors", tcErrorTable.ToString());
                    SB.Replace("Zamba.TCNoEX", tcNEXTable.ToString());

                    String Resume = SB.ToString();


                    var SW = new StreamWriter(ResumeFile);
                    SW.AutoFlush = true;
                    SW.Write(Resume);
                    SW.Close();
                    SW.Dispose();
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                }
                finally
                {
                    dbTools = null;
                }
            }
        }

        public void PrintFullResume(IUser user, Int64 ProjectId, Int64 ObjectTypeId, String ResumeFile, ResumeMode Mode1)
        {
            var TCOkList = new ArrayList();
            var TCErrorList = new ArrayList();
            var TCNoExecutedCount = new ArrayList();
            Int32 TestEXOKCount = 0;
            Int32 TestEXErrorCount = 0;
            var SB = new StringBuilder();
            var dbTools = new DBToolsExt();
            string schema = dbTools.DataBaseSchema;

            SB.Append(GetTCResumeTemplate());
            //agregar estilos
            SB.Replace("Zamba.Styles",GetTCZTCStyle());

            IProject Project = ProjectBussines.GetProjectByID(ProjectId);
            SB.Replace("Zamba.Proyecto", Project.Name);
            SB.Replace("Zamba.FechaActual", DateTime.Now.ToShortDateString());
            SB.Replace("Zamba.Version", "");
            SB.Replace("Zamba.TestCasesIndex", "<b>ENTORNO </b></br> Servidor: " + Servers.Server.DBServer + "</br>Base de datos: " + Servers.Server.DB);
            //reemplazar imagenes
            SB.Replace("Zamba.LogoCliente", ZOptBusiness.GetValue("IconClientLogoOnTestCaseTemplate"));
            SB.Replace("Zamba.LogoZamba", ZOptBusiness.GetValue("IconZambaLogoOnTestCaseTemplate"));

            ZTrace.WriteLineIf(ZTrace.IsInfo, " se cargo el Proyecto: " + Project.Name);


            try
            {
                User = user;
                projectId = ProjectId;

                TCEntities dbContext1;
                TCEntities dbContext2;
                TCEntities dbContext3;

                if (dbTools.UseWindowsAuthentication)
                {
                    dbContext1 = new TCEntities(ControlsFactory.EntityConnectionString);
                    dbContext2 = new TCEntities(ControlsFactory.EntityConnectionString);
                    dbContext3 = new TCEntities(ControlsFactory.EntityConnectionString);
                }
                else
                {
                    dbContext1 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                    dbContext2 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                    dbContext3 = new TCEntities(ControlsFactory.EntityConnectionString, schema);
                }

                var query = (from p in dbContext1.ZTC_CT
                             join u in dbContext1.USRTABLE on p.Author equals u.ID
                             join o in dbContext1.OBJECTTYPES on p.ObjectTypeID equals o.OBJECTTYPESID
                             join c in dbContext1.ZTC_CT on p.ParentNode equals c.TestCaseId
                             join pro in dbContext1.PRJ_R_O on p.ObjectTypeID equals pro.OBJTYP
                             where p.NodeType == 2
                                   && pro.PRJID == (decimal)projectId
                                   && pro.OBJTYP == (decimal)ObjectTypeId
                             select new
                             {
                                 Id = p.TestCaseId,
                                 Categoria = c.NodeName,
                                 Nombre = p.NodeName,
                                 Descripcion = p.NodeDescription,
                                 Creado = p.CreateDate,
                                 Modificado = p.UpdateDate,
                                 Autor = u.NOMBRES + " " + u.APELLIDO,
                                 Tipo = o.OBJECTTYPES1,
                                 ObjectTypeID = o.OBJECTTYPESID,
                                 ObjectId = p.ObjectID,
                             }).Distinct();


                foreach (var x in query)
                {
                    string ObjectName = TCBusiness.GetObjectName((Int64)x.ObjectTypeID, (Int64)x.ObjectId);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, " recorriendo: " + ObjectName);

                    var tstable = new StringBuilder();

                    //if (Mode == ResumeMode.PrepareExecution)
                    //{
                    //    tstable.Append(GetTCHeaderTemplateExecution());
                    //}
                    //else
                    //{
                        tstable.Append(GetTCHeaderTemplate());
                    //}


                    tstable.Replace("Zamba.TCTitle", ToHtml(x.Nombre));
                    tstable.Replace("Zamba.TCCategory", ToHtml(x.Categoria));
                    tstable.Replace("Zamba.TCDescription", ToHtml(x.Descripcion));
                    tstable.Replace("Zamba.TCCreateDate", x.Creado.ToShortDateString());
                    tstable.Replace("Zamba.TCUpdateDate", x.Modificado.ToShortDateString());
                    tstable.Replace("Zamba.TCAuthor", ToHtml(x.Autor));
                    tstable.Replace("Zamba.TCObjectType", ToHtml(x.Tipo));
                    tstable.Replace("Zamba.TCObjectName", ToHtml(ObjectName));
                    tstable.Replace("Zamba.TCId", ToHtml(x.Id.ToString()));


                    var queryTS = from ts in dbContext2.ZTC_TS
                                  join st in dbContext2.ZTC_ST
                                      on ts.StepTypeID equals st.StepTypeID
                                  where ts.TestCaseID == x.Id
                                  select
                                      new
                                      {
                                          ts.StepId,
                                          ts.StepDescription,
                                          StepComments = ts.StepObservation,
                                          StepType = st.StepTypeName
                                      };
                                    

                    String LastResult = string.Empty;
                     var queryEX = from EX in dbContext3.ZTC_EX
                                      join RS in dbContext3.ZTC_RS
                                          on EX.ExecutionResultID equals RS.ExecutionResultID
                                      join U in dbContext3.USRTABLE
                                          on EX.UserId equals U.ID
                                      where EX.TestCaseID == x.Id
                                      orderby EX.ExecutionDate
                                      select new
                                      {
                                          Id = EX.ExecutionId,
                                          EX.Comment,
                                          Date = EX.ExecutionDate,
                                          LastStep = EX.LastStepExecutionID,
                                          Result = RS.ExecutionResultName,
                                          User = U.NAME + " " + U.APELLIDO
                                      };

                        var extable = new StringBuilder();
                        extable.Append(GetEXHeaderTemplate());
                    
                        foreach (var EX in queryEX)
                        {

                            //if (Mode == ResumeMode.PrepareExecution)
                            //{
                                tstable.Replace("Zamba.TestCaseTable", GetSteptableTemplateExecution());
                            //}
                            //else
                            //{
                            //    tstable.Replace("Zamba.TestCaseTable", GetSteptableTemplate());
                            //}


                            foreach (var ts in queryTS)
                            {
                                var tsrow = new StringBuilder();
                                //if (Mode == ResumeMode.PrepareExecution)
                                //{
                                    tsrow.Append(GetStepRowTemplateExecution());
                                //}
                                //else
                                //{
                                //    tsrow.Append(GetStepRowTemplate());
                                //}
                                tsrow.Replace("TsStepId", ts.StepId.ToString());
                                tsrow.Replace("TsDescription", ToHtml(ts.StepDescription));
                                tsrow.Replace("TsComments", ToHtml(ts.StepComments));
                                tsrow.Replace("TsType", ToHtml(ts.StepType));

                                if (EX.Result == "Correcto")
                                {
                                    tsrow.Replace("StepRowChkOK'", ToHtml("StepRowChkOK' checked='checked'   disabled='disabled'"));
                                    tsrow.Replace("StepRowChkError'", ToHtml("StepRowChkError'  disabled='disabled' "));
                                }
                                else 
                                {
                                    if (EX.LastStep == ts.StepId)
                           {
                               tsrow.Replace("StepRowChkError'", ToHtml("StepRowChkError' checked='checked'   disabled='disabled' "));
                               tsrow.Replace("StepRowChkOK'", ToHtml("StepRowChkOK' disabled='disabled'"));
                           }
                                    else if (EX.LastStep > ts.StepId)
                           {
                               tsrow.Replace("StepRowChkOK'", ToHtml("StepRowChkOK' checked='checked'  disabled='disabled'"));
                               tsrow.Replace("StepRowChkError'", ToHtml("StepRowChkError'  disabled='disabled' "));
                           }
                           else
                           {
                               tsrow.Replace("StepRowChkError'", ToHtml("StepRowChkError'  disabled='disabled' "));
                               tsrow.Replace("StepRowChkOK'", ToHtml("StepRowChkOK' disabled='disabled'"));
                           }
                                                                    }

                                tstable.Replace("NewRow", tsrow.ToString());
                            }

                            tstable.Replace("NewRow", "");

                          
                            var EXrow = new StringBuilder();
                            EXrow.Append(GetEXRowTemplate());

                            EXrow.Replace("EXDate", EX.Date.ToString("dd/MM/yyyy HH:mm"));
                            EXrow.Replace("EXUser", EX.User);
                            EXrow.Replace("EXResult", EX.Result);
                            EXrow.Replace("EXLastStep", EX.LastStep.ToString());
                            tstable.Replace("EXCommentsText'", EX.Comment + "' disabled='disabled' ");
                            tstable.Replace("Zamba.TCResult", EX.Result);
                            
                            extable.Replace("NewEXRow", EXrow.ToString());

                            LastResult = EX.Result;

                            if (LastResult == "Correcto")
                            {
                                TestEXOKCount++;
                            }
                            else
                            {
                                TestEXErrorCount++;
                            }
                        }

                        extable.Replace("NewEXRow", "");

                        SB.Replace("Zamba.TestCasesDetails", tstable.ToString());

                        SB.Replace("Zamba.TestCasesEX", extable.ToString());
                    

                    if (LastResult == "Correcto")
                    {
                        TCOkList.Add(x);
                    }
                    else
                    {
                        TCErrorList.Add(x);
                    }


                    if (LastResult == String.Empty)
                    {
                        TCNoExecutedCount.Add(x);
                    }


                    LastResult = "";
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, " fin foreach ");

                SB.Replace("Zamba.TestCasesEX", "");
                SB.Replace("Zamba.TestCasesDetails", "");


                //estadisticas
                var StatisticsCounts_table = new StringBuilder();

                StatisticsCounts_table.Append(GetTCStatistic_Count());

                StatisticsCounts_table.Replace("Zamba.TCCasesOK", TCOkList.Count.ToString());
                StatisticsCounts_table.Replace("Zamba.TCCasesError", TCErrorList.Count.ToString());
                StatisticsCounts_table.Replace("Zamba.TCNonExecuted", TCNoExecutedCount.Count.ToString());
                StatisticsCounts_table.Replace("Zamba.TCCasesTotal",
                                               (TCOkList.Count + TCErrorList.Count + TCNoExecutedCount.Count).ToString());

                StatisticsCounts_table.Replace("Zamba.EXCasesOK", TestEXOKCount.ToString());
                StatisticsCounts_table.Replace("Zamba.EXCasesError", TestEXErrorCount.ToString());
                StatisticsCounts_table.Replace("Zamba.EXCasesTotal", (TestEXOKCount + TestEXErrorCount).ToString());

                SB.Replace("Zamba.TestStatistics", StatisticsCounts_table.ToString());


                //obtener tabla
                var tcErrorTable = new StringBuilder();
                tcErrorTable.Append(GetTestCaseStatisticsErrorsTable());


                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio de Recorriendo de Tabla de errores," + tcErrorTable);
                foreach (object error_row in TCErrorList)
                {
                    //iterar rows
                    var tcErrorRow = new StringBuilder();
                    //obtener template para completar rows
                    tcErrorRow.Append(TestCaseStatisticRowErrorsTemplate());

                    //completar valores
                    string[] values = error_row.ToString().Split(Convert.ToChar(61));

                    string Nombre = values[3].Split(Convert.ToChar(44))[0];
                    string categoria = values[2].Split(Convert.ToChar(44))[0];
                    string descripcion = values[4].Split(Convert.ToChar(44))[0];
                    string tipo = values[8].Split(Convert.ToChar(44))[0];

                    tcErrorRow.Replace("TcName", ToHtml(Nombre));
                    tcErrorRow.Replace("TcType", ToHtml(tipo));
                    tcErrorRow.Replace("TcDescription", ToHtml(descripcion));
                    tcErrorRow.Replace("TcCategory", ToHtml(categoria));

                    //generar row
                    tcErrorTable.Replace("NewRow", tcErrorRow.ToString());
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Fin De Recorriendo Tabla de errores");
                tcErrorTable.Replace("NewRow", "");

                //obtener tabla
                var tcNEXTable = new StringBuilder();
                tcNEXTable.Append(GetTestCaseStatisticsNotExecutedTable());

                ZTrace.WriteLineIf(ZTrace.IsInfo, " Inicio de recorrido: " + tcNEXTable);
                foreach (object tcNEXrow in TCNoExecutedCount)
                {
                    //iterar rows
                    var tcNEXRowTemplate = new StringBuilder();
                    //obtener template para completar rows
                    tcNEXRowTemplate.Append(TestCaseStatisticRowNotExecutedTemplate());

                    //completar valores
                    string[] values = tcNEXrow.ToString().Split(Convert.ToChar(61));

                    string Nombre = values[3].Split(Convert.ToChar(44))[0];
                    string categoria = values[2].Split(Convert.ToChar(44))[0];
                    string descripcion = values[4].Split(Convert.ToChar(44))[0];
                    string tipo = values[8].Split(Convert.ToChar(44))[0];

                    tcNEXRowTemplate.Replace("TcName", ToHtml(Nombre));
                    tcNEXRowTemplate.Replace("TcDescription", ToHtml(descripcion));
                    tcNEXRowTemplate.Replace("TcType", ToHtml(tipo));
                    tcNEXRowTemplate.Replace("TcCategory", ToHtml(categoria));
                    //generar row
                    tcNEXTable.Replace("NewRow", tcNEXRowTemplate.ToString());
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, " fin foreach  tcNEXTable ");

                tcNEXTable.Replace("NewRow", "");

                SB.Replace("Zamba.TCErrors", tcErrorTable.ToString());
                SB.Replace("Zamba.TCNoEX", tcNEXTable.ToString());

                String Resume = SB.ToString();


                var SW = new StreamWriter(ResumeFile);
                SW.AutoFlush = true;
                SW.Write(Resume);
                SW.Close();
                SW.Dispose();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
            }
            finally
            {
                dbTools = null;
            }
        }

        private String GetTCResumeTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCResumeTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetTCZTCStyle()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCZTCStyle));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetTCStatistic_Count()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStatistic_Count));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetStepRowTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStepRowTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String TestCaseStatisticRowErrorsTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStatisticRowErrorsTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String TestCaseStatisticRowNotExecutedTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStatisticRowNotExecutedTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetSteptableTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStepTableTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetTestCaseStatisticsErrorsTable()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStatisticRowErrorsTable));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetTestCaseStatisticsNotExecutedTable()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStatisticRowNotExecutedTable));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }


        private String GetTCHeaderTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCTemplateHeader));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetStepRowTemplateExecution()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStepRowTemplateExecution));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetSteptableTemplateExecution()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCStepTableTemplateExecution));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetTCHeaderTemplateExecution()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCTemplateHeaderExecution));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetEXRowTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCEXRowTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }

        private String GetEXHeaderTemplate()
        {
            var fi = new FileInfo(Path.Combine(Application.StartupPath, TCEXHeaderTemplate));
            if (fi.Exists)
            {
                var SR = new StreamReader(fi.FullName);
                String Template;
                using (SR)
                {
                    Template = SR.ReadToEnd();
                    SR.Close();
                    SR.Dispose();
                }
                return Template;
            }
            return string.Empty;
        }
    }
}