using System;
using System.Xml;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Zamba.Core.DocTypes.DocAsociated;
using System.ComponentModel;
using Zamba.Core.WF.WF;
using Zamba.Filters;
using Microsoft.VisualBasic;

namespace Zamba.PreLoad
{
    public class PreLoadEngine
    {
        public delegate void ChangeText(string text);
        public static event ChangeText ChangeTextEvent;

        public delegate void CloseDialog();
        public static event CloseDialog CloseDialogEvent;


        public PreLoadEngine()
        {
        }

        private BackgroundWorker MessageWorker;
        private BackgroundWorker WorkFlowWorker;
        private BackgroundWorker VariablesWorker;

        public void PreLoadObjects()
        {
            try
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Obteniendo variables globales");

                VariablesWorker = new BackgroundWorker();
                VariablesWorker.DoWork += VariablesWorker_DoWork;
                VariablesWorker.RunWorkerCompleted += VariablesWorker_RunWorkerCompleted;
                VariablesWorker.ProgressChanged += VariablesWorker_ProgressChanged;
                VariablesWorker.RunWorkerAsync();

                if (null != ChangeTextEvent)
                    ChangeTextEvent("Obteniendo variables de usuario");

                WorkFlowWorker = new BackgroundWorker();
                WorkFlowWorker.DoWork += WorkFlowWorker_DoWork;
                WorkFlowWorker.RunWorkerCompleted += WorkFlowWorker_RunWorkerCompleted;
                WorkFlowWorker.ProgressChanged += WorkFlowWorker_ProgressChanged;
                WorkFlowWorker.RunWorkerAsync();

                EntitiesWorker = new BackgroundWorker();
                EntitiesWorker.DoWork += EntitiesWorker_DoWork;
                EntitiesWorker.RunWorkerCompleted += EntitiesWorker_RunWorkerCompleted;
                EntitiesWorker.ProgressChanged += EntitiesWorker_ProgressChanged;
                EntitiesWorker.RunWorkerAsync();

                UsersWorker = new BackgroundWorker();
                UsersWorker.DoWork += UsersWorker_DoWork;
                UsersWorker.RunWorkerCompleted += UsersWorker_RunWorkerCompleted;
                UsersWorker.ProgressChanged += UsersWorker_ProgressChanged;
                UsersWorker.RunWorkerAsync();



            }
            catch (SynchronizationLockException ex)
            { }
            catch (ThreadAbortException ex)
            { }
            catch (System.Threading.ThreadInterruptedException ex)
            { }
            catch (System.Threading.ThreadStateException ex)
            { }
            catch (InvalidOperationException ex)
            {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                if (null != CloseDialogEvent)
                    CloseDialogEvent();
            }

        }

