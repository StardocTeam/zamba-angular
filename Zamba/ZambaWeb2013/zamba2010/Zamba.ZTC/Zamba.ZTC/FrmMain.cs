using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Zamba.AdminControls;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.CoreExt;
using Zamba.WFActivity.Regular;
using Zamba.ZTC.Properties;
using Process = System.Diagnostics.Process;


namespace Zamba.ZTC
{
    public partial class FrmMain : Form
    {
        private const string Windowtitle = "Zamba Casos de Prueba";
        public static RadTreeNode CopiedNode = null;
        public string line = String.Empty;
        private Int64 _objectId;
        private Int64 _objectType;
        private string _pass;
        private Int64 _projectId;
        private int userId;

        private bool _rCreate;
        private bool _rDelete;
        private bool _rEdit;
        private bool _rExecute;
        private bool _rView;
        private string _user;
        
        public FrmMain(Int64 objectType, Int64 objectId, string user, string pass,string commandLine)
        {
            try
            {                
                line = commandLine;                
                string status = string.Empty;
                DBBusiness.InitializeSystem(ObjectTypes.ModuleTestCase, null, false, ref status, new ErrorReportBusiness());

                ControlsFactory.InstanceEntityConnection();
                InitializeComponent();

                SetWindowTitle();

                _objectType = objectType;
                _objectId = objectId;

                _user = user;
                _pass = pass;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public FrmMain(string commandLine)
        {
            try
            {                
                line = commandLine;               
                string status = string.Empty;
                DBBusiness.InitializeSystem(ObjectTypes.ModuleTestCase, null, false, ref status, new ErrorReportBusiness());

                ControlsFactory.InstanceEntityConnection();
                InitializeComponent();
                //InitializeSystem();
                SetWindowTitle();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Agrega los datos adicionales al titulo de la aplicacion
        /// </summary>
        private void SetWindowTitle()
        {
            Text = Windowtitle + Resources.FrmMain_SetWindowTitle__Openparenthesis + Application.ProductVersion +
                   Resources.FrmMain_SetWindowTitle__Closeparenthesis;

            string environmentTitle = ZOptBusiness.GetValue("TestCaseTitle");
            if (string.IsNullOrEmpty(environmentTitle))
                Text = Text;
            else
                Text = Text + Resources.FrmMain_SetWindowTitle____hyphen + environmentTitle;
        }


        /// <summary>
        /// Carga los valores por defecto y el logueo del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTestLoad(object sender, EventArgs e)
        {
            Enabled = false;
            if (!String.IsNullOrEmpty(line))
            {
                line = line.ToUpper();
                //TRATA DE OBTENER EL USERID MANDADO COMO PARAMETRO
                if (line.Contains("USERID="))
                {
                    userId = int.Parse((line.Split(Char.Parse("="))[1]).Split(Char.Parse(" "))[0]);
                    //Remuevo los caracteres que no sirven para los parámetros.
                    line = line.Replace("USERID=" + userId.ToString(), "");
                    line = line.Trim();
                    if (!UserBusiness.Rights.ValidateLogIn(userId, ClientType.Desktop).Equals(null))
                    {
                        /*int timeout = int.Parse(UserPreferences.getValue("TimeOut", Sections.UserPreferences, "20"));
                        string winusername = UserGroupBusiness.GetUserorGroupNamebyId(userId);
                        UcmServices.Login(timeout, "ZTC", userId, winusername, Environment.MachineName, 0, ServiceTypes.Report);*/
                    }
                    else
                    {
                        MessageBox.Show("No se pudo validar el usuario de Zamba, se cerrara la aplicacion", "ATENCION", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Application.Exit();
                    }
                }
            }
            else
            {
                try
                {
                    if (DoLogin())
                    {
                        try
                        {
                            _user = Membership.MembershipHelper.CurrentUser.Name;
                            _pass = Membership.MembershipHelper.CurrentUser.Password;
                            Text = Text + Resources.FrmMain_FrmTestLoad_____ + _user + Resources.FrmMain_FrmTestLoad__;
                            _rView = RightsBusiness.GetUserRights(ObjectTypes.TestCase, RightsType.View);
                            _rEdit = RightsBusiness.GetUserRights(ObjectTypes.TestCase, RightsType.Edit);
                            _rCreate = RightsBusiness.GetUserRights(ObjectTypes.TestCase, RightsType.Create);
                            _rDelete = RightsBusiness.GetUserRights(ObjectTypes.TestCase, RightsType.Delete);
                            _rExecute = RightsBusiness.GetUserRights(ObjectTypes.TestCase, RightsType.Execute);

                            if (!_rView)
                            {
                                MessageBox.Show(
                                    Resources.
                                        FrmMain_FrmTestLoad_Usted_no_posee_permisos_para_este_modulo__Por_favor_contactese_con_el_Administrador_,
                                    Resources.FrmMain_FrmTestLoad_Atención, MessageBoxButtons.OK);
                                Close();
                            }

                            if (_objectId > 0 && _objectType > 0)
                            {
                                string objectName = TCBusiness.GetObjectName(_objectType, _objectId);

                                IQueryable<ZTC_CT> q = from c in ControlsFactory.dbContext.ZTC_CT
                                                       where
                                                           c.ObjectID == _objectId && c.ObjectTypeID == _objectType &&
                                                           c.NodeType == 2
                                                       select c;
                                IEnumerable<ZTC_CT> cTs = q.ToList();


                                Int64 currentprojectid = 0;
                                //Si el Usuario no posee derechos de creación no se visualiza el formulario para seleccionar el proyecto.
                                //El formulario para seleecionar el proyecto sirve para la creacion de casos de prueba y 
                                //es el encargado de realizar la vinculación del proyecto con el caso de prueba
                                if (_rCreate)
                                {
                                    if (cTs.Any())
                                    {
                                        ZTC_CT ct = cTs.First();
                                        currentprojectid = (Int64)TCBusiness.GetProjectId(ct.TestCaseId, ct.ObjectTypeID);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var selector = new FrmProjectSelector();

                                            if (selector.ShowDialog() == DialogResult.OK)
                                            {
                                                currentprojectid = selector.CurrentProjectId();
                                                _projectId = currentprojectid;

                                            }
                                            else
                                            {
                                                Close();
                                            }

                                            selector.Dispose();
                                            selector.Close();

                                        }
                                        catch (Exception ex)
                                        {
                                            ZClass.raiseerror(ex);
                                        }

                                    }
                                }

                                var ucTestCase = new UcTestCase(_objectType, _objectId, objectName, currentprojectid, _rEdit,
                                                                _rCreate, _rDelete, _rExecute)
                                { Dock = DockStyle.Fill };

                                ucTestCase.TestCaseDeleted += UcTestCaseTestCaseDeleted;

                                var tabPage = new RadPageViewPage { Text = ucTestCase.TestCaseName };
                                radPageView1.Pages.Add(tabPage);
                                tabPage.Controls.Add(ucTestCase);

                                WindowState = FormWindowState.Maximized;
                                radPageView1.SelectedPage = tabPage;
                            }

                            Show();
                            LoadProjectAndObjects();
                            SetExportTypes();

                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    Application.Exit();
                }
            }
        }

        private void LoadProjectAndObjects()
        {
            try
            {
                cmbTypes.SelectedIndexChanged -= CmbTypesSelectedIndexChanged;
                LoadObjectTypes();
                if (cmbTypes.ComboBox != null)
                    Int64.TryParse(cmbTypes.ComboBox.SelectedValue.ToString(), out _objectType);
                cmbTypes.SelectedIndexChanged += CmbTypesSelectedIndexChanged;

                cmbProjects.SelectedIndexChanged -= CmbProjectsSelectedIndexChanged;
                LoadProjects();
                if (cmbProjects.ComboBox != null)
                    Int64.TryParse(cmbProjects.ComboBox.SelectedValue.ToString(), out _projectId);
                cmbProjects.SelectedIndexChanged += CmbProjectsSelectedIndexChanged;
                RefreshTcGrid();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        //Se agrega try catch faltante.
        private void SetExportTypes()
        {
            try
            {
                var list = Enum.GetValues(typeof(ExportTypes)).Cast<object>().ToDictionary(v => (int)v,
                                                                                            v =>
                                                                                            Enum.GetName(
                                                                                                typeof(ExportTypes),
                                                                                                (int)v));
                if (cmbExportTypes.ComboBox != null)
                {
                    cmbExportTypes.ComboBox.DisplayMember = "Value";
                    cmbExportTypes.ComboBox.ValueMember = "Key";
                    cmbExportTypes.ComboBox.DataSource = new BindingSource(list, null);

                    cmbExportTypes.ComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        //''' <summary>
        //''' Realiza el login de la aplicacion
        //''' </summary>
        //''' <remarks></remarks>
        private Boolean DoLogin()
        {

            System.Reflection.Assembly assem = new RulesInstance().GetWFActivityRegularAssembly();
            try
            {
                Login frmLogin;
                if (_user != null)
                {
                    frmLogin = new Login(false, true, false, ObjectTypes.TestCase, _user, _pass, Environment.MachineName, assem);
                    frmLogin.Login();
                }
                else
                {
                    frmLogin = new Login(false, false, false, ObjectTypes.TestCase, string.Empty, string.Empty, string.Empty, assem);
                    if (frmLogin.IsDisposed == false) frmLogin.ShowDialog();
                    if (frmLogin.ShowDialog() == DialogResult.Cancel) return false;
                }

                //'Valido que los valores del app.ini coincide con los guardados en la base
                if (UserBusiness.ValidateDataBase())
                {
                    if (Enabled == false)
                    {
                        Enabled = true;
                    }
                    return true;
                }

                String contacto = ZOptBusiness.GetValue("ContactoValidacion");
                String contactoMail = ZOptBusiness.GetValue("MailContactoValidacion");

                if (String.IsNullOrEmpty(contacto))
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.", "Atencion");
                else if (String.IsNullOrEmpty(contactoMail))
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " + contacto, "Atencion");
                else
                    MessageBox.Show("Su instalacion no se encuentra registrada, por favor contactese con " + contacto + " al correo electronico " + contactoMail, "Atencion");

                return false;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return false;
            }
        }

        private void LoadObjectTypes()
        {
            try
            {
                var q = from c in ControlsFactory.dbContext.OBJECTTYPES orderby c.OBJECTTYPES1
                                                   select c;


                List<OBJECTTYPES> alllist = q.AsEnumerable().ToList();
                alllist.Insert(0, new OBJECTTYPES { OBJECTTYPES1 = "Todos", OBJECTTYPESID = 0 });

                IEnumerable<OBJECTTYPES> objecttypes = alllist.ToList();

                if (cmbTypes.ComboBox != null)
                {
                    cmbTypes.ComboBox.DisplayMember = "OBJECTTYPES1";
                    cmbTypes.ComboBox.ValueMember = "OBJECTTYPESID";
                    cmbTypes.ComboBox.DataSource = objecttypes;
                }

                //this.cmbTypes.SelectedValue = (long)ObjectTypes.Todos;
                String lastTypeUsed = UserPreferences.getValue("UltimoTipoUtilizadoZTC", Sections.UserPreferences, "0");

                if (String.IsNullOrEmpty(lastTypeUsed) == false)
                {
                    if (cmbTypes.ComboBox != null)
                        foreach (OBJECTTYPES value in cmbTypes.ComboBox.Items)
                        {
                            if (value.OBJECTTYPESID.ToString(CultureInfo.InvariantCulture) == lastTypeUsed)
                            {
                                cmbTypes.ComboBox.SelectedItem = value;
                                _objectType = (Int64)value.OBJECTTYPESID;
                                break;
                            }
                        }
                }
                //Se agrega el cargado de los objetos para que se grisen si corresponden o no.
                if (cmbTypes.ComboBox != null) LoadObjects(Int64.Parse(cmbTypes.ComboBox.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void LoadProjects()
        {
            try
            {
                IOrderedQueryable<PRJ_TBL> q = from c in ControlsFactory.dbContext.PRJ_TBL
                                               orderby c.Name
                                               select c;
                IEnumerable<PRJ_TBL> projects = q.ToList();

                if (cmbProjects.ComboBox != null)
                {
                    cmbProjects.ComboBox.DisplayMember = "Name";
                    cmbProjects.ComboBox.ValueMember = "Prj_ID";
                    cmbProjects.ComboBox.DataSource = projects;
                }

                String lastProjectUsed = UserPreferences.getValue("UltimoProyectoUtilizadoZTC", Sections.UserPreferences,
                                                                  "0");

                if (String.IsNullOrEmpty(lastProjectUsed) == false)
                {
                    if (cmbProjects.ComboBox != null)
                        foreach (PRJ_TBL value in cmbProjects.ComboBox.Items)
                        {
                            if (value.Prj_ID.ToString(CultureInfo.InvariantCulture) == lastProjectUsed)
                            {
                                cmbProjects.ComboBox.SelectedItem = value;
                                //Se almacena el ProjectId del ultimo utilizado.
                                _projectId = (Int64)value.Prj_ID;
                                break;
                            }
                        }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        private void RefreshTcGrid()
        {
            try
            {
                MasterTemplate.BeginEdit();


                if (_projectId != -1)
                {
                    if (_objectType != 0)
                    {
                        //Si el proyecto es Zamba
                        if (_projectId == 0)
                        {
                            MasterTemplate.DataSource =
                                (from t in ControlsFactory.dbContext.ZTC_CT
                                 join u in ControlsFactory.dbContext.USRTABLE on t.Author equals u.ID
                                 join o in ControlsFactory.dbContext.OBJECTTYPES on t.ObjectTypeID equals o.OBJECTTYPESID
                                 join c in ControlsFactory.dbContext.ZTC_CT on t.ParentNode equals c.TestCaseId
                                 join e in ControlsFactory.dbContext.ZTC_EX on
                                     (from tab1 in ControlsFactory.dbContext.ZTC_EX
                                      where tab1.TestCaseID == t.TestCaseId
                                      select tab1.ExecutionDate).Max() equals e.ExecutionDate
                                     into eq
                                 from nq in eq.DefaultIfEmpty()
                                 join r in ControlsFactory.dbContext.ZTC_RS on nq.ExecutionResultID equals
                                     r.ExecutionResultID
                                     into eq2
                                 from nq2 in eq2.DefaultIfEmpty()
                                 join ut in ControlsFactory.dbContext.USRTABLE on nq.UserId equals ut.ID
                                     into eq3
                                 from nq3 in eq3.DefaultIfEmpty()
                                 where t.NodeType == 2
                                       //&& p.PRJID == (decimal)_projectId
                                       && t.ObjectTypeID == (decimal)_objectType
                                 orderby t.TestOrder, t.NodeName, t.UpdateDate
                                 select new
                                 {
                                     Id = t.TestCaseId,
                                     Categoria = c.NodeName,
                                     Nombre = t.NodeName,
                                     Descripcion = t.NodeDescription,
                                     //Creado = t.CreateDate,
                                     Modificado = t.UpdateDate,
                                     Autor = u.NOMBRES + " " + u.APELLIDO,
                                     Tipo = o.OBJECTTYPES1,
                                     Ejecutado =
                          (nq.ExecutionDate != null) ? nq.ExecutionDate : new DateTime(1900, 1, 1),
                                     //  Usuario = (nq.UserId != null) ? nq.UserId : 0,
                                     Usuario = nq3.NOMBRES + " " + nq3.APELLIDO,
                                     Resultado = (nq2.ExecutionResultName == null) ? "" : nq2.ExecutionResultName,
                                     ObjectTypeID = o.OBJECTTYPESID,
                                     ObjectId = t.ObjectID,
                                     Orden = t.TestOrder,
                                 }).Distinct();
                        }
                        else
                        {
                            MasterTemplate.DataSource =
                                (from t in ControlsFactory.dbContext.ZTC_CT
                                 join p in ControlsFactory.dbContext.PRJ_R_O on
                                        //new {objt = t.ObjectTypeID, objid = t.TestCaseId} equals
                                        //new {objt = p.OBJTYP, objid = p.OBJID}
                                        new { objt = t.ObjectTypeID } equals
                                        new { objt = p.OBJTYP }
                                 join u in ControlsFactory.dbContext.USRTABLE on t.Author equals u.ID
                                 join o in ControlsFactory.dbContext.OBJECTTYPES on t.ObjectTypeID equals o.OBJECTTYPESID
                                 join c in ControlsFactory.dbContext.ZTC_CT on t.ParentNode equals c.TestCaseId
                                 join e in ControlsFactory.dbContext.ZTC_EX on
                                     (from tab1 in ControlsFactory.dbContext.ZTC_EX
                                      where tab1.TestCaseID == t.TestCaseId
                                      select tab1.ExecutionDate).Max() equals e.ExecutionDate
                                     into eq
                                 from nq in eq.DefaultIfEmpty()
                                 join r in ControlsFactory.dbContext.ZTC_RS on nq.ExecutionResultID equals
                                     r.ExecutionResultID
                                     into eq2
                                 from nq2 in eq2.DefaultIfEmpty()
                                 join ut in ControlsFactory.dbContext.USRTABLE on nq.UserId equals ut.ID
                                     into eq3
                                 from nq3 in eq3.DefaultIfEmpty()
                                 where t.NodeType == 2
                                       && p.PRJID == (decimal)_projectId
                                       && t.ObjectTypeID == (decimal)_objectType
                                 orderby t.TestOrder, p.PRJID, p.OBJTYP, p.OBJID, t.NodeName, t.UpdateDate
                                 select new
                                 {
                                     Id = t.TestCaseId,
                                     Categoria = c.NodeName,
                                     Nombre = t.NodeName,
                                     Descripcion = t.NodeDescription,
                                     //Creado = t.CreateDate,
                                     Modificado = t.UpdateDate,
                                     Autor = u.NOMBRES + " " + u.APELLIDO,
                                     Tipo = o.OBJECTTYPES1,
                                     Ejecutado =
                                     (nq.ExecutionDate != null) ? nq.ExecutionDate : new DateTime(1900, 1, 1),
                                     //  Usuario = (nq.UserId != null) ? nq.UserId : 0,
                                     Usuario = nq3.NOMBRES + " " + nq3.APELLIDO,
                                     Resultado = (nq2.ExecutionResultName == null) ? "" : nq2.ExecutionResultName,
                                     ObjectTypeID = o.OBJECTTYPESID,
                                     ObjectId = t.ObjectID,
                                     Orden = t.TestOrder,
                                 }).Distinct();
                        }
                    }
                    else
                    {
                        //Si el proyecto es Zamba
                        if (_projectId == 0)
                        {
                            MasterTemplate.DataSource =
                                (from t in ControlsFactory.dbContext.ZTC_CT
                                     //join p in ControlsFactory.dbContext.PRJ_R_O on
                                     //new {objid = t.TestCaseId} equals
                                     //new { objid = p.OBJID}
                                     //new { objt = t.ObjectTypeID } equals
                                     //new { objt = p.OBJTYP }
                                 join u in ControlsFactory.dbContext.USRTABLE on t.Author equals u.ID
                                 join o in ControlsFactory.dbContext.OBJECTTYPES on t.ObjectTypeID equals o.OBJECTTYPESID
                                 join c in ControlsFactory.dbContext.ZTC_CT on t.ParentNode equals c.TestCaseId
                                 join e in ControlsFactory.dbContext.ZTC_EX on
                                     (from tab1 in ControlsFactory.dbContext.ZTC_EX
                                      where tab1.TestCaseID == t.TestCaseId
                                      select tab1.ExecutionDate).Max() equals e.ExecutionDate
                                     into eq
                                 from nq in eq.DefaultIfEmpty()
                                 join r in ControlsFactory.dbContext.ZTC_RS on nq.ExecutionResultID equals
                                     r.ExecutionResultID
                                     into eq2
                                 from nq2 in eq2.DefaultIfEmpty()
                                 join ut in ControlsFactory.dbContext.USRTABLE on nq.UserId equals ut.ID
                                     into eq3
                                 from nq3 in eq3.DefaultIfEmpty()
                                 where t.NodeType == 2
                                       //&& t.PRJID == (decimal)_projectId
                                       && (t.ObjectTypeID == 105 || t.ObjectTypeID == 109)
                                 orderby t.TestOrder, t.NodeName, t.UpdateDate
                                 select new
                                 {
                                     Id = t.TestCaseId,
                                     Categoria = c.NodeName,
                                     Nombre = t.NodeName,
                                     Tipo = o.OBJECTTYPES1,
                                     //Creado = t.CreateDate,
                                     Modificado = t.UpdateDate,
                                     Autor = u.NOMBRES + " " + u.APELLIDO,
                                     Descripcion = t.NodeDescription,
                                     Ejecutado =
                          (nq.ExecutionDate != null) ? nq.ExecutionDate : new DateTime(1900, 1, 1),
                                     //  Usuario = (nq.UserId != null) ? nq.UserId : 0,
                                     Usuario = nq3.NOMBRES + " " + nq3.APELLIDO,
                                     Resultado = (nq2.ExecutionResultName == null) ? "" : nq2.ExecutionResultName,
                                     Orden = t.TestOrder,
                                     ObjectTypeID = o.OBJECTTYPESID,
                                     ObjectId = t.ObjectID,
                                 }).Distinct();
                        }
                        else
                        {
                            MasterTemplate.DataSource =
                                (from t in ControlsFactory.dbContext.ZTC_CT
                                 join p in ControlsFactory.dbContext.PRJ_R_O on
                                        //new {objt = t.ObjectTypeID, objid = t.TestCaseId} equals
                                        //new {objt = p.OBJTYP, objid = p.OBJID}
                                        new { objt = t.ObjectTypeID } equals
                                        new { objt = p.OBJTYP }
                                 join u in ControlsFactory.dbContext.USRTABLE on t.Author equals u.ID
                                 join o in ControlsFactory.dbContext.OBJECTTYPES on t.ObjectTypeID equals o.OBJECTTYPESID
                                 join c in ControlsFactory.dbContext.ZTC_CT on t.ParentNode equals c.TestCaseId
                                 join e in ControlsFactory.dbContext.ZTC_EX on
                                     (from tab1 in ControlsFactory.dbContext.ZTC_EX
                                      where tab1.TestCaseID == t.TestCaseId
                                      select tab1.ExecutionDate).Max() equals e.ExecutionDate
                                     into eq
                                 from nq in eq.DefaultIfEmpty()
                                 join r in ControlsFactory.dbContext.ZTC_RS on nq.ExecutionResultID equals
                                     r.ExecutionResultID
                                     into eq2
                                 from nq2 in eq2.DefaultIfEmpty()
                                 join ut in ControlsFactory.dbContext.USRTABLE on nq.UserId equals ut.ID
                                     into eq3
                                 from nq3 in eq3.DefaultIfEmpty()
                                 where t.NodeType == 2
                                       && p.PRJID == (decimal)_projectId
                                 orderby t.TestOrder, p.PRJID, p.OBJTYP, p.OBJID, t.NodeName, t.UpdateDate
                                 select new
                                 {
                                     Id = t.TestCaseId,
                                     Categoria = c.NodeName,
                                     Nombre = t.NodeName,
                                     Tipo = o.OBJECTTYPES1,
                                     //Creado = t.CreateDate,
                                     Modificado = t.UpdateDate,
                                     Autor = u.NOMBRES + " " + u.APELLIDO,
                                     Descripcion = t.NodeDescription,
                                     Ejecutado =
                                     (nq.ExecutionDate != null) ? nq.ExecutionDate : new DateTime(1900, 1, 1),
                                     //  Usuario = (nq.UserId != null) ? nq.UserId : 0,
                                     Usuario = nq3.NOMBRES + " " + nq3.APELLIDO,
                                     Resultado = (nq2.ExecutionResultName == null) ? "" : nq2.ExecutionResultName,
                                     Orden = t.TestOrder,
                                     ObjectTypeID = o.OBJECTTYPESID,
                                     ObjectId = t.ObjectID,
                                 }).Distinct();
                        }
                    }
                    MasterTemplate.EndEdit();

                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Le da un formato, ordenamiento y agrupamiento por defecto a la grilla
        /// </summary>
        /// <history>Marcelo    01/11/12 Modified</history>
        private void FormatGrid()
        {
            if (MasterTemplate.GroupDescriptors.Count == 0)
            {
                var descriptor = new GroupDescriptor();
                var sort = new SortDescriptor("Orden", ListSortDirection.Ascending);
                var sort2 = new SortDescriptor("Id", ListSortDirection.Ascending);

                descriptor.GroupNames.Add("Categoria", ListSortDirection.Ascending);
                descriptor.GroupNames.Add("Tipo", ListSortDirection.Ascending);
                MasterTemplate.GroupDescriptors.Add(descriptor);
                MasterTemplate.SortDescriptors.Add(sort);
                MasterTemplate.SortDescriptors.Add(sort2);
            }

            MasterTemplate.Columns[0].Width = 40;
            MasterTemplate.Columns[1].BestFit();
            MasterTemplate.Columns[MasterTemplate.Columns.Count - 1].IsVisible = false;
            MasterTemplate.Columns[MasterTemplate.Columns.Count - 2].IsVisible = false;
            MasterTemplate.Columns[MasterTemplate.Columns.Count - 3].IsVisible = false;
        }

        public static String GetObjectName(Int64 objectTypeId, Int64 objectId)
        {
            try
            {
                switch (objectTypeId)
                {
                    case 1:
                        return "Indice: " + IndexsBusiness.GetIndexNameById(objectId);
                    case 2:
                        return " Tipo de Documento: " + DocTypesBusiness.GetDocTypeName(objectId, true);
                    case 3:
                    //Implementar...
                    //return "Archivo: " + FileBusiness.GetUniqueFileName
                    case 5:
                        return "Usuario: " + UserGroupBusiness.GetUserorGroupNamebyId(objectId);
                    case 42:
                        return "Etapa: " + WFStepBusiness.GetStepNameById(objectId);
                    case 43:
                        return "Proceso: " + WFRulesBusiness.GetRuleNameById(objectId);
                    case 52:
                        ZwebForm form = FormBusiness.GetForm(objectId);
                        return "Formulario: " + TCBusiness.GetFormNameType(form);
                    case 55:
                        return "Workflow: " + WFBusiness.GetWorkflowNameByWFId(objectId);
                    case 61:
                        return "Módulo de Reportes: ";
                    case 103:
                        return "Grupo de Usuario: " + UserGroupBusiness.GetUserorGroupNamebyId(objectId);
                }


                //string nombre = IndexsBusiness.GetIndexNameById(ObjectId);

                //GetIndexsDsIdsAndNames()
                //return nombre;
                return string.Empty;
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Va al caso de test especificado
        /// </summary>
        public bool Validate(string var1, string var2)
        {
            if (var2 != null && String.CompareOrdinal(var1, var2) == 0)
                return true;
            return false;
        }


        /// <summary>
        /// Evito cerrar el page principal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadPageView1PageRemoving(object sender, RadPageViewCancelEventArgs e)
        {
            try
            {
                //Ezequiel: Si el tabpage es el principal evito cerrarlo
                if (e.Page.TabIndex == 0)
                {
                    e.Cancel = true;
                }
                else
                {
                    foreach (var c in e.Page.Controls)
                    {
                        var ucTestCase = c as UcTestCase;
                        if (ucTestCase != null)
                            ucTestCase.TestCaseDeleted -= UcTestCaseTestCaseDeleted;

                        if (ucTestCase != null) ucTestCase.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Procedimiento que carga todos los objetos que se van a utilizar en sus respectivos combobox.
        /// </summary>
        public void LoadObjects(Int64 objectTypeId)
        {
            try
            {
                //Limpio el dataSource para que no se produzcan errores con los valuemembers y Displaymembers 

                if (cmbObjects.ComboBox != null) cmbObjects.ComboBox.DataSource = null;

                if (objectTypeId != 0)
                {
                    cmbObjects.Enabled = true;
                    txtObject.Enabled = true;
                    btnNewTestCase.Enabled = true;
                }
                else
                {
                    cmbObjects.Enabled = false;
                    txtObject.Enabled = false;
                    btnNewTestCase.Enabled = false;
                }

                switch (objectTypeId)
                {
                    case 0:
                        cmbObjects.Text = "";
                        break;

                    case 1:

                        //this.cmbObjects.DisplayMember = "";
                        //this.cmbObjects.ValueMember = "";
                        //this.cmbObjects.DataSource = IndexsBusiness.GetAllIndexsIdsAndNames();
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "index_name";
                            cmbObjects.ComboBox.ValueMember = "index_id";
                            cmbObjects.ComboBox.DataSource = IndexsBusiness.GetIndexIdAndName();
                        }

                        break;

                    case 2:
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "doc_type_name";
                            cmbObjects.ComboBox.ValueMember = "doc_type_id";
                            cmbObjects.ComboBox.DataSource = DocTypesBusiness.GetAllDocTypes().Tables[0];
                        }


                        break;

                    //case 5:

                    //    this.cmbObjects.DataSource = UserBusiness.GetAllUsersArray();
                    //    this.cmbObjects.DisplayMember = "name";
                    //    this.cmbObjects.ValueMember = "id";
                    //    break;

                    case 42:
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "Name";
                            cmbObjects.ComboBox.ValueMember = "Step_Id";
                            cmbObjects.ComboBox.DataSource = WFStepBusiness.GetDsAllSteps().Tables[0];
                        }

                        break;

                    case 43:
                        IOrderedQueryable<WFRules> query = from r in ControlsFactory.dbContext.WFRules
                                                           where r.Class == "DoDesign"
                                                           orderby r.Name
                                                           select r;
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "Name";
                            cmbObjects.ComboBox.ValueMember = "Id";
                            cmbObjects.ComboBox.DataSource = query.ToList();
                        }


                        break;

                    case 52:
                        //return "Formulario: " + FormBusiness.GetForm(ObjectId).Name;
                        var forms = FormBusiness.GetForms(true);

                        forms = SetNamesFormsTypes(forms);
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "Name";
                            cmbObjects.ComboBox.ValueMember = "Id";
                            cmbObjects.ComboBox.DataSource = forms;
                        }


                        break;

                    case 55:
                        IQueryable<string> query1 = (from r in ControlsFactory.dbContext.WFRules                                                        
                                                    orderby r.Class
                                                    select r.Class).Distinct();
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "Name";
                            cmbObjects.ComboBox.ValueMember = "Id";
                            cmbObjects.ComboBox.DataSource = query1.ToList();
                        }


                        break;


                    case 103:
                        if (cmbObjects.ComboBox != null)
                        {
                            cmbObjects.ComboBox.DisplayMember = "Name";
                            cmbObjects.ComboBox.ValueMember = "Id";
                            cmbObjects.ComboBox.DataSource = UserGroupBusiness.GetAllGroupsArrayList();
                        }

                        break;
                    case 105:
                    case 109:
                        if (cmbObjects.ComboBox != null)
                        {
                            TestCaseBusiness wfstExt = new TestCaseBusiness();
                            try
                            {
                                cmbObjects.ComboBox.DisplayMember = "Name";
                                cmbObjects.ComboBox.ValueMember = "Id";
                                cmbObjects.ComboBox.DataSource = wfstExt.GetGeneralTypes();
                            }
                            finally
                            {
                                wfstExt = null;
                            }
                        }
                        break;

                    default:
                        cmbObjects.Items.Add("Todos");                        
                        break;

                }
                
                if (cmbObjects.Items.Count.Equals(0))
                {
                    cmbObjects.Items.Add("Todos");
                }
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private static List<ZwebForm> SetNamesFormsTypes(List<ZwebForm> forms)
        {
            foreach (ZwebForm form in forms)
            {
                form.Name = TCBusiness.GetFormNameType(form);
            }

            return forms;
        }


        /// <summary>
        /// Refresca la grilla de casos de prueba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefreshGridClick(object sender, EventArgs e)
        {
            try
            {
                RefreshTcGrid();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Exporta el contenido de la grilla a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExportClick(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            try
            {
                sfd.Title = Resources.FrmMain_BtnExportClick_Ingrese_la_ruta_y_el_nombre_del_archivo_de_Excel;
                if (cmbExportTypes.ComboBox != null)
                {
                    switch (
                        (ExportTypes)Enum.Parse(typeof(ExportTypes), cmbExportTypes.ComboBox.SelectedValue.ToString())
                        )
                    {
                        case ExportTypes.CSV:
                            sfd.Filter = Resources.FrmMain_BtnExportClick_CSV_files____csv____csv;
                            break;
                        case ExportTypes.Excel:
                            sfd.Filter = Resources.FrmMain_BtnExportClick_excel_files____xls____xls;
                            break;
                        case ExportTypes.PDF:
                            sfd.Filter = Resources.FrmMain_BtnExportClick_PDF_files____pdf____pdf;
                            break;
                        case ExportTypes.Word:
                            sfd.Filter = Resources.FrmMain_BtnExportClick_Word_files____doc____doc;
                            break;
                    }

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var tcb = new TCBusiness();
                        MessageBox.Show(tcb.Export_Excel(sfd.FileName, MasterTemplate,
                                                         (ExportTypes)
                                                         Enum.Parse(typeof(ExportTypes),
                                                                    cmbExportTypes.ComboBox.SelectedValue.ToString()))
                                            ? "Exportacion realizada con exito"
                                            : "Ha ocurrido un error en la exportacion",
                                        Resources.FrmMain_FrmTestLoad_Zamba_Software,
                                        MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                sfd.Dispose();
            }
        }

        public new bool Validate()
        {
            if (txtObject.Text.Length > 18)
            {
                MessageBox.Show(Resources.FrmMain_Validate_Id_maximo_soportado_18_caracteres,
                                Resources.FrmMain_FrmTestLoad_Atención, MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }

        private void UcTestCaseTestCaseDeleted(object sender, EventArgs e)
        {
            RefreshTcGrid();
        }

        private void RadGridView1ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            try
            {
                var cell = sender as GridDataCellElement;
                if (cell != null)
                {
                    var dataCell = sender as GridDataCellElement;
                    if (dataCell.Value != null)
                        e.ToolTipText = dataCell.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void MasterTemplateCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.Column.Name == "Ejecutado" && e.CellElement.Value.ToString().Contains("01/01/1900"))
                {
                    e.CellElement.ToolTipText = "";
                    e.CellElement.Text = "";
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void FrmMainFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                ZTrace.RemoveListener("ZTC");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }



        private void CommandBarButton1Click(object sender, EventArgs e)
        {
            try
            {
                if (_objectType == 0)
                {
                    MessageBox.Show(
                        Resources.
                            FrmMain_CommandBarButton1Click_Por_favor_seleccione_un_tipo_específico_para_listar_los_casos_de_prueba_,
                        Resources.FrmMain_CommandBarButton1Click_Listado_de_Casos_de_Prueba, MessageBoxButtons.OK);
                    return;
                }

                frmSelectReportType srt = new frmSelectReportType();


                if (srt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var wait = new WaitForm("Cargando");


                    var tcr = new TCResumeBusiness();
                    var sf = new SaveFileDialog
                    {
                        AddExtension = true,
                        AutoUpgradeEnabled = true,
                        CheckPathExists = true,
                        DefaultExt = ".htm",
                        FileName = "Listado de caso de Pruebas.htm",
                        RestoreDirectory = true
                    };

                    DialogResult dialogResult = sf.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {

                        tcr.PrintResume(Membership.MembershipHelper.CurrentUser, _projectId, _objectType, sf.FileName,
                                     srt.selectedResumeMode);

                        Process.Start(sf.FileName);
                    }
                    wait.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    Resources.
                        FrmMain_CommandBarButton1Click_Ocurrio_un_error_al_generar_el_informe__por_favor_contactese_con_el_administrador_del_sistema,
                    Resources.FrmMain_CommandBarButton1Click_Error, MessageBoxButtons.OK);
                ZClass.raiseerror(ex);
            }
        }

        private void BtnNewTestCaseClick(object sender, EventArgs e)
        {

            try
            {
                if (Validate())
                    if (!String.IsNullOrEmpty(txtObject.Text.Trim()) || _objectId >=0)
                    {
                        Int64 objType = _objectType;

                        var objectId =
                            Int64.Parse(string.IsNullOrEmpty(txtObject.Text.Trim())
                                            ? _objectId.ToString(CultureInfo.InvariantCulture)
                                            : txtObject.Text);

                        string objectName = TCBusiness.GetObjectName(objType, objectId);
                        //var ObjectName = GetObjectName(objType, objectId);

                        if (objectId != 0 || _objectType != 0)
                        {
                            //esto es suceptible a error porque podria cargar para un objeto que esta tambien vinculado a otro proyecto.
                            //el proyecto se toma del combo, porque estoy agregando uno nuevo
                            var ucTestCase = new UcTestCase(objType, objectId, objectName, _projectId, _rEdit, _rCreate,
                                                            _rDelete, _rExecute)
                            { Dock = DockStyle.Fill };

                            ucTestCase.TestCaseDeleted += UcTestCaseTestCaseDeleted;

                            bool exist = false;

                            for (int i = 0; i <= (radPageView1.Pages.Count) - 1; i++)
                            {
                                if (Validate(radPageView1.Pages[i].Text, ucTestCase.TestCaseName))
                                {
                                    radPageView1.SelectedPage = radPageView1.Pages[i];
                                    exist = true;
                                    break;
                                }
                            }
                            if (!exist)
                            {
                                var tabPage = new RadPageViewPage { Text = ucTestCase.TestCaseName };
                                radPageView1.Pages.Add(tabPage);
                                tabPage.Controls.Add(ucTestCase);
                                radPageView1.SelectedPage = tabPage;
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                Resources.FrmMain_btnNewTestCase_Click_Debe_seleccionar_un_tipo_de_objeto_y_objeto,
                                Resources.FrmMain_btnNewTestCase_Click_Advertencia,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    else
                    {
                        MessageBox.Show(
                            Resources.
                                FrmMain_btnNewTestCase_Click_Se_debe_ingresar_un_ID_para_el_objeto_al_que_se_desea_generar_un_caso_de_prueba,
                            Resources.FrmMain_btnNewTestCase_Click_ATENCION);
                    }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }


        }


        private void CmbObjectsSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cmbObjects.ComboBox != null && _objectType != 0 && cmbObjects.ComboBox.SelectedValue != null)
                {
                    _objectId = Int64.Parse(cmbObjects.ComboBox.SelectedValue.ToString());

                }
                else
                {
                    _objectId = 0;
                }

            }

            catch
                (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private
            void CmbTypesSelectedIndexChanged
            (object sender, EventArgs e)
        {

            var wait = new WaitForm("Cargando");
            try
            {
                if (cmbTypes.ComboBox != null &&
                    (radPageView1.SelectedPage.Name == "TabTC" && cmbTypes.ComboBox.SelectedValue != null))
                {
                    _objectType = Int64.Parse(cmbTypes.ComboBox.SelectedValue.ToString());
                    RefreshTcGrid();
                    LoadObjects(Int64.Parse(cmbTypes.ComboBox.SelectedValue.ToString()));
                    UserPreferences.setValue("UltimoTipoUtilizadoZTC", cmbTypes.ComboBox.SelectedValue.ToString(),
                                             Sections.UserPreferences);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            wait.Close();
        }

        private
            void CmbProjectsSelectedIndexChanged
            (object sender, EventArgs e)
        {


            var wait = new WaitForm("Cargando");
            try
            {
                if (cmbProjects.ComboBox != null &&
                    (radPageView1.SelectedPage.Name == "TabTC" && cmbProjects.ComboBox.SelectedValue != null))
                {
                    _projectId = Int64.Parse(cmbProjects.ComboBox.SelectedValue.ToString());

                    RefreshTcGrid();
                    UserPreferences.setValue("UltimoProyectoUtilizadoZTC",
                                             cmbProjects.ComboBox.SelectedValue.ToString(),
                                             Sections.UserPreferences);

                }
                else
                {
                    _projectId = -1;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            wait.Close();
        }

        /// <summary>
        /// Evento doble click de la grilla de casos de prueba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterTemplateCellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            var wait = new WaitForm("Cargando");
            string currentTestCaseId;
            string objectName;

            try
            {
                currentTestCaseId = MasterTemplate.SelectedRows[0].Cells["Id"].Value.ToString();
                long currentobjId = Int64.Parse(MasterTemplate.SelectedRows[0].Cells["ObjectId"].Value.ToString());
                long currentobjType = Int64.Parse(MasterTemplate.SelectedRows[0].Cells["ObjectTypeId"].Value.ToString());

                objectName = TCBusiness.GetObjectName(currentobjType, currentobjId);

                //el proyecto se toma del combo porque si o si es el que le corresponde ya que fue el que cargo la grilla
                var ucTestCase = new UcTestCase(currentobjType, currentobjId, objectName, _projectId, _rEdit, _rCreate,
                                                _rDelete, _rExecute)
                { Dock = DockStyle.Fill };

                ucTestCase.TestCaseDeleted += UcTestCaseTestCaseDeleted;

                bool exist = false;

                for (int i = 0; i <= (radPageView1.Pages.Count) - 1; i++)
                {
                    if (Validate(radPageView1.Pages[i].Text, ucTestCase.TestCaseName))
                    {
                        radPageView1.SelectedPage = radPageView1.Pages[i];
                        //Obtengo el UcTestCase que se utiliza en esa pagina para que luego seleccione el 
                        // Caso de prueba correspondiente.
                        ucTestCase = (UcTestCase)(radPageView1.Pages[i]).Controls[0];
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    var tabPage = new RadPageViewPage { Text = ucTestCase.TestCaseName };
                    radPageView1.Pages.Add(tabPage);
                    tabPage.Controls.Add(ucTestCase);

                    radPageView1.SelectedPage = tabPage;
                }
                //Selecciono el caso de prueba que voy a utilizar.
                ucTestCase.ShowTc(Int64.Parse(currentTestCaseId));

                WindowState = FormWindowState.Maximized;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                wait.Close();
                wait.Dispose();
            }
        }
    }

}