using System;
using System.Xml;
using System.Data;
using Zamba.Core;
using System.Collections;
using System.Threading;
using Zamba.Indexs;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Zamba.Core.DocTypes.DocAsociated;
using System.ComponentModel;
using Zamba.Core.WF.WF;
using Zamba.Filters;
using Microsoft.VisualBasic;
using System.Net;

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

                UpdateForms();

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

                UserGroupBusiness.GetAllUsersorGroupsNamesForPreLoad();
                UserBusiness.GetUserGroupsIdsByUserid(Membership.MembershipHelper.CurrentUser.ID);
                UserGroupBusiness.getInheritanceOfGroup(Membership.MembershipHelper.CurrentUser.ID);


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
                    List<IDocType> Entities = DocTypesBusiness.GetDocTypesbyUserRightsOfView(Membership.MembershipHelper.CurrentUser.ID, RightsType.View);

                    if (Entities != null)
                    {
                        List<Int64> IndexsLoaded = new List<Int64>();

                        foreach (IDocType E in Entities)
                        {
                            IDocType entity = DocTypesBusiness.GetDocType(E.ID, true);
                            if (null != ChangeTextEvent)
                                ChangeTextEvent("Obteniendo Entidad: " + E.Name);

                            IndexsBusiness.GetIndexsSchemaAsListOfDT(E.ID, true);
                            UserBusiness.Rights.GetIndexsRights(E.ID, Membership.MembershipHelper.CurrentUser.ID, true, true);
                            DocTypesBusiness.GetIndexsProperties(E.ID);

                            foreach (IIndex I in entity.Indexs)
                            {
                                if (IndexsLoaded.Contains(I.ID) == false)
                                {
                                    LoadIndex(I.ID);
                                    IndexsLoaded.Add(I.ID);
                                    if (null != ChangeTextEvent)
                                        ChangeTextEvent("Atributo: " + I.Name);
                                }
                                Zamba.Core.IndexsBusiness.getReferenceStatus(E.ID, I.ID);
                            }

                            WFTaskBusiness.GetDocTypeUserRestrictions(E.ID, Membership.MembershipHelper.CurrentUser.ID, true);

                            String restrictionKey = Membership.MembershipHelper.CurrentUser.ID + "-" + E.ID;

                            if (Core.Cache.DocTypesAndIndexs.hsRestrictionsIndexs.ContainsKey(restrictionKey) == false)
                            {
                                Core.Cache.DocTypesAndIndexs.hsRestrictionsIndexs.Add(restrictionKey, RestrictionsMapper_Factory.getRestrictionIndexs(Membership.MembershipHelper.CurrentUser.ID, E.ID, true));
                            }

                            var DocTypesAsociated = DocAsociatedBusiness.getDocTypesIdsAsociatedToDocType(E);

                            foreach (Int64 DT2 in DocTypesAsociated)
                            {

                                var AsociatedDocType = DocTypesBusiness.GetDocType(DT2);
                                if (null != ChangeTextEvent)
                                    ChangeTextEvent("Asociado " + AsociatedDocType.Name);
                                DocAsociatedBusiness.GetAsociations(E, AsociatedDocType);
                            }

                            FiltersComponent FC = new FiltersComponent();
                            FC.GetLastUsedFilters(E.ID, Membership.MembershipHelper.CurrentUser.ID, FilterTypes.Task);
                            FC.GetLastUsedFilters(E.ID, Membership.MembershipHelper.CurrentUser.ID, FilterTypes.Document);
                            FC = null;

                            FormBusiness.GetAllForms(E.ID, true);

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
                    WFTaskBusiness.UpdateAllUserAsignedTasksState(Membership.MembershipHelper.CurrentUser.ID);
                    Zamba.Core.WFBusiness.GetStepsByUserRestrictedDoctypes(Membership.MembershipHelper.CurrentUser.ID);
                    List<EntityView> workflows = WFBusiness.GetUserWFIdsAndNamesWithSteps(Membership.MembershipHelper.CurrentUser.ID);
                    if (workflows != null && workflows.Count > 0)
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
            // ZOptBusiness.GetAllValues();

            ToolsBusiness.loadGlobalVariables();
        }




        private void LoadWorkFlow(List<EntityView> Workflows)
        {
            foreach (EntityView workflow in Workflows)
            {
                if (null != ChangeTextEvent)
                    ChangeTextEvent("Obteniendo Entidad: " + workflow.Name);

                GenericButtonBusiness.GetRuleButtons(ButtonPlace.BarraTareas, true, workflow.ID);
                GenericButtonBusiness.GetRuleButtons(ButtonPlace.DocumentToolbar_tasks, true, workflow.ID);
                GenericButtonBusiness.GetRuleButtons(ButtonPlace.ArbolTareas, true, workflow.ID);
            }

            DsSteps ds = WFStepBusiness.GetDsSteps(Workflows);
            List<Int64> StepsIDList = new List<Int64>();

            if (ds != null)
            {
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

                    IWFStep WFStep = WFStepBusiness.GetStepById((Int64)step.Step_Id, false);
                    WFStepBusiness.GetStepColors((Int64)step.Step_Id);
                    WFStepBusiness.GetDocTypesByWfStepAsDT((Int64)step.Step_Id, true);
                    WFBusiness.CanExecuteRulesInStep((Int64)step.Step_Id, Membership.MembershipHelper.CurrentUser.ID);
                }
            }
        }


        private void LoadIndex(object pIndexId)
        {
            Int32 IndexId = Int32.Parse(pIndexId.ToString());
            IIndex I = ZCore.GetIndex(IndexId);
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

        private void LoadDisplayindex(IIndex index)
        {
            DisplayindexCtl ctrl;
            bool isReindex;
            isReindex = true;
            ctrl = new DisplayindexCtl(index, isReindex);
        }

        IndexController IC = new IndexController();

        private void LoadSearchIndex(IIndex index)
        {
            try
            {
                IC.GetControl(index, 0);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        #region "Formularios"

        Thread td1, td = null;
        private BackgroundWorker EntitiesWorker;
        private BackgroundWorker UsersWorker;

        private void UpdateForms()
        {
            td1 = new Thread(CopyDefaultFormsComponentsToLocalTemp);
            td1.Start();

            Thread td = new Thread(CopyFormsToLocalTemp);
            td.Start();
        }

        private void CopyDefaultFormsComponentsToLocalTemp()
        {
            try
            {
                Int64 currentDefaultFormVersion = Int64.Parse(UserPreferences.getValue("DefaultFormsVersion", UPSections.FormPreferences, 0));
                Int64 DefaultFormVersion = Int64.Parse(ZOptBusiness.GetValueOrDefault("DefaultFormsVersion", "0"));

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Version Actual formularios por defecto en carpeta usuario:" + currentDefaultFormVersion);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Version Actual formularios por defecto en servidor:" + DefaultFormVersion);

                ZTrace.WriteLineIf(ZTrace.IsInfo, "Version de formulario servidor:" + DefaultFormVersion);
                if (currentDefaultFormVersion < DefaultFormVersion)
                {


                    if (null != ChangeTextEvent)
                        ChangeTextEvent("Actualizando Componentes de Formularios");

                    String toirectoryname = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zamba Software\\Temp";
                    String fromdirectoryname = Environment.CurrentDirectory + "\\Formularios";

                    if (Directory.Exists(fromdirectoryname))
                    {
                        var fromdirectory = new System.IO.DirectoryInfo(fromdirectoryname);
                        var toirectory = new System.IO.DirectoryInfo(toirectoryname);
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "actualizo componentes de form:" + fromdirectory);
						RemoveReadOnlyFlag(toirectory);

						CopyFilesAndSubDirectoryFiles(fromdirectory, toirectory);
                    }
                    else
                        ChangeTextEvent("No existe la ruta: " + Environment.CurrentDirectory + "\\Formularios");
                }

                UserPreferences.setValue("DefaultFormsVersion", DefaultFormVersion.ToString(), UPSections.FormPreferences);
            }
            catch (System.Runtime.InteropServices.SEHException ex)
            {
                ZClass.raiseerror(ex);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

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


        private void CopyFormsToLocalTemp()
        {
            try
            {
                List<ZwebForm> frms = null;
                ArrayList Paths = null;


                if (null != ChangeTextEvent)

                    ChangeTextEvent("Actualizando formularios. Esto puede demorar unos minutos.");

                try
                {
                    Int64 currentFormVersion = Int64.Parse(UserPreferences.getValue("FormsVersion", UPSections.FormPreferences, 0));
                    Int64 FormVersion = Int64.Parse(ZOptBusiness.GetValueOrDefault("FormsVersion", "0"));

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Version Actual formularios  en carpeta usuario:" + currentFormVersion);

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Version Actual formularios en servidor:" + FormVersion);
                    if (currentFormVersion < FormVersion)
                    {


                        frms = FormBusiness.GetForms(true);
                        Paths = new ArrayList();

                        bool isFirst = true;
                        string pathLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zamba Software\\Temp";
                        foreach (IZwebForm frm in frms)
                        {
                            if (frm.Type == FormTypes.Edit || frm.Type == FormTypes.Insert || frm.Type == FormTypes.Search || frm.Type == FormTypes.Show || frm.Type == FormTypes.WorkFlow)
                            {
                                if (!frm.UseBlob && !string.IsNullOrEmpty(frm.Path))
                                {

                                    try
                                    {
                                        FileInfo FI = new FileInfo(frm.Path);

                                        if (!Paths.Contains(FI.Directory.FullName))
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtengo Path:" + FI.Directory.FullName);
                                            Paths.Add(FI.Directory.FullName);

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                      //  ZClass.raiseerror(ex);
                                    }
                                }
                                else if (frm.UseBlob == true)
                                {
                                    string filePath = pathLocal + ((char)92) + frm.TempPathName;
                                    if (File.Exists(filePath))
                                    {
                                        FileInfo fileInfo = new FileInfo(filePath);
                                        if (fileInfo.CreationTime < frm.LastUpdate)
                                        {
                                            if (File.GetAttributes(filePath).ToString().Contains("ReadOnly"))
                                            {
                                                File.SetAttributes(filePath, FileAttributes.Normal);

                                            }
                                            File.Delete(filePath);
                                            FormBusinessExt frmBusinessExt = new FormBusinessExt();
                                            frm.Path = frmBusinessExt.CopyBlobToTemp((ZwebForm)frm, false);
                                            frmBusinessExt = null;
                                        }
                                    }
                                    else
                                    {
                                        FormBusinessExt frmBusinessExt = new FormBusinessExt();
                                        frm.Path = frmBusinessExt.CopyBlobToTemp((ZwebForm)frm, false);
                                        frmBusinessExt = null;
                                    }
                                }
                            }
                        }

                        try
                        {
                            FormsAdditionalFilesBusiness frmAdditionalFilesBusiness = new FormsAdditionalFilesBusiness();
                            List<BlobDocument> lstblobDocuments = frmAdditionalFilesBusiness.GetAllAdditionalFiles();
                            foreach (BlobDocument file in lstblobDocuments)
                            {
                                string directoryPath = pathLocal + file.Description;
                                string filePath = directoryPath + ((char)92) + file.Name;
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                if (File.Exists(filePath))
                                {
                                    FileInfo fileInfo = new FileInfo(filePath);
                                    if (fileInfo.CreationTime < file.UpdateDate)
                                    {
                                        if (File.GetAttributes(filePath).ToString().Contains("ReadOnly"))
                                        {
                                            File.SetAttributes(filePath, FileAttributes.Normal);
                                        }
                                        File.Delete(filePath);
                                        fileInfo = null;
                                        FileEncode.Decode(filePath, file.BlobFile);
                                    }
                                }
                                else
                                {
                                    FileEncode.Decode(filePath, file.BlobFile);
                                }
                                filePath = null;
                                directoryPath = null;
                            }
                            lstblobDocuments = null;
                            frmAdditionalFilesBusiness = null;

                        }
                        catch (Exception ex)
                        {
                           // ZClass.raiseerror(ex);
                            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                        }


                        Boolean AnyFails = false;
                        foreach (string p in Paths)
                        {
                            try
                            {
                                RemoveReadOnlyFlag(new DirectoryInfo(p));
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Copiando Forms: " + p + " " + pathLocal);
                                RecursiveDirectoryCopy(p, pathLocal, true, true);
                            }
                            catch (Exception ex)
                            {
                                AnyFails = true;
                                //ZClass.raiseerror(ex);
                                ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                            }

                        }
                        if (AnyFails == false)
                            UserPreferences.setValue("FormsVersion", FormVersion.ToString(), UPSections.FormPreferences);
                    }
                }
                catch (Exception ex)
                {
                    //ZClass.raiseerror(ex);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
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




		public void RemoveReadOnlyFlag(DirectoryInfo di)
		{
			try
			{

			foreach (DirectoryInfo dif in di.GetDirectories())
			{
				RemoveReadOnlyFlag(dif);
			}

			foreach (FileInfo fi in di.GetFiles())
			{
				fi.Attributes = FileAttributes.Normal;
			}

			di.Attributes = FileAttributes.Normal;
			}
			catch (Exception)
			{
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
                {
                    dDirInfo.Create();
                }
                else
                {
                    try
                    {
                        foreach (var info in dDirInfo.GetFileSystemInfos("*", SearchOption.AllDirectories))
                        {
                            info.Attributes = FileAttributes.Normal;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                dDirInfo = null;
            }
            catch (Exception ex)
            {
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

						RemoveReadOnlyFlag(sDirInfo);

						// Since we are in recursive mode, copy the children also
						RecursiveDirectoryCopy(sDirInfo.FullName, dDirInfo.FullName, fRecursive, overWrite);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            try
            {
                if (dFileInfo != null && File.GetAttributes(dFileInfo.FullName).ToString().Contains("ReadOnly"))
                {
                    File.SetAttributes(dFileInfo.FullName, FileAttributes.Normal);

                }
            }
            catch (Exception)
            {

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
                            try
                            {
                                if (File.GetAttributes(dFileInfo.FullName).ToString().Contains("ReadOnly"))
                                {
                                    File.SetAttributes(dFileInfo.FullName, FileAttributes.Normal);

                                }
                            }
                            catch (Exception)
                            {

                            }
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                }
                finally
                {
                    sFileInfo = null;
                    dFileInfo = null;
                }
            }

            //NetworkConnection = null;

        }

        #endregion


    }
}
