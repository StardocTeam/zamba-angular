using System;
using Stardoc.HtmlEditor.Enumerators;
using System.Windows.Forms;

    /// <summary>
    /// Form used to enter an Html Image attribute
    /// Consists of Href, Text and Image Alignment
    /// </summary>
internal class EnterImageForm 
    : Form
{
    private Button bInsert;
    private Button bCancel;
    private Label labelText;
    private Label labelHref;
    private TextBox hrefText;
    private TextBox hrefLink;
    private Label labelAlign;
    private ComboBox listAlign;

    private System.ComponentModel.Container components = null;

    public EnterImageForm()
    {
        InitializeComponent();

        // define the text for the alignment
        this.listAlign.Items.AddRange(Enum.GetNames(typeof(ImageAlignOption)));

        // ensure default value set for target
        this.listAlign.SelectedIndex = 4;

    } 


    // property for the text to display
    public string ImageText
    {
        get
        {
            return this.hrefText.Text;
        }
        set
        {
            this.hrefText.Text = value;
        }

    } //ImageText

    // property for the href for the image
    public string ImageLink
    {
        get
        {
            return this.hrefLink.Text.Trim();
        }
        set
        {
            this.hrefLink.Text = value.Trim();
        }

    } //ImageLink

    //property for the image align
    public ImageAlignOption ImageAlign
    {
        get
        {
            return (ImageAlignOption)this.listAlign.SelectedIndex;
        }
        set
        {
            this.listAlign.SelectedIndex = (int)value;
        }
    }


    // Clean up any resources being used.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);

    } //Dispose

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EnterImageForm));
        this.bInsert = new Button();
        this.bCancel = new Button();
        this.labelText = new Label();
        this.labelHref = new Label();
        this.hrefText = new TextBox();
        this.hrefLink = new TextBox();
        this.labelAlign = new Label();
        this.listAlign = new ComboBox();
        this.SuspendLayout();
        // 
        // bInsert
        // 
        this.bInsert.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        this.bInsert.DialogResult = DialogResult.OK;
        this.bInsert.Location = new System.Drawing.Point(264, 106);
        this.bInsert.Name = "bInsert";
        this.bInsert.Size = new System.Drawing.Size(80, 23);
        this.bInsert.TabIndex = 4;
        this.bInsert.Text = "Insert Image";
        // 
        // bCancel
        // 
        this.bCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
        this.bCancel.DialogResult = DialogResult.Cancel;
        this.bCancel.Location = new System.Drawing.Point(352, 106);
        this.bCancel.Name = "bCancel";
        this.bCancel.TabIndex = 5;
        this.bCancel.Text = "Cancel";
        // 
        // labelText
        // 
        this.labelText.Location = new System.Drawing.Point(8, 40);
        this.labelText.Name = "labelText";
        this.labelText.Size = new System.Drawing.Size(32, 23);
        this.labelText.TabIndex = 3;
        this.labelText.Text = "Text:";
        // 
        // labelHref
        // 
        this.labelHref.Location = new System.Drawing.Point(8, 8);
        this.labelHref.Name = "labelHref";
        this.labelHref.Size = new System.Drawing.Size(32, 23);
        this.labelHref.TabIndex = 4;
        this.labelHref.Text = "Href:";
        // 
        // hrefText
        // 
        this.hrefText.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
        this.hrefText.Location = new System.Drawing.Point(48, 40);
        this.hrefText.Name = "hrefText";
        this.hrefText.Size = new System.Drawing.Size(376, 20);
        this.hrefText.TabIndex = 2;
        this.hrefText.Text = "";
        // 
        // hrefLink
        // 
        this.hrefLink.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
        this.hrefLink.Location = new System.Drawing.Point(48, 8);
        this.hrefLink.Name = "hrefLink";
        this.hrefLink.Size = new System.Drawing.Size(376, 20);
        this.hrefLink.TabIndex = 1;
        this.hrefLink.Text = "";
        // 
        // labelAlign
        // 
        this.labelAlign.Location = new System.Drawing.Point(8, 80);
        this.labelAlign.Name = "labelAlign";
        this.labelAlign.Size = new System.Drawing.Size(32, 23);
        this.labelAlign.TabIndex = 7;
        this.labelAlign.Text = "Align:";
        // 
        // listAlign
        // 
        this.listAlign.DropDownStyle = ComboBoxStyle.DropDownList;
        this.listAlign.Location = new System.Drawing.Point(48, 80);
        this.listAlign.Name = "listAlign";
        this.listAlign.Size = new System.Drawing.Size(121, 21);
        this.listAlign.TabIndex = 3;
        // 
        // EnterImageForm
        // 
        this.AcceptButton = this.bInsert;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.CancelButton = this.bCancel;
        this.ClientSize = new System.Drawing.Size(432, 136);
        this.Controls.Add(this.listAlign);
        this.Controls.Add(this.labelAlign);
        this.Controls.Add(this.hrefLink);
        this.Controls.Add(this.hrefText);
        this.Controls.Add(this.labelHref);
        this.Controls.Add(this.labelText);
        this.Controls.Add(this.bCancel);
        this.Controls.Add(this.bInsert);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "EnterImageForm";
        this.ShowInTaskbar = false;
        this.SizeGripStyle = SizeGripStyle.Hide;
        this.Text = "Enter Image";
        this.ResumeLayout(false);

    }
    #endregion
}

