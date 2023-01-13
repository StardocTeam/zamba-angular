partial class frmTest
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTest));
        this.tbDocTypes = new System.Windows.Forms.TextBox();
        this.btDocTypes = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.tbWSDLDocTypes = new System.Windows.Forms.TextBox();
        this.gbWebserviceDoctypes = new System.Windows.Forms.GroupBox();
        this.button1 = new System.Windows.Forms.Button();
        this.label2 = new System.Windows.Forms.Label();
        this.tbImageId = new System.Windows.Forms.TextBox();
        this.btQueryImage = new System.Windows.Forms.Button();
        this.label3 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.tbWSDL = new System.Windows.Forms.TextBox();
        this.btQueryXML = new System.Windows.Forms.Button();
        this.btQuerySearch = new System.Windows.Forms.Button();
        this.tbQuery = new System.Windows.Forms.TextBox();
        this.tbQueryResult = new System.Windows.Forms.TextBox();
        this.lbIndexList = new System.Windows.Forms.Label();
        this.tbIndexes = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.panel1 = new System.Windows.Forms.Panel();
        this.wbrFile = new AxSHDocVw.AxWebBrowser();
        this.gbWebserviceDoctypes.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.wbrFile)).BeginInit();
        this.SuspendLayout();
        // 
        // tbDocTypes
        // 
        this.tbDocTypes.Location = new System.Drawing.Point(26, 76);
        this.tbDocTypes.Multiline = true;
        this.tbDocTypes.Name = "tbDocTypes";
        this.tbDocTypes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.tbDocTypes.Size = new System.Drawing.Size(745, 97);
        this.tbDocTypes.TabIndex = 13;
        // 
        // btDocTypes
        // 
        this.btDocTypes.Location = new System.Drawing.Point(786, 37);
        this.btDocTypes.Name = "btDocTypes";
        this.btDocTypes.Size = new System.Drawing.Size(81, 35);
        this.btDocTypes.TabIndex = 14;
        this.btDocTypes.Text = "Tipos de Documento";
        this.btDocTypes.UseVisualStyleBackColor = true;
        this.btDocTypes.Click += new System.EventHandler(this.btDocTypes_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(6, 34);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(39, 13);
        this.label4.TabIndex = 15;
        this.label4.Text = "WSDL";
        // 
        // tbWSDLDocTypes
        // 
        this.tbWSDLDocTypes.Location = new System.Drawing.Point(51, 34);
        this.tbWSDLDocTypes.Name = "tbWSDLDocTypes";
        this.tbWSDLDocTypes.Size = new System.Drawing.Size(720, 20);
        this.tbWSDLDocTypes.TabIndex = 16;
        // 
        // gbWebserviceDoctypes
        // 
        this.gbWebserviceDoctypes.Controls.Add(this.panel1);
        this.gbWebserviceDoctypes.Controls.Add(this.button1);
        this.gbWebserviceDoctypes.Controls.Add(this.label2);
        this.gbWebserviceDoctypes.Controls.Add(this.tbImageId);
        this.gbWebserviceDoctypes.Controls.Add(this.btQueryImage);
        this.gbWebserviceDoctypes.Controls.Add(this.label3);
        this.gbWebserviceDoctypes.Controls.Add(this.label1);
        this.gbWebserviceDoctypes.Controls.Add(this.tbWSDL);
        this.gbWebserviceDoctypes.Controls.Add(this.btQueryXML);
        this.gbWebserviceDoctypes.Controls.Add(this.btQuerySearch);
        this.gbWebserviceDoctypes.Controls.Add(this.tbQuery);
        this.gbWebserviceDoctypes.Controls.Add(this.tbQueryResult);
        this.gbWebserviceDoctypes.Controls.Add(this.lbIndexList);
        this.gbWebserviceDoctypes.Controls.Add(this.tbIndexes);
        this.gbWebserviceDoctypes.Location = new System.Drawing.Point(12, 12);
        this.gbWebserviceDoctypes.Name = "gbWebserviceDoctypes";
        this.gbWebserviceDoctypes.Size = new System.Drawing.Size(881, 390);
        this.gbWebserviceDoctypes.TabIndex = 19;
        this.gbWebserviceDoctypes.TabStop = false;
        this.gbWebserviceDoctypes.Text = "Test WebService ServiceIndex";
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(786, 278);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(81, 23);
        this.button1.TabIndex = 25;
        this.button1.Text = "Close Image";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(571, 245);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(74, 13);
        this.label2.TabIndex = 24;
        this.label2.Text = "Id Documento";
        // 
        // tbImageId
        // 
        this.tbImageId.Location = new System.Drawing.Point(651, 242);
        this.tbImageId.Name = "tbImageId";
        this.tbImageId.Size = new System.Drawing.Size(120, 20);
        this.tbImageId.TabIndex = 23;
        // 
        // btQueryImage
        // 
        this.btQueryImage.Location = new System.Drawing.Point(786, 239);
        this.btQueryImage.Name = "btQueryImage";
        this.btQueryImage.Size = new System.Drawing.Size(81, 23);
        this.btQueryImage.TabIndex = 22;
        this.btQueryImage.Text = "Image";
        this.btQueryImage.UseVisualStyleBackColor = true;
        this.btQueryImage.Click += new System.EventHandler(this.btQueryImage_Click);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(6, 121);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(35, 13);
        this.label3.TabIndex = 21;
        this.label3.Text = "Query";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 20);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(39, 13);
        this.label1.TabIndex = 20;
        this.label1.Text = "WSDL";
        // 
        // tbWSDL
        // 
        this.tbWSDL.Location = new System.Drawing.Point(51, 17);
        this.tbWSDL.Name = "tbWSDL";
        this.tbWSDL.Size = new System.Drawing.Size(720, 20);
        this.tbWSDL.TabIndex = 19;
        // 
        // btQueryXML
        // 
        this.btQueryXML.Location = new System.Drawing.Point(786, 68);
        this.btQueryXML.Name = "btQueryXML";
        this.btQueryXML.Size = new System.Drawing.Size(81, 23);
        this.btQueryXML.TabIndex = 18;
        this.btQueryXML.Text = "Indices";
        this.btQueryXML.UseVisualStyleBackColor = true;
        this.btQueryXML.Click += new System.EventHandler(this.btQueryXML_Click);
        // 
        // btQuerySearch
        // 
        this.btQuerySearch.Location = new System.Drawing.Point(786, 140);
        this.btQuerySearch.Name = "btQuerySearch";
        this.btQuerySearch.Size = new System.Drawing.Size(81, 23);
        this.btQuerySearch.TabIndex = 17;
        this.btQuerySearch.Text = "Query";
        this.btQuerySearch.UseVisualStyleBackColor = true;
        this.btQuerySearch.Click += new System.EventHandler(this.btQuerySearch_Click);
        // 
        // tbQuery
        // 
        this.tbQuery.Location = new System.Drawing.Point(26, 140);
        this.tbQuery.Name = "tbQuery";
        this.tbQuery.Size = new System.Drawing.Size(745, 20);
        this.tbQuery.TabIndex = 16;
        // 
        // tbQueryResult
        // 
        this.tbQueryResult.Location = new System.Drawing.Point(26, 176);
        this.tbQueryResult.Multiline = true;
        this.tbQueryResult.Name = "tbQueryResult";
        this.tbQueryResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.tbQueryResult.Size = new System.Drawing.Size(745, 57);
        this.tbQueryResult.TabIndex = 15;
        // 
        // lbIndexList
        // 
        this.lbIndexList.AutoSize = true;
        this.lbIndexList.Location = new System.Drawing.Point(6, 52);
        this.lbIndexList.Name = "lbIndexList";
        this.lbIndexList.Size = new System.Drawing.Size(93, 13);
        this.lbIndexList.TabIndex = 14;
        this.lbIndexList.Text = "Listado de Indices";
        // 
        // tbIndexes
        // 
        this.tbIndexes.Location = new System.Drawing.Point(26, 68);
        this.tbIndexes.Multiline = true;
        this.tbIndexes.Name = "tbIndexes";
        this.tbIndexes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.tbIndexes.Size = new System.Drawing.Size(745, 50);
        this.tbIndexes.TabIndex = 13;
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.tbDocTypes);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.btDocTypes);
        this.groupBox1.Controls.Add(this.tbWSDLDocTypes);
        this.groupBox1.Location = new System.Drawing.Point(12, 408);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(881, 198);
        this.groupBox1.TabIndex = 20;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Test WebService DocTypes";
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.wbrFile);
        this.panel1.Location = new System.Drawing.Point(26, 278);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(745, 85);
        this.panel1.TabIndex = 26;
        // 
        // wbrFile
        // 
        this.wbrFile.Dock = System.Windows.Forms.DockStyle.Fill;
        this.wbrFile.Enabled = true;
        this.wbrFile.Location = new System.Drawing.Point(0, 0);
        this.wbrFile.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wbrFile.OcxState")));
        this.wbrFile.Size = new System.Drawing.Size(745, 85);
        this.wbrFile.TabIndex = 19;
        // 
        // frmTest
        // 
        this.ClientSize = new System.Drawing.Size(906, 614);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.gbWebserviceDoctypes);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Name = "frmTest";
        this.Text = "Test de Aplicacion Zamba.WebServices";
        this.Load += new System.EventHandler(this.frmTest_Load_1);
        this.gbWebserviceDoctypes.ResumeLayout(false);
        this.gbWebserviceDoctypes.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.wbrFile)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox tbDocTypes;
    private System.Windows.Forms.Button btDocTypes;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox tbWSDLDocTypes;
    private System.Windows.Forms.GroupBox gbWebserviceDoctypes;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbImageId;
    private System.Windows.Forms.Button btQueryImage;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbWSDL;
    private System.Windows.Forms.Button btQueryXML;
    private System.Windows.Forms.Button btQuerySearch;
    private System.Windows.Forms.TextBox tbQuery;
    private System.Windows.Forms.TextBox tbQueryResult;
    private System.Windows.Forms.Label lbIndexList;
    private System.Windows.Forms.TextBox tbIndexes;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Panel panel1;
    private AxSHDocVw.AxWebBrowser wbrFile;
}

