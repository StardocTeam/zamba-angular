using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.IO;
using Zamba.AdminControls;
using Zamba.AppBlock;
using Zamba.Controls;
using Zamba.Core;
using Zamba.Viewers;
using Zamba.Tools;
using Zamba.Indexs;

namespace Zamba.CoverPage
{
    public partial class MainForm : Form
    {
        ExternalTimer timer;
        private UCTemplatesNew _ucTemplate;
        private UCIndexIndexerViewer _indexviewer;
        private Int32 _barcodeId;
        private Int32 _oldId;
        private NewResult _res;
        private IResult _loadedResult;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                ExternalLogin login = new ExternalLogin();
                if (!login.validate(string.Empty, string.Empty))
                    Application.Exit();
                else
                {
                    timer = new ExternalTimer();

                    timer.connectionTimeOut += new ExternalTimer.connectionTimeOutEventHandler(relogin);

                    timer.InitializeConnectionTimer();
                }
                this._ucTemplate = new UCTemplatesNew();
                this._ucTemplate.Dock = DockStyle.Fill;
                this._ucTemplate.lnkclicked += new UCTemplatesNew.lnkclickedEventHandler(TemplateSelected);
                this.panel2.Controls.Add(this._ucTemplate);
                if (bool.Parse(UserPreferences.getValue("InsertNewWord", Sections.UserPreferences, true)))
                {
                    LinkLabel Lnk1 = new LinkLabel();
                    Lnk1.BackColor = Color.Transparent;
                    Lnk1.ImageList = this.imageList1;
                    Lnk1.ImageIndex = 0;
                    Lnk1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    Lnk1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    Lnk1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
                    Lnk1.Text = "    Nuevo documento de Word";
                    Font _Font = new Font("Verdana", 10);
                    Lnk1.Font = _Font;
                    Lnk1.SetBounds(10, (this.panel1.Controls.Count * 25) + 2, 20 * 10 + 30, 25);
                    this.panel1.Controls.Add(Lnk1);
                    Lnk1.Click += new EventHandler(Lnk1_Click);
                }  
                this.lstDocTypes.DisplayMember = "name";
                this.lstDocTypes.ValueMember = "id";
                this.lstDocTypes.DataSource = DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserBusiness.Rights.CurrentUser().ID, Zamba.Core.RightsType.Create);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        void Lnk1_Click(object sender, EventArgs e)
        {
            try
            {
                LinkLabel lnk = (LinkLabel)sender;
                FileInfo file = new FileInfo(Application.StartupPath + @"\New.doc");
                this.textBox1.Text = file.Name;
                this.textBox1.Tag = file.FullName;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Método utilizado para limpiar la instancia de usuario actual y mostrar un nuevo formulario de login
        /// </summary>
        ///     ''' <history>
        ///     [Gaston]    13/05/2008  Created (basado del ReLogin del cliente)
        ///     [Marcelo]    07/07/2008  Modified (se quito el borrado del core)
        /// </history>
        private void relogin()
        {
            try
            {
                Zamba.Core.Users.User.CurrentUser = null;

                dvalidatetimeout d = new dvalidatetimeout(validatetimeout);
                this.Invoke(d);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        delegate void dvalidatetimeout();

        private void validatetimeout()
        {
            ExternalLogin login;
            try
            {
                login = new ExternalLogin();
                if (login.validateTimeOut() == false)
                    Application.Exit();
            }
            finally
            {
                login = null;
            }

        }

        private void TemplateSelected(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                this.textBox1.Text = file.Name;
                this.textBox1.Tag = file.FullName;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void lstDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _res = new NewResult();
                if (lstDocTypes.SelectedIndex > -1)
                {
                _res.Parent = DocTypesBusiness.GetDocType(Int64.Parse((lstDocTypes.SelectedValue.ToString())),true);
                _res.DocumentalId = 0;
                _res.Indexs = ZCore.FilterIndex(Int32.Parse(_res.Parent.ID.ToString()),false);

                if (this._loadedResult != null)
                    {
                    foreach (Index I in this._loadedResult.Indexs)
                    {
                        foreach (Index I1 in _res.Indexs)
                        {
                            if (I.ID == I1.ID)
                            {
                                I1.Data = I.Data;
                                I1.DataTemp = I.DataTemp;
                                I1.dataDescription = I.dataDescription;
                                I1.dataDescriptionTemp = I.dataDescriptionTemp;
                                break;
                            }
                        }
                    }
                    }
                LoadIndexViewer(_res);
                this._loadedResult = _res;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        private void LoadIndexViewer(NewResult newResult)
        {
            try
            {
                if (_indexviewer == null)
                {
                    _indexviewer = new UCIndexIndexerViewer(false);
                    _indexviewer.Dock = DockStyle.Fill;
                    this.splitContainer2.Panel2.Controls.Add(this._indexviewer);
                    _indexviewer.BringToFront();
                }
                _indexviewer.IndexChanged -= new UCIndexIndexerViewer.IndexChangedEventHandler(autocomplete);
                _indexviewer.IndexChanged += new UCIndexIndexerViewer.IndexChangedEventHandler(autocomplete);

                IResult _var = newResult;
                _indexviewer.ShowDocument(ref _var, false);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        private void autocomplete(ref IResult Result, IIndex _index )
        {
            try
            {
                Viewers.frmGrilla newFrmGrilla = new frmGrilla();
                System.Windows.Forms.Form grd = newFrmGrilla;
                if (AutocompleteBCBussines.ExecuteAutoComplete(ref Result,ref _index,ref grd) != null)
                    LoadIndexViewer(Result);
         
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

          
        private void LoadIndexViewer(IResult Result)
        {
            try
            {
                if (_indexviewer == null)
                {
                    _indexviewer = new UCIndexIndexerViewer(false);
                    _indexviewer.Dock = DockStyle.Fill;
                    this.splitContainer2.Panel2.Controls.Add(this._indexviewer);
                    _indexviewer.BringToFront();
                }

                _indexviewer.IndexChanged -= new UCIndexIndexerViewer.IndexChangedEventHandler(autocomplete);
                _indexviewer.IndexChanged += new UCIndexIndexerViewer.IndexChangedEventHandler(autocomplete);
                _indexviewer.ShowDocument(ref Result, true);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        private void Evento_btnGenerar(bool bImprimir)
        {
            try
            {
                _barcodeId = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.Caratulas);
                this._oldId = this._barcodeId;
                this._res.ID = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.DOCID);
                this._res.FolderId = Zamba.Core.ToolsBussines.GetNewID(Zamba.Core.IdTypes.FOLDERID);
                GetIndexsFromTemp();

                Microsoft.Office.Interop.Word.Application wordapp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document worddoc = new Microsoft.Office.Interop.Word.Document();
                object path = this.textBox1.Tag;
                object vfalse = false;
                object vtrue = true;
                object nada = Type.Missing;
                worddoc = wordapp.Documents.Open(ref path, ref vfalse, ref vtrue, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada, ref nada);
                object wordobj = worddoc;
                Zamba.Office.OfficeInterop.BarcodeInWord(wordobj, this._barcodeId.ToString(), false, "Centro", 20);
                if (Zamba.Core.BarcodesBussines.Insert((NewResult)_res, Int32.Parse(_res.Parent.ID.ToString()), Int32.Parse(UserBusiness.CurrentUser().ID.ToString()), this._barcodeId))
                {
                    UserBusiness.Rights.SaveAction(_res.ID, ObjectTypes.ModuleBarCode, RightsType.Create, "usuario imprimio caratula", (Int32)UserBusiness.CurrentUser().ID);
                }
                else
                {
                    MessageBox.Show("no se pudo insertar el código de barras", "error en inserción", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                wordapp.Visible = true;
                worddoc.Activate();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
        private void GetIndexsFromTemp()
        {
            try
            {
                foreach (Index _index in this._res.Indexs)
                {
                    _index.Data = _index.DataTemp;
                    _index.dataDescription = _index.dataDescriptionTemp;
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Evento_btnGenerar(true);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.stopTimeOut();
                timer = null;
            }
        }
    }
}
