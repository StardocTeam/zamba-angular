using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Zamba.AppBlock;

namespace ZItoDOC_ID
{



    public partial class frmZItoDOCIDProcess : Form
    {

        #region "Delegates"
        delegate void UpdateProgressDelegate();
        delegate void UpdateErrors();
        delegate void UpdateWasFinished();
        delegate void   UpdatedItem(DataRow row);
        #endregion

        #region "Globals"
        Int32  TotalReg=0;
        string error=string.Empty ;
        public static  List<string> updates =new List<string>() ;
        #endregion

        #region "Method"

        public frmZItoDOCIDProcess()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Comienza la actualización de la base de datos poniendo en platter id el userio id de la ZI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBeginProcess_Click(object sender, EventArgs e)
        {
            Thread ExecUpdate = new Thread(new ThreadStart(ExecuteZItoDocI));
            ExecUpdate.Start();
            btnBeginProcess.Enabled = false;
            if (Thread.CurrentThread.ThreadState == ThreadState.Stopped)
                btnSaveError.Visible = true;
            
        }

        /// <summary>
        /// Cancela la actualización.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelProcess_Click(object sender, EventArgs e)
        {

            if (string.Compare(btnCancelProcess.Text.ToLower(), "ver modificaciones") == 0)
            {
                tabControl1.SelectedTab = tbUpdated;
                this.Height = 481;
            }
            else
            {             
                CloseForm();
            }   
        }

        /// <summary>
        /// Cierra el formulario.
        /// </summary>
        private void CloseForm()
        {

            if (MessageBox.Show("¿Desea cerrar la aplicación?", "Zamba ZI TO DOC I", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                try
                {                    
                    ZItoDOC_ID_Bussiness.CloseConection();
                    this.FormClosed -= new FormClosedEventHandler(frmZItoDOCIDProcess_FormClosed);
                    this.Close();
                    
                }
                catch (Exception ex)
                {
                    ZException.Log(ex, false);
                    ZItoDOC_ID_Bussiness.CloseConection();
                }        

            }
          
        }

        /// <summary>
        /// Método que actualiza la barra de progreso.
        /// </summary>
        private void UpdateProgress()
        {
        
            progressBar1.Maximum = TotalReg;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;

            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 1;
                lblProgreso.Text = "Procesados: " + progressBar1.Value;
            }
            
            

        }

        /// <summary>
        /// Método que agrega los errores que se van produciendo en el list box.
        /// </summary>
        private void AddErrorsToListBox()
        {
            lstErrors.Items.Add(error);
        }

        private void UpdateFinished()
        {
            if (progressBar1.Value >= progressBar1.Maximum)
            {                
                btnSaveError.Visible = true;
                
                if (lstErrors.Items.Count > 0)
                {
                    MessageBox.Show("Se han producido errores para visualizarlos haga clic en \"Errores >>\"", "Zamba ZI to DOC I", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    MessageBox.Show("Proceso terminado exitosamente", "Zamba ZI to DOC I", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }         
            }

            btnCancelProcess.Text = "VER MODIFICACIONES";
            lstUpdatedItems.Items.Clear();
            lstUpdatedItems.DataSource = updates;
        }

        private void UpdatedItems(DataRow row)
        {
           
           updates.Add("Se actualizo la columna PLATTER_ID de doc_i"+row["dtid"] + 
                         " con el User id: " + row["userid"] + " para el doc_id: " + row["docid"] );
            
        }
        /// <summary>
        /// Ejecuta la actualización en la base de datos.
        /// </summary>
        private void ExecuteZItoDocI()
        {
            DataSet dsZI =new DataSet();
            Int64 ConCount = 0;
            try
            {
                dsZI = ZItoDOC_ID_Bussiness.GetCompleteZI();
                TotalReg = dsZI.Tables[0].Rows.Count;

                foreach (DataRow row in dsZI.Tables[0].Rows)
                {
                    try
                    {
                        this.Invoke(new UpdateProgressDelegate(UpdateProgress));
                        ZItoDOC_ID_Bussiness.UpdateDocTable(row);
                        this.Invoke(new UpdatedItem(UpdatedItems), row);
                        ConCount += 1;                        
                        //System.Threading.Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message + "en Doc_I" + row["dtid"].ToString() + " Doc Id: " + row["DocId"].ToString();
                        this.Invoke(new UpdateErrors(AddErrorsToListBox));
                    }
                }
                
                this.Invoke(new UpdateWasFinished(UpdateFinished));
                //Thread.CurrentThread.Suspend();

            }
            catch (Exception ex)
            {
                Zamba.Servers.ZConnectionException.Log(ex, ex.Message, false);
            }
            finally
            {
                dsZI = null;
                ZItoDOC_ID_Bussiness.CloseConection();
            }
        }

        
        /// <summary>
        /// Guarda los errores listado en el list box en un archivo de texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveError_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDialog = new SaveFileDialog();
            SaveDialog.Filter = "*.txt|txt";
            try
            {

                if (SaveDialog.ShowDialog() == DialogResult.OK)
                {

                    StreamWriter archivo = new StreamWriter(SaveDialog.FileName.ToString() + ".txt");

                    foreach (string item in lstErrors.Items)
                    {
                        archivo.WriteLine(item);
                    }
                    MessageBox.Show("Se guardo el archivo con exito", "Zamba ZI to DOC I", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

            catch (Exception ex)
            {
                ZException.Log(ex, false);
            }
            finally
            {
                SaveDialog.Dispose();            
            }

            
        }

        /// <summary>
        /// Método que cierra la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmZItoDOCIDProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseForm();
        }

        private void lnkErrors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.Height >= 480)
                this.Height = 232;
            else
                this.Height = 481;
        }

        /// <summary>
        /// Carga de configuraciones de la aplicación al inciar la misma.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmZItoDOCIDProcess_Load(object sender, EventArgs e)
        {
            this.Height = 232;
            ToolTip ControlToolTip = new ToolTip();

            ControlToolTip.SetToolTip(btnSaveError, "Le permite guardar el listado de errores en su PC");
            ControlToolTip.SetToolTip(btnBeginProcess, "Comienza la actualización");
            ControlToolTip.SetToolTip(btnCancelProcess, "Cancela la ejecución en la base de datos y cierra la aplicación");
            ControlToolTip.SetToolTip(lnkErrors, "Muestra el listado de errores");

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
    #endregion
}
