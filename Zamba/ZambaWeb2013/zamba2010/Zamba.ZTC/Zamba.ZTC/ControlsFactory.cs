using System;
using System.Data;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Zamba.Core;
using Zamba.CoreExt;
using Zamba.Data;
using Zamba.Servers;

namespace Zamba.ZTC
{
    internal class ControlsFactory
    {
        public static EntityConnection EntityConnection;
        public static String EntityConnectionString;
        private static TCEntities _dbContext;
        private readonly long _projectid;
        public Int64 ObjectType { get; set; }
        public Int64 ObjectId { get; set; }
        public RadTreeView RtvTestCase { get; set; }
        public IUser User { get; set; }
        public UCTestCaseDescription ucTestCaseDescription { get; set; }
        public UCTestCaseNewExecution ucTestCaseNewExecution { get; set; }
        public UCTestCaseExecutionHistory ucTestCaseExecutionHistory { get; set; }

        public ControlsFactory(Int64 objType, Int64 objectId, RadTreeView rtvTestCase,
                               UCTestCaseDescription _uctestDescription, UCTestCaseNewExecution _ucTestCaseNewExecution,
                               UCTestCaseExecutionHistory _ucTestCaseExecutionHistory, Int64 ProjectId)
        {
            try
            {
                _projectid = ProjectId;
                ObjectType = objType;
                ObjectId = objectId;
                RtvTestCase = rtvTestCase;
                ucTestCaseExecutionHistory = _ucTestCaseExecutionHistory;
                ucTestCaseNewExecution = _ucTestCaseNewExecution;
                ucTestCaseDescription = _uctestDescription;
                User = Membership.MembershipHelper.CurrentUser;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Error al establecer la conexión", "Zamba Software", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Dispose de la clase ControlFactory
        /// </summary>
        public void Dispose()
        {
            if (RtvTestCase != null)
            {
                RtvTestCase.Dispose();
                RtvTestCase = null;
            }
            if (User != null)
            {
                User.Dispose();
                User = null;
            }
            if (ucTestCaseDescription!= null)
            {
                ucTestCaseDescription.Dispose();
                ucTestCaseDescription = null;
            }
            if (ucTestCaseNewExecution != null)
            {
                ucTestCaseNewExecution.Dispose();
                ucTestCaseNewExecution = null;
            }
            if (ucTestCaseExecutionHistory != null)
            {
                ucTestCaseExecutionHistory.Dispose();
                ucTestCaseExecutionHistory = null;
            }
        }

        public static TCEntities dbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    var dbTools = new DBToolsExt();
                    if (dbTools.UseWindowsAuthentication)
                    {
                        _dbContext = new TCEntities(EntityConnectionString);
                    }
                    else
                    {
                        _dbContext = new TCEntities(EntityConnectionString, dbTools.DataBaseSchema);
                    }
                    dbTools = null;
                }
                   
                return _dbContext;
            }
        }

        public static void InstanceEntityConnection()
        {
            //// Start out by creating the SQL Server connection string
            var sqlBuilder = new SqlConnectionStringBuilder();
            
            //Sirve para instanciar el resto de las variables de server.
            DBTYPES serverType = Server.ServerType;
            sqlBuilder.DataSource = Server.DBServer;
            sqlBuilder.InitialCatalog = Server.DB;
            sqlBuilder.PersistSecurityInfo = true;

            var dbTools = new DBToolsExt();
            if (dbTools.UseWindowsAuthentication)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio de sesión mediante las credenciales de Windows");
                sqlBuilder.IntegratedSecurity = true;
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Inicio de sesión mediante Zamba Software");
                sqlBuilder.IntegratedSecurity = false;
                sqlBuilder.Password = Server.DBPassword;
                sqlBuilder.UserID = Server.DBUser;
            }
            dbTools = null;

            var entityBuilder = new EntityConnectionStringBuilder();
            //String CoreExtPath = @"res://" + Application.StartupPath + @"/Zamba.CoreExt.dll/ModelTC.";

            entityBuilder.Provider = "System.Data.SqlClient";
            entityBuilder.ProviderConnectionString = sqlBuilder.ToString();
            //entityBuilder.Metadata = CoreExtPath + "csdl|" + CoreExtPath + "ssdl|" + CoreExtPath + "msl";
            //           entityBuilder.Metadata = @"res://Zamba.CoreExt.dll/ModelTC.csdl|res://Zamba.CoreExt.dll/ModelTC.ssdl|res://Zamba.CoreExt.dll/ModelTC.msl";
            entityBuilder.Metadata = @"res://*/ModelTC.csdl|res://*/ModelTC.ssdl|res://*/ModelTC.msl";

