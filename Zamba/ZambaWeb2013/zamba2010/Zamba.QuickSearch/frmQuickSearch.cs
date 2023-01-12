using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.Core.Search;
using Zamba.Core.Searchs;
using Zamba.Searchs;

namespace Zamba.QuickSearch
{
    public partial class frmQuickSearch : Form, IChromiumQuickSearch
    {
        private ChromiumWebBrowser cwb;

        string strClipCurrent = String.Empty;
        int xPos = 0;
        int yPos = 0;

        public enum SearchModes
        {
            Web = 0,
            Desktop = 1
        }

        private frmQuickSearch()
        {
        }
        public SearchModes SearchMode { get; private set; }
        public IContainer _container { get; private set; }

        public frmQuickSearch(SearchModes searchMode, IContainer container)
        {
            this.SearchMode = searchMode;
            this._container = container;
            InitializeComponent();
            ZQSEvents();
        }

        private void ZQSEvents()
        {
            this.Resize += FrmQuickSearch_Resize;
            this.FormClosing += FrmQuickSearch_FormClosing;
            this.notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
        }

        public void StartListener()
        {
            try
            {
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        /// <summary>
        /// This is the timer tick event handler to
        /// look for new items in the Clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            AddToBoard();
        }

        #region "Board"
        /// <summary>
        /// This method retrieves text/image from the Clipboard
        /// and stores it in the list for later use.
        /// </summary>
        private void AddToBoard()
        {
            // Before retrieving Text from the Clipboard make sure the 
            // current data on Clipboard is for type text.
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                string s = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();

                // strClipCurrent has the last string retrieved from the Clipboard.
                // This checking is to avoid the unnecessary looping of the ListBox control
                // unless a new string has come to the clipboard.
                if (s != strClipCurrent)
                {
                    if (!string.IsNullOrEmpty(strClipCurrent))
                    {
                        if (SearchMode == SearchModes.Web)
                            InitQuickSearch.ExecuteJSFunction(cwb, "doSearch", s);
                        else
                            ExecuteSearch(s);
                    }
                    strClipCurrent = s;
                }
            }
        }

        #endregion

        #region "PopUp"

        public void PopUpQuickSearch()
        {
            this.Show();
            this.Opacity = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xPos = Screen.GetWorkingArea(this).Width;
            yPos = Screen.GetWorkingArea(this).Height;
            this.Location = new Point(xPos - this.Width, yPos + this.Height);
            if (SearchMode == SearchModes.Web) LoadWebSearch();
        }

        #endregion


        /// <summary>
        /// When the notify icon of Clipboard ring in the system tray
        /// is clicked, the timer instance tmr1 is enabled, which 
        /// brings the application form to the visinity with an 
        /// animated effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Opacity = 1;

            tmr1.Enabled = true;
        }