        private void UsersWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void UsersWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != CloseDialogEvent)
                CloseDialogEvent();
        }

        private void UsersWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Obteniendo Datos de usuario");

                //UserGroupBusiness.GetAllUsersorGroupsNamesForPreLoad();
                UserBusiness.GetUserGroupsIdsByUserid(Membership.MembershipHelper.CurrentUser.ID);
                //UserGroupBusiness.getInheritanceOfGroup(Membership.MembershipHelper.CurrentUser.ID);


            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void EntitiesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void EntitiesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != CloseDialogEvent)
                CloseDialogEvent();

        }

        private void EntitiesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Membership.MembershipHelper.CurrentUser != null)
                {
                    if (null != ChangeTextEvent)
                        ChangeTextEvent("Obteniendo entidades");
                    ArrayList Entities = DocTypesBusiness.GetDocTypesbyUserRightsOfView(Membership.MembershipHelper.CurrentUser.ID, RightsType.View);

                    if (Entities != null)
                    {
                        List<Int64> IndexsLoaded = new List<Int64>();

                        foreach (IDocType E in Entities)
                        {
                            IDocType entity = DocTypesBusiness.GetDocType(E.ID, true);
                            if (null != ChangeTextEvent)
                                ChangeTextEvent("Obteniendo Entidad: " + E.Name);

                            IndexsBusiness.GetIndexSchemaAsDataSet(E.ID);
                            UserBusiness.Rights.GetIndexsRights(E.ID, Membership.MembershipHelper.CurrentUser.ID, true, true);
                            DocTypesBusiness.GetIndexsProperties(E.ID,true);

                            foreach (IIndex I in entity.Indexs)
                            {
                                if (IndexsLoaded.Contains(I.ID) == false)
                                {
                                    LoadIndex(I.ID);
                                    IndexsLoaded.Add(I.ID);
                                    if (null != ChangeTextEvent)
                                        ChangeTextEvent("Atributo: " + I.Name);
                                }
                               // Zamba.Core.IndexsBusiness.getReferenceStatus(E.ID, I.ID);
                            }

                            //WFTaskBusiness.GetDocTypeUserRestrictions(E.ID, Membership.MembershipHelper.CurrentUser.ID, true);

                            String restrictionKey = Membership.MembershipHelper.CurrentUser.ID + "-" + E.ID;

                            //if (Core.Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(restrictionKey) == false)
                            //{
                            //    Core.Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(restrictionKey, RestrictionsMapper_Factory.getRestrictionIndexs(Membership.MembershipHelper.CurrentUser.ID, E.ID));
                            //}

                            var DocTypesAsociated = DocAsociatedBusiness.getDocTypesAsociated(E.ID);

                            foreach (Asociados Asociado2 in DocTypesAsociated)
                            {

                                var AsociatedDocType = DocTypesBusiness.GetDocType(Asociado2.DocTypeId2, true);
                                if (null != ChangeTextEvent)
                                    ChangeTextEvent("Asociado " + AsociatedDocType.Name);
                              //  DocAsociatedBusiness.GetAsociations(E, AsociatedDocType);
                            }

                            FiltersComponent FC = new FiltersComponent();
                            FC.GetLastUsedFilters(E.ID, Membership.MembershipHelper.CurrentUser.ID, true);
                            FC.GetLastUsedFilters(E.ID, Membership.MembershipHelper.CurrentUser.ID, false);
                            FC = null;

                            FormBusiness.GetAllForms(E.ID);

                            //FormBusiness.GetForms(E.ID, FormTypes.All);
                            FormBusiness.GetShowAndEditForms(E.ID);

                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void WorkFlowWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void WorkFlowWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != CloseDialogEvent)
                CloseDialogEvent();

        }

        private void VariablesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void VariablesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (null != CloseDialogEvent)
                CloseDialogEvent();
        }

        private void WorkFlowWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Membership.MembershipHelper.CurrentUser != null)
                {
                    Zamba.Core.WFBusiness.GetUserWFIdsAndNamesWithSteps(Membership.MembershipHelper.CurrentUser.ID);
                   // WFTaskBusiness.UpdateAllUserAsignedTasksState(Membership.MembershipHelper.CurrentUser.ID);
                    Zamba.Core.WFBusiness.GetStepsByUserRestrictedDoctypes(Membership.MembershipHelper.CurrentUser.ID);
                    List<EntityView> workflows = WFBusiness.GetUserWFIdsAndNamesWithSteps(Membership.MembershipHelper.CurrentUser.ID);
                    if (workflows != null)
                    {
                        LoadWorkFlow(workflows);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void VariablesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // UserPreferences.LoadAllMachineConfigValues();
            //  UserPreferences.LoadAllUserConfigValues();
          //  ZOptBusiness.GetAllValues();

            ToolsBusiness.loadGlobalVariables();
        }




        private void LoadWorkFlow(List<EntityView> Workflows)
        {
            List<Int64> WorkflowsIds = new List<long>();
            foreach (EntityView workflow in Workflows)
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Obteniendo Entidad: " + workflow.Name);
                WorkflowsIds.Add(workflow.ID);

//                GenericButtonBusiness.GetRuleButtons(ButtonPlace.BarraTareas, true, workflow.ID);
  //              GenericButtonBusiness.GetRuleButtons(ButtonPlace.DocumentToolbar_tasks, true, workflow.ID);
    //            GenericButtonBusiness.GetRuleButtons(ButtonPlace.ArbolTareas, true, workflow.ID);
            }

            DsSteps ds = WFStepBusiness.GetDsSteps(WorkflowsIds);
            List<Int64> StepsIDList = new List<Int64>();

            //Cargo las opciones de las reglas de los workflows
            //Cache.Workflows.HSRulesOptions.Add(WFRulesBusiness.GetRuleOptionsByWFList(WFlistid));
            ChangeTextEvent("Cargando reglas de negocio");
            //  WFRulesBusiness.GetRulesByWFId(WFlistid);
            foreach (DsSteps.WFStepsRow step in ds.WFSteps.Rows)
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Etapa " + step.Name);

                StepsIDList.Add(Int64.Parse(step.Step_Id.ToString()));

                WFStepStatesComponent.LoadStepStatesByStepIdList(StepsIDList);

                IWFStep WFStep = WFStepBusiness.GetStepById((Int64)step.Step_Id);
                WFStepBusiness.GetStepColors((Int64)step.Step_Id);
                WFStepBusiness.GetDocTypesByWfStepAsDT((Int64)step.Step_Id);
              //  WFBusiness.CanExecuteRulesInStep((Int64)step.Step_Id, Membership.MembershipHelper.CurrentUser.ID);
            }
        }


        private void LoadIndex(object pIndexId)
        {
            Int32 IndexId = Int32.Parse(pIndexId.ToString());
            IIndex I = ZCore.GetInstance().GetIndex(IndexId);
            if (I != null)
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Atributo: " + I.Name);

                if (I.DropDown == IndexAdditionalType.DropDown || I.DropDown == IndexAdditionalType.DropDownJerarquico)
                {
                    List<string> List = null;
                    List = IndexsBusiness.GetDropDownList(I.ID);
                    //todo: cargar controles               
                    //todo: Cargar Select
                }
                else if (I.DropDown == IndexAdditionalType.AutoSustitución || I.DropDown == IndexAdditionalType.AutoSustituciónJerarquico)
                {
                    DataTable Table = AutoSubstitutionBusiness.GetIndexData(I.ID, false);
                }
                if (!Zamba.Core.Cache.DocTypesAndIndexs.hsIndexsName.ContainsKey(I.Name))
                {
                    Zamba.Core.Cache.DocTypesAndIndexs.hsIndexsName.Add(I.Name, I.ID);
                }

                Zamba.Core.IndexsBusiness.CheckIfAllowDataOutOfList(I.ID);
                Zamba.Core.AutoCompleteBarcode_FactoryBusiness.getIndexKeys(I.ID);
            }
        }



        #region "Formularios"

        private BackgroundWorker EntitiesWorker;
        private BackgroundWorker UsersWorker;


        private static void CopyFilesAndSubDirectoryFiles(DirectoryInfo fromdirectory, DirectoryInfo toirectory)
        {
            CopyFilesFromDirectory(toirectory, fromdirectory);

            foreach (var di in fromdirectory.GetDirectories())
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Copio archivos desde:" + toirectory.FullName + "\\" + di.Name);
                DirectoryInfo newtodir = new DirectoryInfo(toirectory.FullName + "\\" + di.Name);
                CopyFilesAndSubDirectoryFiles(di, newtodir);

            }
        }

        private static void CopyFilesFromDirectory(DirectoryInfo toirectory, DirectoryInfo di)
        {
            try
            {
                foreach (var fi in di.GetFiles())
                {
                    fi.CopyTo(toirectory.FullName + "\\" + fi.Name, true);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Copio archivos desde directorio:" + toirectory.FullName + "\\" + fi.Name);
                    if (null != ChangeTextEvent)
                        ChangeTextEvent("Actualizando Formularios");

                }
            }
            catch (InvalidOperationException ex)
            {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        

        private void RecursiveDirectoryCopy(string sourceDir, string destDir, bool fRecursive, bool overWrite)
        {
            string sDir = null;

            string sFile = null;
            FileInfo sFileInfo = null;
            FileInfo dFileInfo = null;

            // Add trailing separators to the supplied paths if they don't exist.
            if (!sourceDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                sourceDir += System.IO.Path.DirectorySeparatorChar;

            if (!destDir.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                destDir += System.IO.Path.DirectorySeparatorChar;

            try
            {
                //If destination directory does not exist, create it.
                DirectoryInfo dDirInfo = new DirectoryInfo(destDir);

                if (dDirInfo.Exists == false)
                    dDirInfo.Create();

                dDirInfo = null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

            // Recursive switch to continue drilling down into directory structure.
            if (fRecursive)
            {
                // Get a list of directories from the current parent.
                foreach (string sDir_loopVariable in Directory.GetDirectories(sourceDir))
                {
                    try
                    {
                        sDir = sDir_loopVariable;
                        DirectoryInfo sDirInfo = new DirectoryInfo(sDir);
                        DirectoryInfo dDirInfo = new DirectoryInfo(destDir + sDirInfo.Name);

                        // Create the directory if it does not exist.
                        if (dDirInfo.Exists == false)
                            dDirInfo.Create();

                        // Since we are in recursive mode, copy the children also
                        RecursiveDirectoryCopy(sDirInfo.FullName, dDirInfo.FullName, fRecursive, overWrite);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }
            }

            // Get the files from the current parent.
            foreach (string sFile_loopVariable in System.IO.Directory.GetFiles(sourceDir))
            {
                try
                {
                    sFile = sFile_loopVariable;
                    sFileInfo = new FileInfo(sFile);
                    dFileInfo = new FileInfo(sFile.Replace(sourceDir, destDir));

                    //If File does not exist. Copy.
                    if (dFileInfo.Exists == false)
                    {
                        sFileInfo.CopyTo(dFileInfo.FullName, overWrite);
                    }
                    else
                    {
                        if (dFileInfo.IsReadOnly)
                            dFileInfo.IsReadOnly = false;

                        //If file exists and is the same length (size). Skip.
                        //If file exists and is of different Length (size) and overwrite = True. Copy
                        //if (sFileInfo.Length != dFileInfo.Length && overWrite)
                        if (overWrite)
                        {
                            sFileInfo.CopyTo(dFileInfo.FullName, overWrite);
                            //If file exists and is of different Length (size) and overwrite = False. Skip
                        }
                        //else if (sFileInfo.Length != dFileInfo.Length && !overWrite)
                        else
                        {
                            Debug.WriteLine(sFileInfo.FullName + " Not copied.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
                finally
                {
                    sFileInfo = null;
                    dFileInfo = null;
                }
            }
        }

        #endregion


    }
}
