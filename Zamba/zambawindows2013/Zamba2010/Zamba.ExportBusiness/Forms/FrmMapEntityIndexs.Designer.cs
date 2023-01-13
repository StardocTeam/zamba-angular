using Zamba.AppBlock;

namespace ExportaOutlook.Forms
{
    partial class FrmMapEntityIndexs
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

                if (eMaps != null)
                {
                    eMaps.Clear();
                    eMaps = null;
                }
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
            this.lblEntity = new ZLabel();
            this.cboEntity = new System.Windows.Forms.ComboBox();
            this.cboSender = new System.Windows.Forms.ComboBox();
            this.label1 = new ZLabel();
            this.cboTo = new System.Windows.Forms.ComboBox();
            this.label2 = new ZLabel();
            this.cboCC = new System.Windows.Forms.ComboBox();
            this.label3 = new ZLabel();
            this.cboBCC = new System.Windows.Forms.ComboBox();
            this.label4 = new ZLabel();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.label5 = new ZLabel();
            this.cboMessage = new System.Windows.Forms.ComboBox();
            this.label6 = new ZLabel();
            this.cboDate = new System.Windows.Forms.ComboBox();
            this.label7 = new ZLabel();
            this.cboWindows = new System.Windows.Forms.ComboBox();
            this.label8 = new ZLabel();
            this.cboCode = new System.Windows.Forms.ComboBox();
            this.label9 = new ZLabel();
            this.btnSave = new ZButton();
            this.btnClose = new ZButton();
            this.cboZamba = new System.Windows.Forms.ComboBox();
            this.label10 = new ZLabel();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.cboExportType = new System.Windows.Forms.ComboBox();
            this.label11 = new ZLabel();
            this.cboAutoincremental = new System.Windows.Forms.ComboBox();
            this.label12 = new ZLabel();
            this.lstMappedIndexs = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.Location = new System.Drawing.Point(12, 15);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(43, 13);
            this.lblEntity.TabIndex = 0;
            this.lblEntity.Text = "Entidad";
            // 
            // cboEntity
            // 
            this.cboEntity.BackColor = System.Drawing.Color.White;
            this.cboEntity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEntity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboEntity.FormattingEnabled = true;
            this.cboEntity.Location = new System.Drawing.Point(109, 12);
            this.cboEntity.Name = "cboEntity";
            this.cboEntity.Size = new System.Drawing.Size(270, 21);
            this.cboEntity.TabIndex = 1;
            // 
            // cboSender
            // 
            this.cboSender.BackColor = System.Drawing.Color.White;
            this.cboSender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboSender.FormattingEnabled = true;
            this.cboSender.Location = new System.Drawing.Point(109, 51);
            this.cboSender.Name = "cboSender";
            this.cboSender.Size = new System.Drawing.Size(270, 21);
            this.cboSender.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enviado por";
            // 
            // cboTo
            // 
            this.cboTo.BackColor = System.Drawing.Color.White;
            this.cboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboTo.FormattingEnabled = true;
            this.cboTo.Location = new System.Drawing.Point(109, 78);
            this.cboTo.Name = "cboTo";
            this.cboTo.Size = new System.Drawing.Size(270, 21);
            this.cboTo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Para";
            // 
            // cboCC
            // 
            this.cboCC.BackColor = System.Drawing.Color.White;
            this.cboCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboCC.FormattingEnabled = true;
            this.cboCC.Location = new System.Drawing.Point(109, 105);
            this.cboCC.Name = "cboCC";
            this.cboCC.Size = new System.Drawing.Size(270, 21);
            this.cboCC.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "CC";
            // 
            // cboBCC
            // 
            this.cboBCC.BackColor = System.Drawing.Color.White;
            this.cboBCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboBCC.FormattingEnabled = true;
            this.cboBCC.Location = new System.Drawing.Point(109, 132);
            this.cboBCC.Name = "cboBCC";
            this.cboBCC.Size = new System.Drawing.Size(270, 21);
            this.cboBCC.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "BCC";
            // 
            // cboSubject
            // 
            this.cboSubject.BackColor = System.Drawing.Color.White;
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(109, 159);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(270, 21);
            this.cboSubject.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Asunto";
            // 
            // cboMessage
            // 
            this.cboMessage.BackColor = System.Drawing.Color.White;
            this.cboMessage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboMessage.FormattingEnabled = true;
            this.cboMessage.Location = new System.Drawing.Point(109, 186);
            this.cboMessage.Name = "cboMessage";
            this.cboMessage.Size = new System.Drawing.Size(270, 21);
            this.cboMessage.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cuerpo del mail";
            // 
            // cboDate
            // 
            this.cboDate.BackColor = System.Drawing.Color.White;
            this.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboDate.FormattingEnabled = true;
            this.cboDate.Location = new System.Drawing.Point(109, 213);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(270, 21);
            this.cboDate.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Fecha";
            // 
            // cboWindows
            // 
            this.cboWindows.BackColor = System.Drawing.Color.White;
            this.cboWindows.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWindows.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboWindows.FormattingEnabled = true;
            this.cboWindows.Location = new System.Drawing.Point(109, 240);
            this.cboWindows.Name = "cboWindows";
            this.cboWindows.Size = new System.Drawing.Size(270, 21);
            this.cboWindows.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Usuario Windows";
            // 
            // cboCode
            // 
            this.cboCode.BackColor = System.Drawing.Color.White;
            this.cboCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboCode.FormattingEnabled = true;
            this.cboCode.Location = new System.Drawing.Point(109, 294);
            this.cboCode.Name = "cboCode";
            this.cboCode.Size = new System.Drawing.Size(270, 21);
            this.cboCode.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 297);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Código mail";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(551, 396);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(657, 396);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 25);
            this.btnClose.TabIndex = 29;
            this.btnClose.Text = "Cancelar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboZamba
            // 
            this.cboZamba.BackColor = System.Drawing.Color.White;
            this.cboZamba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboZamba.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboZamba.FormattingEnabled = true;
            this.cboZamba.Location = new System.Drawing.Point(109, 267);
            this.cboZamba.Name = "cboZamba";
            this.cboZamba.Size = new System.Drawing.Size(270, 21);
            this.cboZamba.TabIndex = 31;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Usuario Zamba";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(769, 445);
            this.shapeContainer1.TabIndex = 32;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.Gainsboro;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 14;
            this.lineShape1.X2 = 514;
            this.lineShape1.Y1 = 41;
            this.lineShape1.Y2 = 41;
            // 
            // cboExportType
            // 
            this.cboExportType.BackColor = System.Drawing.Color.White;
            this.cboExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExportType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboExportType.FormattingEnabled = true;
            this.cboExportType.Location = new System.Drawing.Point(109, 321);
            this.cboExportType.Name = "cboExportType";
            this.cboExportType.Size = new System.Drawing.Size(270, 21);
            this.cboExportType.TabIndex = 36;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 324);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Tipo exportación";
            // 
            // cboAutoincremental
            // 
            this.cboAutoincremental.BackColor = System.Drawing.Color.White;
            this.cboAutoincremental.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAutoincremental.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.cboAutoincremental.FormattingEnabled = true;
            this.cboAutoincremental.Location = new System.Drawing.Point(109, 348);
            this.cboAutoincremental.Name = "cboAutoincremental";
            this.cboAutoincremental.Size = new System.Drawing.Size(270, 21);
            this.cboAutoincremental.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 351);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Autoincremental";
            // 
            // lstMappedIndexs
            // 
            this.lstMappedIndexs.BackColor = System.Drawing.Color.White;
            this.lstMappedIndexs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.lstMappedIndexs.Location = new System.Drawing.Point(431, 51);
            this.lstMappedIndexs.Name = "lstMappedIndexs";
            this.lstMappedIndexs.Size = new System.Drawing.Size(326, 313);
            this.lstMappedIndexs.TabIndex = 37;
            this.lstMappedIndexs.UseCompatibleStateImageBehavior = false;
            // 
            // FrmMapEntityIndexs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 445);
            this.ControlBox = false;
            this.Controls.Add(this.lstMappedIndexs);
            this.Controls.Add(this.cboExportType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboAutoincremental);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboZamba);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboWindows);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboMessage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboSubject);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboBCC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboCC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboEntity);
            this.Controls.Add(this.lblEntity);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "FrmMapEntityIndexs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione los atributos de la entidad que desea enlazar con los campos del mail " +
    "entrante";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMapIndexs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZLabel lblEntity;
        private System.Windows.Forms.ComboBox cboEntity;
        private System.Windows.Forms.ComboBox cboSender;
        private ZLabel label1;
        private System.Windows.Forms.ComboBox cboTo;
        private ZLabel label2;
        private System.Windows.Forms.ComboBox cboCC;
        private ZLabel label3;
        private System.Windows.Forms.ComboBox cboBCC;
        private ZLabel label4;
        private System.Windows.Forms.ComboBox cboSubject;
        private ZLabel label5;
        private System.Windows.Forms.ComboBox cboMessage;
        private ZLabel label6;
        private System.Windows.Forms.ComboBox cboDate;
        private ZLabel label7;
        private System.Windows.Forms.ComboBox cboWindows;
        private ZLabel label8;
        private System.Windows.Forms.ComboBox cboCode;
        private ZLabel label9;
        private ZButton btnSave;
        private ZButton btnClose;
        private System.Windows.Forms.ComboBox cboZamba;
        private ZLabel label10;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.ComboBox cboExportType;
        private ZLabel label11;
        private System.Windows.Forms.ComboBox cboAutoincremental;
        private ZLabel label12;
        private System.Windows.Forms.ListView lstMappedIndexs;
    }
}