        /// <summary>
        /// When user clicks the Close button,
        /// timer instance tmr2 is enabled which hides the 
        /// form with an animated effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Hide();
            tmr2.Enabled = true;
        }

        /// <summary>
        /// brings the application form to the visinity with an 
        /// animated effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmr1_Tick(object sender, EventArgs e)
        {
            int curPos = this.Location.Y;
            if (curPos > yPos - this.Height)
            {
                this.Location = new Point(xPos - this.Width, curPos - 20);
            }
            else
            {
                tmr1.Stop();
                tmr1.Enabled = false;
            }
        }

        /// <summary>
        /// hides the form with an animated effect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmr2_Tick(object sender, EventArgs e)
        {
            int curPos = this.Location.Y;

            if (curPos < (yPos + 30))
            {
                this.Location = new Point(xPos - this.Width, curPos + 20);
            }
            else
            {
                tmr2.Stop();
                tmr2.Enabled = false;
                //this.Close();
            }
        }
        private void FrmQuickSearch_Resize(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void FrmQuickSearch_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void NotifyIcon_BalloonTipClicked(object sender, System.EventArgs e)
        {
            FadeInForm();
        }

        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FadeInForm();
            }
        }
        private void FadeInForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Opacity = 1;

            tmr1.Enabled = true;
        }



        #region "WebSearch"

        private void LoadWebSearch()
        {
            var browser = new InitQuickSearch(this);
            cwb = browser.Init();
            this.Controls.Add(cwb);
        }



        #region "TaskController"

        public Controls.Controller ControllerTask;
        private void LoadDesktopSearch()
        {
            ControllerTask = new Zamba.Controls.Controller(Membership.MembershipHelper.CurrentUser.ID,null);
            ControllerTask.Dock = DockStyle.Fill;
            this.Controls.Add(ControllerTask);

            ControllerTask.UCTaskGrid.TaskDoubleClick -= ShowTask;
            ControllerTask.UCTaskGrid.TaskDoubleClick += ShowTask;
            ControllerTask.tabResults.ShowTask -= ShowTaskViewer;
            ControllerTask.tabResults.ShowTask += ShowTaskViewer;

        }

        private void ShowTaskViewer(long TaskId, long stepId, long docTypeId)
        {
            throw new NotImplementedException();
        }

        private void ShowTask(ITaskResult Task)
        {
            throw new NotImplementedException();
        }

        internal void ShowController(ISearch Search, string query, string queryCount)
        {
            try
            {
                //Pongo la grilla de resultados por delante.
                ControllerTask.ShowResultsGrid();

                if (ControllerTask.tabResults.UCFusion2 != null)
                {
                    //Limpia la grilla.
                    ControllerTask.tabResults.UCFusion2.FillResults(null, null);
                    ControllerTask.tabResults.UCFusion2.ResetGrid();
                    //Vuelve a la primer pagina.
                    ControllerTask.tabResults.UCFusion2.LastPage = 0;
                    //Obtiene los resultados con la consulta con su conteo
                    DataTable dt = Zamba.Core.Search.ModDocuments.SearchRows(query, queryCount, 0);
                    //Llena la grilla con los resultados obtenidos
                    ControllerTask.tabResults.UCFusion2.FillResults(dt, Search);

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

        internal void ShowController(ISearch Search)
        {
            try
            {
                //Los filtros son por grupo de usuarios o usuarios, aun no son por etapas, entonces son los mismos para todas las etapas
                //se agrega una opcion en el userconfig, que por defecto hara que se mantengan los filtros entre etapas.
                ControllerTask.ShowResultsGrid();

                if (ControllerTask.tabResults.UCFusion2 != null)
                {
                    ControllerTask.tabResults.UCFusion2.FillResults(null, Search);
                    ControllerTask.tabResults.UCFusion2.LastPage = 0;
                    ControllerTask.tabResults.UCFusion2.ShowTaskOfDT();
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


        #endregion




        #endregion

        public event ShowResultEventHandler ShowResult;
        public delegate void ShowResultEventHandler(ref IResult Result);


        #region "QuickSearch"

        private void ToolStripButton1_Click()
        {
            //Top = this.Top;
            //Left = this.Left - Width - 3;
            if (!Visible) Show(); else Hide();
        }

        private void ToolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Visible) Show();

        }

        public void DownloadFile(string file)
        {
            throw new NotImplementedException();
        }


        #endregion
        //    Delegate Sub dShowProperResult(ByRef Result As IResult)
        //Private Sub delegateShowProperResult(ByRef Result As IResult)

        //    SelectTab(TabPages.TabTaskDetails)
        //    AttachResultEventsHandlers()
        //    If Not TypeOf Result Is ITaskResult Then
        //        SelectTab(TabPages.TabTasks)
        //        _TabTareas.UCPanels.ShowProperGrid()
        //    End If

        //    _TabTareas.UCPanels.ShowResult(Result)

        //End Sub
        //Private Sub ShowProperResult(ByRef Result As IResult)
        //    Dim d As New dShowProperResult(AddressOf delegateShowProperResult)
        //    Me.Invoke(d, New Object() { Result})
        //End Sub

        internal void ExecuteSearch(string s)
        {
            try
            {


                Search Search = new Search(null, s, null, true, string.Empty);

                if (Search != null)
                {
                    var FC = new Filters.FiltersComponent();
                    String EntitiesEnabledForQuickSearch = UserPreferences.getValue("EntitiesEnabledForQuickSearch", UPSections.Search, "");

                    if (EntitiesEnabledForQuickSearch != null && EntitiesEnabledForQuickSearch.Length > 0)
                    {
                        List<IEntityEnabledForQuickSearch> EQS = new List<IEntityEnabledForQuickSearch>();
                        Search.SearchType = SearchTypes.QuickSearch;

                        foreach (String E in EntitiesEnabledForQuickSearch.Split(char.Parse("|")))
                        {
                            IEntityEnabledForQuickSearch EQ = new EntityEnabledForQuickSearch();
                            EQ.EntityId = Int64.Parse(E.Split(char.Parse(":"))[0]);
                            DocType Entity = DocTypeBusinessExt.GetDocTypeByID(EQ.EntityId);

                            if (EQ.IndexsIds == null) EQ.IndexsIds = new List<long>();
                            foreach (String I in E.Split(char.Parse(":"))[1].Split(char.Parse(",")))
                            {
                                EQ.IndexsIds.Add(Int64.Parse(I));
                            }
                            Search.AddDocType(Entity);
                            EQS.Add(EQ);
                        }
                        Search.EntitiesEnabledForQuickSearch = EQS;
                        if (Membership.MembershipHelper.CurrentUser != null)
                        {
                            DataTable dt = ModDocuments.DoSearch(Search, Membership.MembershipHelper.CurrentUser.ID, ref FC, 0, 5, false, false, FilterTypes.Document, false, null, true, false, false, false, "");

                            this.showResults(EQS, Search, dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        private void showResults(List<IEntityEnabledForQuickSearch> eQS, Search search, DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    UCThumbsPreview RP = new UCThumbsPreview(dt);
                    RP.Dock = DockStyle.Fill;
                    this.Controls.Add(RP);
                    this.FadeInForm();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

}