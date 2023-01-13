using Zamba.AppBlock;

namespace Zamba.Simulator
{
    partial class UcSimulationABM
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new ZLabel();
            this.label2 = new ZLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblTitle = new ZLabel();
            this.lblTestCases = new ZLabel();
            this.btnCancel = new ZButton();
            this.btnSave = new ZButton();
            this.TCRepeater = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.btnEditDictionary = new ZButton();
            this.btnRemoveTC = new ZButton();
            this.lblProcessValue = new ZLabel();
            this.lblDictionaryValue = new ZLabel();
            this.lblDictionaryDesc = new ZLabel();
            this.lblProcessDesc = new ZLabel();
            this.lblExplanation = new ZLabel();
            this.TCRepeater.ItemTemplate.SuspendLayout();
            this.TCRepeater.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(11, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(257, 22);
            this.txtName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Descripción";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(11, 99);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(257, 343);
            this.txtDescription.TabIndex = 19;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(7, 452);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 31);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Nueva Simulación";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTestCases
            // 
            this.lblTestCases.AutoSize = true;
            this.lblTestCases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestCases.Location = new System.Drawing.Point(280, 15);
            this.lblTestCases.Name = "lblTestCases";
            this.lblTestCases.Size = new System.Drawing.Size(96, 16);
            this.lblTestCases.TabIndex = 21;
            this.lblTestCases.Text = "Casos de Test";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(627, 448);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Honeydew;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(496, 448);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(125, 35);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // TCRepeater
            // 
            this.TCRepeater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // TCRepeater.ItemTemplate
            // 
            this.TCRepeater.ItemTemplate.Controls.Add(this.btnEditDictionary);
            this.TCRepeater.ItemTemplate.Controls.Add(this.btnRemoveTC);
            this.TCRepeater.ItemTemplate.Controls.Add(this.lblProcessValue);
            this.TCRepeater.ItemTemplate.Controls.Add(this.lblDictionaryValue);
            this.TCRepeater.ItemTemplate.Controls.Add(this.lblDictionaryDesc);
            this.TCRepeater.ItemTemplate.Controls.Add(this.lblProcessDesc);
            this.TCRepeater.ItemTemplate.Size = new System.Drawing.Size(461, 37);
            this.TCRepeater.Location = new System.Drawing.Point(283, 34);
            this.TCRepeater.Name = "TCRepeater";
            this.TCRepeater.Size = new System.Drawing.Size(469, 408);
            this.TCRepeater.TabIndex = 34;
            this.TCRepeater.VirtualMode = true;
            this.TCRepeater.ItemValueNeeded += new Microsoft.VisualBasic.PowerPacks.DataRepeaterItemValueEventHandler(this.TCRepeater_ItemValueNeeded);
            // 
            // btnEditDictionary
            // 
            this.btnEditDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditDictionary.BackgroundImage = global::Zamba.Simulator.Properties.Resources.edit_property_64;
            this.btnEditDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditDictionary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditDictionary.ForeColor = System.Drawing.Color.Transparent;
            this.btnEditDictionary.Location = new System.Drawing.Point(377, 3);
            this.btnEditDictionary.Name = "btnEditDictionary";
            this.btnEditDictionary.Size = new System.Drawing.Size(30, 30);
            this.btnEditDictionary.TabIndex = 9;
            this.btnEditDictionary.UseVisualStyleBackColor = true;
            this.btnEditDictionary.Click += new System.EventHandler(this.btnEditDictionary_Click);
            // 
            // btnRemoveTC
            // 
            this.btnRemoveTC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTC.BackgroundImage = global::Zamba.Simulator.Properties.Resources.delete_sign_64;
            this.btnRemoveTC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoveTC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveTC.ForeColor = System.Drawing.Color.Transparent;
            this.btnRemoveTC.Location = new System.Drawing.Point(413, 2);
            this.btnRemoveTC.Name = "btnRemoveTC";
            this.btnRemoveTC.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveTC.TabIndex = 8;
            this.btnRemoveTC.UseVisualStyleBackColor = true;
            this.btnRemoveTC.Click += new System.EventHandler(this.btnRemoveTC_Click);
            // 
            // lblProcessValue
            // 
            this.lblProcessValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcessValue.Location = new System.Drawing.Point(73, 1);
            this.lblProcessValue.Name = "lblProcessValue";
            this.lblProcessValue.Size = new System.Drawing.Size(298, 13);
            this.lblProcessValue.TabIndex = 3;
            this.lblProcessValue.Text = "[process value]";
            // 
            // lblDictionaryValue
            // 
            this.lblDictionaryValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDictionaryValue.Location = new System.Drawing.Point(73, 19);
            this.lblDictionaryValue.Name = "lblDictionaryValue";
            this.lblDictionaryValue.Size = new System.Drawing.Size(298, 13);
            this.lblDictionaryValue.TabIndex = 2;
            this.lblDictionaryValue.Text = "[dictionary value]";
            // 
            // lblDictionaryDesc
            // 
            this.lblDictionaryDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDictionaryDesc.AutoSize = true;
            this.lblDictionaryDesc.Location = new System.Drawing.Point(3, 20);
            this.lblDictionaryDesc.Name = "lblDictionaryDesc";
            this.lblDictionaryDesc.Size = new System.Drawing.Size(63, 13);
            this.lblDictionaryDesc.TabIndex = 1;
            this.lblDictionaryDesc.Text = "Diccionario:";
            // 
            // lblProcessDesc
            // 
            this.lblProcessDesc.AutoSize = true;
            this.lblProcessDesc.Location = new System.Drawing.Point(4, 1);
            this.lblProcessDesc.Name = "lblProcessDesc";
            this.lblProcessDesc.Size = new System.Drawing.Size(52, 13);
            this.lblProcessDesc.TabIndex = 0;
            this.lblProcessDesc.Text = "Proceso: ";
            // 
            // lblExplanation
            // 
            this.lblExplanation.AutoSize = true;
            this.lblExplanation.ForeColor = System.Drawing.Color.Gray;
            this.lblExplanation.Location = new System.Drawing.Point(378, 17);
            this.lblExplanation.Name = "lblExplanation";
            this.lblExplanation.Size = new System.Drawing.Size(377, 13);
            this.lblExplanation.TabIndex = 35;
            this.lblExplanation.Text = "Para agregar procesos haga doble click sobre las reglas del árbol de Workflow";
            // 
            // UcSimulationABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblExplanation);
            this.Controls.Add(this.TCRepeater);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTestCases);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Name = "UcSimulationABM";
            this.Size = new System.Drawing.Size(764, 495);
            this.Load += new System.EventHandler(this.UcSimulationABM_Load);
            this.TCRepeater.ItemTemplate.ResumeLayout(false);
            this.TCRepeater.ItemTemplate.PerformLayout();
            this.TCRepeater.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private ZLabel label1;
        private ZLabel label2;
        private System.Windows.Forms.TextBox txtDescription;
        private ZLabel lblTitle;
        private ZLabel lblTestCases;
        private ZButton btnCancel;
        private ZButton btnSave;
        private Microsoft.VisualBasic.PowerPacks.DataRepeater TCRepeater;
        private ZLabel lblProcessValue;
        private ZLabel lblDictionaryValue;
        private ZLabel lblDictionaryDesc;
        private ZLabel lblProcessDesc;
        private ZLabel lblExplanation;
        private ZButton btnRemoveTC;
        private ZButton btnEditDictionary;
    }
}