            EntityConnection = new EntityConnection(entityBuilder.ToString());
            EntityConnectionString = entityBuilder.ConnectionString;
        }

        /// <summary>
        /// Carga el arbol de los casos de prueba
        /// </summary>
        public void LoadTree()
        {
            try
            {
                RtvTestCase.DataSource = null;
                RtvTestCase.DisplayMember = "NodeName";
                RtvTestCase.ValueMember = "TestCaseId";
                RtvTestCase.ParentMember = "ParentNode";
                RtvTestCase.ChildMember = "TestCaseId";
                RtvTestCase.DataSource = ZTCData.GetCases(ObjectType, ObjectId);

                RtvTestCase.ExpandAll();

                CheckNodesToCollapse(RtvTestCase.Nodes);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                MessageBox.Show("Ha ocurrido un error al cargar los casos de prueba", "Zamba Software",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CheckNodesToCollapse(RadTreeNodeCollection nodes)
        {
            foreach (RadTreeNode n in nodes)
            {
                try
                {
                    object item = n.DataBoundItem;
                    if (((DataRowView) item)[2].ToString() != "1" && ((DataRowView) item)[2].ToString() != "0")
                    {
                        n.Collapse();
                    }
                    CheckNodesToCollapse(n.Nodes);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            var form = new Form();
            var label = new Label();
            var textBox = new TextBox();
            var buttonOk = new Button();
            var buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancelar";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] {label, textBox, buttonOk, buttonCancel});
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public Int64 SelectedTestCaseId()
        {
            return Int64.Parse(RtvTestCase.SelectedNode.Value.ToString());
        }

        #region Category

        public void AddCategory()
        {
            string categoryName = string.Empty;
            if (InputBox("Agregar categoría", "Ingrese el nombre de la nueva categoría", ref categoryName) ==
                DialogResult.OK)
                if (categoryName.Trim() == "")
                {
                    MessageBox.Show("Complete el título del caso de uso antes de guardar", "Atención",
                                    MessageBoxButtons.OK);
                    AddCategory();
                }
                else
                {
                    {
                        try
                        {
                            //Int64 parentId = (long)this.RtvTestCase.SelectedNode.Value;
                            //ZTCData.AddCategory(this.ObjectType, this.ObjectId, CoreData.GetNewID(IdTypes.TestCaseNodeId), categoryName, String.Empty, parentId, TestCaseNodeTypes.Category, this.User.ID);
                            ZTCData.AddCategory(ObjectType, ObjectId, 1, categoryName, String.Empty, 0,
                                                TestCaseNodeTypes.Category, User.ID);
                            LoadTree();

                            UserBusiness.Rights.SaveAction(ObjectId, ObjectTypes.TestCaseCategory, RightsType.Create,
                                                           "Creación de la categoría: " +
                                                           categoryName);
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            MessageBox.Show("Ha ocurrido un error al agregar la categoría", "Zamba Software",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        public void ChangeCategoryName()
        {
            RadTreeNode rtNode = RtvTestCase.SelectedNode;
            if (rtNode != null)
            {
                //Se abre el formulario de cambio de Nombre de categoría con el nombre actual en el textbox.
                string categoryName = RtvTestCase.SelectedNode.Text;
                if (InputBox("Cambiar nombre", "Ingrese el nombre de la categoría", ref categoryName) == DialogResult.OK)
                {
                    try
                    {
                        ZTCData.ChangeCategoryName(Int64.Parse(rtNode.Value.ToString()), categoryName);
                        //rtNode.Text = categoryName;
                        //esto tira error por estar con datasource

                        //Se genera una entrada en USER_HST con el cambio de nombre de una Categoría
                        UserBusiness.Rights.SaveAction(ObjectId, ObjectTypes.TestCaseCategory, RightsType.ChangeName,
                                                       "Se modificó el nombre de la categoría de: '" +
                                                       rtNode.Name + "' a: '" + categoryName + "'");
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        MessageBox.Show("Ha ocurrido un error al agregar la categoría", "Zamba Software",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    LoadTree();
                }
            }
        }

        public void DeleteCategory()
        {
            RadTreeNode rtNode = RtvTestCase.SelectedNode;
            if (rtNode != null)
            {
                bool delete = false;

                if (rtNode.Nodes.Count > 0)

                {
                    MessageBox.Show(
                        "No se puede eliminar ya que existen " + rtNode.Nodes.Count.ToString() +
                        " casos de prueba asignados a la categoría", "Atención", MessageBoxButtons.OK);
                }
                else
                {
                    if (
                        MessageBox.Show("¿Realmente desea eliminar la categoría ?", "Confirmar acción",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        delete = true;
                }

                if (delete)
                    try
                    {
                        string categoryName = rtNode.Name;
                        DeleteCategory(rtNode);
                        LoadTree();

                        UserBusiness.Rights.SaveAction(ObjectId, ObjectTypes.TestCaseCategory, RightsType.Delete,
                                                       "Eliminación de la categoría: " +
                                                       categoryName);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                        MessageBox.Show("Ha ocurrido un error al quitar la categoría", "Zamba Software",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void DeleteCategory(RadTreeNode node)
        {
            if (node.Nodes.Count > 0)
                for (int i = 0; i < node.Nodes.Count; i++)
                    DeleteCategory(node.Nodes[i]);

            ZTCData.DeleteTestCase(Int64.Parse(node.Value.ToString()));
        }

        #endregion

        #region TestCase

        public void AddTestCase()
        {
            string TestCaseName = string.Empty;
            try
            {
                RadTreeNode rtNode = RtvTestCase.SelectedNode;
                if (rtNode != null)
                {
                    ucTestCaseDescription.AddTestCase(Int64.Parse(rtNode.Value.ToString()), User, (int) ObjectType,
                                                      ObjectId, TestCaseName, _projectid);
                }
                //se agregan sobre una categoria, 
                //no existen casos padre/hijo
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        public void SelectNodeByID(Decimal DCID)
        {
            bool found = false;


            foreach (RadTreeNode n1 in RtvTestCase.Nodes)
            {
                foreach (RadTreeNode n2 in n1.Nodes)
                {
                    if ((decimal) n2.Value == DCID)
                    {
                        RtvTestCase.SelectedNode = n2;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }
        }


        public void PasteTestCase(RadTreeNode nodeToPaste, bool cutNode)
        {
            RadTreeNode categoryNode = RtvTestCase.SelectedNode;
            if (categoryNode != null)
            {
                string nodeDescription = ((DataRowView) nodeToPaste.DataBoundItem).Row.ItemArray[10].ToString();
                long parentNode = Int64.Parse(categoryNode.Value.ToString());

                try
                {
                    Int32 testCaseId;
                    if (cutNode)
                        throw new NotImplementedException("La función cortar todavía no se encuentra implementada");
                    else
                        testCaseId = ZTCData.PasteCopiedTestCase(ObjectType, ObjectId, 0, nodeToPaste.Name,
                                                                 nodeDescription, parentNode, TestCaseNodeTypes.TestCase,
                                                                 User.ID, Int64.Parse(nodeToPaste.Value.ToString()));
                    TCBusiness.AssingTCToProject(testCaseId, _projectid, ObjectType);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    MessageBox.Show("Ha ocurrido un error al pegar el caso de prueba", "Zamba Software",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                LoadTree();
            }
        }

        public Boolean DeleteTestCase()
        {
            RadTreeNode rtNode = RtvTestCase.SelectedNode;
            Int64 tcid = Int64.Parse(rtNode.Value.ToString());
            //lo comentado esta para implementar :
            Boolean hasExecution = CheckIfTCHasExecutions(tcid);
            Boolean hasVersions = CheckIfTCHasVersions(tcid);

            if (rtNode != null &&
                MessageBox.Show("¿Desea eliminar el caso de prueba?", "Confirmar acción", MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    if (hasVersions) FixChildTC(Int64.Parse(rtNode.Value.ToString()));

                    ZTCData.DeleteTestCase(Int64.Parse(rtNode.Value.ToString()));

                    UserBusiness.Rights.SaveAction(ObjectId, ObjectTypes.TestCase, RightsType.Delete,
                                                   "Eliminación del Caso de prueba: " +
                                                   ucTestCaseDescription.CurrentTestCaseName);

                    LoadTree();
                    return true;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    MessageBox.Show("Ha ocurrido un error al quitar la categoría", "Zamba Software",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //private void DeleteExecutions(long p)
        //{

        //}

        private void FixChildTC(long TCId)
        {
            // Se actualizan los nodetype , parentId de las versiones y luego se eliminan.

            ZTC_CT Parent = (from p in dbContext.ZTC_CT where p.TestCaseId == TCId select p).First();
            ZTC_CT Child = (from p in dbContext.ZTC_CT where p.ParentNode == TCId select p).First();
            Child.ParentNode = Parent.ParentNode;
            Child.NodeType = Parent.NodeType;
            dbContext.DeleteObject(Parent);
            dbContext.SaveChanges();
        }


        private bool CheckIfTCHasVersions(Int64 TCID)
        {
            IQueryable<ZTC_CT> tc = from p in dbContext.ZTC_CT where p.ParentNode == TCID select p;

            if (tc.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfTCHasExecutions(Int64 TCID)
        {
            IQueryable<ZTC_EX> tc = from p in dbContext.ZTC_EX where p.TestCaseID == TCID select p;

            if (tc.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadTestCase(Int64 testCaseId)
        {
            if (ucTestCaseDescription.LoadTestCase(Decimal.Parse(testCaseId.ToString()), User))
                ucTestCaseExecutionHistory.LoadExecutionHistoryTestCase(testCaseId);
        }

        #endregion
    }
}