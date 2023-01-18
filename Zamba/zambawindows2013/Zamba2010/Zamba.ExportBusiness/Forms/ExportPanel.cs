using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;
//using Zamba.ExportaOutlook.RServices;
//using Zamba.Services.RemoteEntities;
//using Zamba.Services.RemoteInterfaces;
using ExportaOutlook;
using System.Threading;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using ExportaOutlook.Helper;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services.RemoteInterfaces;

public delegate void ColapseLinkClicked(bool show);

namespace OutlookPanel
{
    /// <summary>
    /// Custom docked panel control
    /// </summary>
    /// <remarks>
    /// This panel shows subject and all recipients of the email which is selected.
    /// </remarks>
    public partial class ExportPanel : UserControl
    {
        private Microsoft.Office.Interop.Outlook.Application Application;
        private IEnumerable<IMail> bufferlist;
        public Microsoft.Office.Tools.CustomTaskPane CTP { get; set; }
        public string PanelState { get; set; }
        private const string Maximized = "Maximized";
        private const string Minimized = "Minimized";

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExportPanel(Microsoft.Office.Interop.Outlook.Application application)
        {
            InitializeComponent();
            this.Application = application;
            this.bufferlist = new List<IMail>();

            try
            {
                FillDatagrid();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Control load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DockedControl_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        /// <summary>
        /// Modifica la cantidad de mails a exportar.
        /// </summary>
        /// <param name="count"></param>
        /// <history>
        ///   [Ezequiel] 29/05/2010 - Created
        /// </history>
        public void ChangeToExportText(Int16 count)
        {
            lblToExport.Text = (Int16.Parse(lblToExport.Text) + count).ToString();
            
            //Si no hay nada mas por exportar minimizo.
            if (PanelState == Maximized && count == 0)
            {
                Minimize();
            }
            else if (PanelState != Maximized && count != 0)
            {
                Maximize();
            }
        }

        /// <summary>
        /// Modifica la cantidad de mails exportados.
        /// </summary>
        /// <param name="count"></param>
        /// <history>
        ///   [Ezequiel] 29/05/2010 - Created
        /// </history>
        public void IncreaseExportedCount()
        {
            this.lblExported.Text = (int.Parse(this.lblExported.Text) + 1).ToString();
        }

        public void IncreaseErrorCount()
        {
            this.lblError.Text = (int.Parse(this.lblError.Text) + 1).ToString();
        }

        public void UpdatePendingCount()
        {
            this.lblToExport.Text = dgvMailsToExport.Rows.Count.ToString();
        }

        /// <summary>
        /// Carga la lista de mails a exportar en el datagridview.
        /// </summary>
        /// <param name="maillist"></param>
        /// <history>
        /// [Ezequiel] - 02/06/2010 Created.
        /// </history>
        public void LoadList(IEnumerable<IMail> maillist)
        {
            try
            {
                List<IMail> lista = new List<IMail>(maillist);
                bufferlist = lista;
                FillDatagrid();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

            }
        }

        /// <summary>
        /// Completa la grilla de mails
        /// </summary>
        private void FillDatagrid()
        {
            //Carga de datos
            dgvMailsToExport.DataSource = null;
            dgvMailsToExport.DataSource = bufferlist;

            //Se hace visible solo la columna asunto
            for (int i = 0; i < dgvMailsToExport.Columns.Count; i++)
                dgvMailsToExport.Columns[i].Visible = false;
            dgvMailsToExport.Columns["subject"].Visible = true;
            dgvMailsToExport.Columns["receivedDate"].Visible = true;

            //Visualización de la información importante
            dgvMailsToExport.Columns["subject"].HeaderText = "Asunto";
            dgvMailsToExport.Columns["subject"].DisplayIndex = 0;
            dgvMailsToExport.Columns["receivedDate"].HeaderText = "Fecha";
            dgvMailsToExport.Columns["receivedDate"].DisplayIndex = 1;
        }

        /// <summary>
        /// Evento para mostrar el panel de mails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowBar_Click(object sender, EventArgs e)
        {
            Maximize();
        }

        /// <summary>
        /// Minimiza el panel
        /// </summary>
        public void Minimize()
        {
            CTP.Width = 0;
            this.pnlExport.Visible = false;
            this.pnlTitle.Visible = false;
            this.btnShowBar.Visible = true;
            this.btnShowBar.Width = 66;
            this.PanelState = Minimized;
        }

        /// <summary>
        /// Maximiza el panel
        /// </summary>
        public void Maximize()
        {
            CTP.Visible = true;
            CTP.Width = 228;
            this.pnlExport.Visible = true;
            this.pnlTitle.Visible = true;
            this.btnShowBar.Visible = false;
            this.PanelState = Maximized;
        }

        /// <summary>
        /// Dispara un evento para colapsar el panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        ///     [Tomas]     09/06/2010 - Created
        /// </history>
        private void btnHidePanel_Click(object sender, EventArgs e)
        {
            Minimize();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Maximize();
        }
    }
}