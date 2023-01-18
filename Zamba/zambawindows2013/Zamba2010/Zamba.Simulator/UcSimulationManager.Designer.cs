using Zamba.AppBlock;

namespace Zamba.Simulator
{
    partial class UcSimulationManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SimRepeater = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.imgService = new System.Windows.Forms.PictureBox();
            this.lblSimDescription = new ZLabel();
            this.lblSimResult = new ZLabel();
            this.label4 = new ZLabel();
            this.lblSimLastExecution = new ZLabel();
            this.lblSimLastEdition = new ZLabel();
            this.lbl234 = new ZLabel();
            this.label2 = new ZLabel();
            this.lblSimName = new ZLabel();
            this.button13 = new ZButton();
            this.btnService = new ZButton();
            this.button9 = new ZButton();
            this.btnPlayTest = new ZButton();
            this.btnDelete = new ZButton();
            this.btnCopy = new ZButton();
            this.btnEdit = new ZButton();
            this.btnNew = new ZButton();
            this.pnlServiceMgr = new System.Windows.Forms.Panel();
            this.SimRepeater.ItemTemplate.SuspendLayout();
            this.SimRepeater.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgService)).BeginInit();
            this.pnlServiceMgr.SuspendLayout();
            this.SuspendLayout();
            // 
            // SimRepeater
            // 
            this.SimRepeater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // SimRepeater.ItemTemplate
            // 
            this.SimRepeater.ItemTemplate.Controls.Add(this.imgService);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lblSimDescription);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lblSimResult);
            this.SimRepeater.ItemTemplate.Controls.Add(this.label4);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lblSimLastExecution);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lblSimLastEdition);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lbl234);
            this.SimRepeater.ItemTemplate.Controls.Add(this.label2);
            this.SimRepeater.ItemTemplate.Controls.Add(this.lblSimName);
            this.SimRepeater.ItemTemplate.Size = new System.Drawing.Size(840, 65);
            this.SimRepeater.Location = new System.Drawing.Point(0, 55);
            this.SimRepeater.Name = "SimRepeater";
            this.SimRepeater.Size = new System.Drawing.Size(848, 580);
            this.SimRepeater.TabIndex = 13;
            this.SimRepeater.VirtualMode = true;
            this.SimRepeater.ItemValueNeeded += new Microsoft.VisualBasic.PowerPacks.DataRepeaterItemValueEventHandler(this.SimRepeater_ItemValueNeeded);
            // 
            // imgService
            // 
            this.imgService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.imgService.BackgroundImage = global::Zamba.Simulator.Properties.Resources.timer_64;
            this.imgService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgService.Location = new System.Drawing.Point(449, 3);
            this.imgService.Name = "imgService";
            this.imgService.Size = new System.Drawing.Size(20, 20);
            this.imgService.TabIndex = 8;
            this.imgService.TabStop = false;
            this.imgService.Visible = false;
            // 
            // lblSimDescription
            // 
            this.lblSimDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimDescription.Location = new System.Drawing.Point(5, 26);
            this.lblSimDescription.Name = "lblSimDescription";
            this.lblSimDescription.Size = new System.Drawing.Size(464, 33);
            this.lblSimDescription.TabIndex = 7;
            this.lblSimDescription.Text = "[DESCRIPCION]";
            this.lblSimDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSimResult
            // 
            this.lblSimResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimResult.AutoSize = true;
            this.lblSimResult.Location = new System.Drawing.Point(575, 46);
            this.lblSimResult.Name = "lblSimResult";
            this.lblSimResult.Size = new System.Drawing.Size(118, 13);
            this.lblSimResult.TabIndex = 6;
            this.lblSimResult.Text = "[VALOR RESULTADO]";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(477, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Último resultado:";
            // 
            // lblSimLastExecution
            // 
            this.lblSimLastExecution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimLastExecution.AutoSize = true;
            this.lblSimLastExecution.Location = new System.Drawing.Point(575, 26);
            this.lblSimLastExecution.Name = "lblSimLastExecution";
            this.lblSimLastExecution.Size = new System.Drawing.Size(111, 13);
            this.lblSimLastExecution.TabIndex = 4;
            this.lblSimLastExecution.Text = "[FECHA EJECUCION]";
            // 
            // lblSimLastEdition
            // 
            this.lblSimLastEdition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimLastEdition.AutoSize = true;
            this.lblSimLastEdition.Location = new System.Drawing.Point(575, 5);
            this.lblSimLastEdition.Name = "lblSimLastEdition";
            this.lblSimLastEdition.Size = new System.Drawing.Size(95, 13);
            this.lblSimLastEdition.TabIndex = 3;
            this.lblSimLastEdition.Text = "[FECHA EDICION]";
            // 
            // lbl234
            // 
            this.lbl234.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl234.AutoSize = true;
            this.lbl234.Location = new System.Drawing.Point(477, 26);
            this.lbl234.Name = "lbl234";
            this.lbl234.Size = new System.Drawing.Size(88, 13);
            this.lbl234.TabIndex = 2;
            this.lbl234.Text = "Última ejecución:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(477, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Última edición:";
            // 
            // lblSimName
            // 
            this.lblSimName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSimName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSimName.Location = new System.Drawing.Point(4, 1);
            this.lblSimName.Name = "lblSimName";
            this.lblSimName.Size = new System.Drawing.Size(439, 24);
            this.lblSimName.TabIndex = 0;
            this.lblSimName.Text = "[NOMBRE]";
            // 
            // button13
            // 
            this.button13.BackgroundImage = global::Zamba.Simulator.Properties.Resources.statistics_64;
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button13.Enabled = false;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.ForeColor = System.Drawing.Color.Transparent;
            this.button13.Location = new System.Drawing.Point(415, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(46, 46);
            this.button13.TabIndex = 23;
            this.button13.UseVisualStyleBackColor = true;
            // 
            // btnService
            // 
            this.btnService.BackgroundImage = global::Zamba.Simulator.Properties.Resources.timer_64;
            this.btnService.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService.ForeColor = System.Drawing.Color.Transparent;
            this.btnService.Location = new System.Drawing.Point(311, 3);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(46, 46);
            this.btnService.TabIndex = 22;
            this.btnService.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.BackgroundImage = global::Zamba.Simulator.Properties.Resources.todo_list_64;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Enabled = false;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.Color.Transparent;
            this.button9.Location = new System.Drawing.Point(363, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(46, 46);
            this.button9.TabIndex = 21;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // btnPlayTest
            // 
            this.btnPlayTest.BackgroundImage = global::Zamba.Simulator.Properties.Resources.next_64;
            this.btnPlayTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlayTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayTest.ForeColor = System.Drawing.Color.Transparent;
            this.btnPlayTest.Location = new System.Drawing.Point(259, 3);
            this.btnPlayTest.Name = "btnPlayTest";
            this.btnPlayTest.Size = new System.Drawing.Size(46, 46);
            this.btnPlayTest.TabIndex = 19;
            this.btnPlayTest.UseVisualStyleBackColor = true;
            this.btnPlayTest.Click += new System.EventHandler(this.btnPlayTest_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::Zamba.Simulator.Properties.Resources.delete_64;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Location = new System.Drawing.Point(159, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(46, 46);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackgroundImage = global::Zamba.Simulator.Properties.Resources.copy_64;
            this.btnCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopy.Enabled = false;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.ForeColor = System.Drawing.Color.Transparent;
            this.btnCopy.Location = new System.Drawing.Point(55, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(46, 46);
            this.btnCopy.TabIndex = 17;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = global::Zamba.Simulator.Properties.Resources.edit_64;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.Transparent;
            this.btnEdit.Location = new System.Drawing.Point(107, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(46, 46);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = global::Zamba.Simulator.Properties.Resources.add_file_64;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ForeColor = System.Drawing.Color.Transparent;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(46, 46);
            this.btnNew.TabIndex = 15;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pnlServiceMgr
            // 
            this.pnlServiceMgr.Controls.Add(this.btnNew);
            this.pnlServiceMgr.Controls.Add(this.button13);
            this.pnlServiceMgr.Controls.Add(this.SimRepeater);
            this.pnlServiceMgr.Controls.Add(this.btnService);
            this.pnlServiceMgr.Controls.Add(this.btnEdit);
            this.pnlServiceMgr.Controls.Add(this.button9);
            this.pnlServiceMgr.Controls.Add(this.btnCopy);
            this.pnlServiceMgr.Controls.Add(this.btnDelete);
            this.pnlServiceMgr.Controls.Add(this.btnPlayTest);
            this.pnlServiceMgr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlServiceMgr.Location = new System.Drawing.Point(0, 0);
            this.pnlServiceMgr.Name = "pnlServiceMgr";
            this.pnlServiceMgr.Size = new System.Drawing.Size(848, 635);
            this.pnlServiceMgr.TabIndex = 24;
            // 
            // UcSimulationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlServiceMgr);
            this.Name = "UcSimulationManager";
            this.Size = new System.Drawing.Size(848, 635);
            this.SimRepeater.ItemTemplate.ResumeLayout(false);
            this.SimRepeater.ItemTemplate.PerformLayout();
            this.SimRepeater.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgService)).EndInit();
            this.pnlServiceMgr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.DataRepeater SimRepeater;
        private ZButton btnNew;
        private ZButton btnEdit;
        private ZButton btnDelete;
        private ZButton btnCopy;
        private ZButton btnService;
        private ZButton button9;
        private ZButton btnPlayTest;
        private ZButton button13;
        private ZLabel lblSimName;
        private ZLabel lblSimDescription;
        private ZLabel lblSimResult;
        private ZLabel label4;
        private ZLabel lblSimLastExecution;
        private ZLabel lblSimLastEdition;
        private ZLabel lbl234;
        private ZLabel label2;
        private System.Windows.Forms.PictureBox imgService;
        private System.Windows.Forms.Panel pnlServiceMgr;
    }
}