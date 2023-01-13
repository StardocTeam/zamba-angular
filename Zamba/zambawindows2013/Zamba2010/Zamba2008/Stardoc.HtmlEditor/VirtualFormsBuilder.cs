using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.Enumerators;
using System.Collections.Generic;

public sealed class VirtualFormsBuilder
    : Form
{
    #region Atributos
    private Button bToolbar;
    private Button bBackground;
    private Button bForeground;
    private Button bEditHTML;
    private Button bViewHtml;
    private Button bStyle;
    private CheckBox readonlyCheck;
    private Button bOverWrite;
    private Button bOpenHtml;
    private Button bSaveHtml;
    private Container components = null;
    private ComboBox listHeadings;
    private Button bHeading;
    private Button bInsertHtml;
    private Button bImage;
    private Button bBasrHref;
    private Button bPaste;
    private Button bFormatted;
    private Button bNormal;
    private Button bScript;
    private Button bMicrosoft;
    private Button bLoadFile;
    private Button bUrl;
    private HtmlEditorControl htmlEditorControl;
    private HtmlControlsList UcControlList;
    private String workingDirectory = string.Empty;
    private ListViewItem _draggedItem = null;
    #endregion

    #region Propiedades
    public Dictionary<Int64, String> DocTypes
    {
        set { UcControlList.DocTypes = value; }
    }
    #endregion

    #region Constructores

    public VirtualFormsBuilder()
    {
        InitializeComponent();
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.bToolbar = new System.Windows.Forms.Button();
        this.bEditHTML = new System.Windows.Forms.Button();
        this.bBackground = new System.Windows.Forms.Button();
        this.bForeground = new System.Windows.Forms.Button();
        this.bViewHtml = new System.Windows.Forms.Button();
        this.bStyle = new System.Windows.Forms.Button();
        this.readonlyCheck = new System.Windows.Forms.CheckBox();
        this.bOverWrite = new System.Windows.Forms.Button();
        this.bOpenHtml = new System.Windows.Forms.Button();
        this.bSaveHtml = new System.Windows.Forms.Button();
        this.listHeadings = new System.Windows.Forms.ComboBox();
        this.bHeading = new System.Windows.Forms.Button();
        this.bInsertHtml = new System.Windows.Forms.Button();
        this.bImage = new System.Windows.Forms.Button();
        this.bBasrHref = new System.Windows.Forms.Button();
        this.bPaste = new System.Windows.Forms.Button();
        this.bFormatted = new System.Windows.Forms.Button();
        this.bNormal = new System.Windows.Forms.Button();
        this.bScript = new System.Windows.Forms.Button();
        this.bMicrosoft = new System.Windows.Forms.Button();
        this.bLoadFile = new System.Windows.Forms.Button();
        this.bUrl = new System.Windows.Forms.Button();
        this.UcControlList = new Stardoc.HtmlEditor.HtmlControlsList();
        this.htmlEditorControl = new Stardoc.HtmlEditor.HtmlEditorControl();
        this.SuspendLayout();
        // 
        // bToolbar
        // 
        this.bToolbar.Location = new System.Drawing.Point(8, 64);
        this.bToolbar.Name = "bToolbar";
        this.bToolbar.Size = new System.Drawing.Size(75, 23);
        this.bToolbar.TabIndex = 2;
        this.bToolbar.Text = "Tool Bar";
        this.bToolbar.Click += new System.EventHandler(this.bToolbar_Click);
        // 
        // bEditHTML
        // 
        this.bEditHTML.Location = new System.Drawing.Point(8, 88);
        this.bEditHTML.Name = "bEditHTML";
        this.bEditHTML.Size = new System.Drawing.Size(75, 23);
        this.bEditHTML.TabIndex = 3;
        this.bEditHTML.Text = "Edit HTML";
        this.bEditHTML.Click += new System.EventHandler(this.bEditHTML_Click);
        // 
        // bBackground
        // 
        this.bBackground.Location = new System.Drawing.Point(8, 144);
        this.bBackground.Name = "bBackground";
        this.bBackground.Size = new System.Drawing.Size(75, 23);
        this.bBackground.TabIndex = 4;
        this.bBackground.Text = "Background";
        this.bBackground.Click += new System.EventHandler(this.bBackground_Click);
        // 
        // bForeground
        // 
        this.bForeground.Location = new System.Drawing.Point(8, 168);
        this.bForeground.Name = "bForeground";
        this.bForeground.Size = new System.Drawing.Size(75, 23);
        this.bForeground.TabIndex = 5;
        this.bForeground.Text = "Foreground";
        this.bForeground.Click += new System.EventHandler(this.bForeground_Click);
        // 
        // bViewHtml
        // 
        this.bViewHtml.Location = new System.Drawing.Point(8, 112);
        this.bViewHtml.Name = "bViewHtml";
        this.bViewHtml.Size = new System.Drawing.Size(75, 23);
        this.bViewHtml.TabIndex = 7;
        this.bViewHtml.Text = "View Html";
        this.bViewHtml.Click += new System.EventHandler(this.bViewHtml_Click);
        // 
        // bStyle
        // 
        this.bStyle.Location = new System.Drawing.Point(8, 8);
        this.bStyle.Name = "bStyle";
        this.bStyle.Size = new System.Drawing.Size(75, 23);
        this.bStyle.TabIndex = 8;
        this.bStyle.Text = "StyleSheet";
        this.bStyle.Click += new System.EventHandler(this.bStyle_Click);
        // 
        // readonlyCheck
        // 
        this.readonlyCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.readonlyCheck.Location = new System.Drawing.Point(573, 534);
        this.readonlyCheck.Name = "readonlyCheck";
        this.readonlyCheck.Size = new System.Drawing.Size(104, 24);
        this.readonlyCheck.TabIndex = 9;
        this.readonlyCheck.Text = "Read Only";
        this.readonlyCheck.CheckedChanged += new System.EventHandler(this.readonlyCheck_CheckedChanged);
        // 
        // bOverWrite
        // 
        this.bOverWrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bOverWrite.Location = new System.Drawing.Point(485, 534);
        this.bOverWrite.Name = "bOverWrite";
        this.bOverWrite.Size = new System.Drawing.Size(75, 23);
        this.bOverWrite.TabIndex = 10;
        this.bOverWrite.Text = "OverWrite";
        this.bOverWrite.Click += new System.EventHandler(this.bOverWrite_Click);
        // 
        // bOpenHtml
        // 
        this.bOpenHtml.Location = new System.Drawing.Point(8, 256);
        this.bOpenHtml.Name = "bOpenHtml";
        this.bOpenHtml.Size = new System.Drawing.Size(75, 23);
        this.bOpenHtml.TabIndex = 12;
        this.bOpenHtml.Text = "Open Html";
        this.bOpenHtml.Click += new System.EventHandler(this.bOpenHtml_Click);
        // 
        // bSaveHtml
        // 
        this.bSaveHtml.Location = new System.Drawing.Point(8, 280);
        this.bSaveHtml.Name = "bSaveHtml";
        this.bSaveHtml.Size = new System.Drawing.Size(75, 23);
        this.bSaveHtml.TabIndex = 13;
        this.bSaveHtml.Text = "Save Html";
        this.bSaveHtml.Click += new System.EventHandler(this.bSaveHtml_Click);
        // 
        // listHeadings
        // 
        this.listHeadings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.listHeadings.Items.AddRange(new object[] {
            "H1",
            "H2",
            "H3",
            "H4",
            "H5"});
        this.listHeadings.Location = new System.Drawing.Point(8, 360);
        this.listHeadings.MaxDropDownItems = 5;
        this.listHeadings.Name = "listHeadings";
        this.listHeadings.Size = new System.Drawing.Size(72, 21);
        this.listHeadings.TabIndex = 14;
        // 
        // bHeading
        // 
        this.bHeading.Location = new System.Drawing.Point(8, 384);
        this.bHeading.Name = "bHeading";
        this.bHeading.Size = new System.Drawing.Size(75, 23);
        this.bHeading.TabIndex = 15;
        this.bHeading.Text = "Set Heading";
        this.bHeading.Click += new System.EventHandler(this.bHeading_Click);
        // 
        // bInsertHtml
        // 
        this.bInsertHtml.Location = new System.Drawing.Point(8, 304);
        this.bInsertHtml.Name = "bInsertHtml";
        this.bInsertHtml.Size = new System.Drawing.Size(75, 23);
        this.bInsertHtml.TabIndex = 16;
        this.bInsertHtml.Text = "Insert Html";
        this.bInsertHtml.Click += new System.EventHandler(this.bInsertHtml_Click);
        // 
        // bImage
        // 
        this.bImage.Location = new System.Drawing.Point(8, 192);
        this.bImage.Name = "bImage";
        this.bImage.Size = new System.Drawing.Size(75, 23);
        this.bImage.TabIndex = 17;
        this.bImage.Text = "Local Image";
        this.bImage.Click += new System.EventHandler(this.bImage_Click);
        // 
        // bBasrHref
        // 
        this.bBasrHref.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bBasrHref.Location = new System.Drawing.Point(405, 534);
        this.bBasrHref.Name = "bBasrHref";
        this.bBasrHref.Size = new System.Drawing.Size(75, 23);
        this.bBasrHref.TabIndex = 18;
        this.bBasrHref.Text = "Word Wrap";
        this.bBasrHref.Click += new System.EventHandler(this.bBasrHref_Click);
        // 
        // bPaste
        // 
        this.bPaste.Location = new System.Drawing.Point(8, 328);
        this.bPaste.Name = "bPaste";
        this.bPaste.Size = new System.Drawing.Size(75, 23);
        this.bPaste.TabIndex = 19;
        this.bPaste.Text = "Insert Text";
        this.bPaste.Click += new System.EventHandler(this.bPaste_Click);
        // 
        // bFormatted
        // 
        this.bFormatted.Location = new System.Drawing.Point(8, 408);
        this.bFormatted.Name = "bFormatted";
        this.bFormatted.Size = new System.Drawing.Size(75, 23);
        this.bFormatted.TabIndex = 20;
        this.bFormatted.Text = "Formatted";
        this.bFormatted.Click += new System.EventHandler(this.bFormatted_Click);
        // 
        // bNormal
        // 
        this.bNormal.Location = new System.Drawing.Point(8, 432);
        this.bNormal.Name = "bNormal";
        this.bNormal.Size = new System.Drawing.Size(75, 23);
        this.bNormal.TabIndex = 21;
        this.bNormal.Text = "Normal";
        this.bNormal.Click += new System.EventHandler(this.bNormal_Click);
        // 
        // bScript
        // 
        this.bScript.Location = new System.Drawing.Point(8, 32);
        this.bScript.Name = "bScript";
        this.bScript.Size = new System.Drawing.Size(75, 23);
        this.bScript.TabIndex = 22;
        this.bScript.Text = "ScriptBlock";
        this.bScript.Click += new System.EventHandler(this.bScript_Click);
        // 
        // bMicrosoft
        // 
        this.bMicrosoft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bMicrosoft.Location = new System.Drawing.Point(245, 534);
        this.bMicrosoft.Name = "bMicrosoft";
        this.bMicrosoft.Size = new System.Drawing.Size(75, 23);
        this.bMicrosoft.TabIndex = 25;
        this.bMicrosoft.Text = "Microsoft";
        this.bMicrosoft.Click += new System.EventHandler(this.bMicrosoft_Click);
        // 
        // bLoadFile
        // 
        this.bLoadFile.Location = new System.Drawing.Point(8, 216);
        this.bLoadFile.Name = "bLoadFile";
        this.bLoadFile.Size = new System.Drawing.Size(75, 23);
        this.bLoadFile.TabIndex = 27;
        this.bLoadFile.Text = "Local File";
        this.bLoadFile.Click += new System.EventHandler(this.bLoadFile_Click);
        // 
        // bUrl
        // 
        this.bUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.bUrl.Location = new System.Drawing.Point(165, 534);
        this.bUrl.Name = "bUrl";
        this.bUrl.Size = new System.Drawing.Size(75, 23);
        this.bUrl.TabIndex = 28;
        this.bUrl.Text = "Enter Href";
        this.bUrl.Click += new System.EventHandler(this.bUrl_Click);
        // 
        // UcControlList
        // 
        this.UcControlList.AllowDrop = true;
        this.UcControlList.Location = new System.Drawing.Point(690, 8);
        this.UcControlList.Name = "UcControlList";
        this.UcControlList.Size = new System.Drawing.Size(314, 435);
        this.UcControlList.TabIndex = 29;
        this.UcControlList.OnSelectedHtmlControl += new Stardoc.HtmlEditor.HtmlControlsList.SelectedHtmlControl(this.UcControlList_OnSelectedHtmlControl);
        this.UcControlList.OnSelectedDocTypeChanged += new HtmlControlsList.SelectedDocTypeChanged(UcControlList_OnSelectedDocTypeChanged);
        // 
        // htmlEditorControl
        // 
        this.htmlEditorControl.AllowDrop = true;
        this.htmlEditorControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.htmlEditorControl.InnerText = null;
        this.htmlEditorControl.Location = new System.Drawing.Point(96, 8);
        this.htmlEditorControl.Name = "htmlEditorControl";
        this.htmlEditorControl.Size = new System.Drawing.Size(588, 520);
        this.htmlEditorControl.TabIndex = 26;
        this.htmlEditorControl.HtmlNavigation += new Stardoc.HtmlEditor.HtmlNavigationEventHandler(this.htmlEditorControl_HtmlNavigation);
        this.htmlEditorControl.InsertedHtmlControl += new Stardoc.HtmlEditor.HtmlEditorControl.OnInsertedHtmlControl(this.htmlEditorControl_InsertedHtmlControl);
        // 
        // EditorTestForm
        // 
        this.AllowDrop = true;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.ClientSize = new System.Drawing.Size(1028, 566);
        this.Controls.Add(this.UcControlList);
        this.Controls.Add(this.bUrl);
        this.Controls.Add(this.bLoadFile);
        this.Controls.Add(this.htmlEditorControl);
        this.Controls.Add(this.bMicrosoft);
        this.Controls.Add(this.bScript);
        this.Controls.Add(this.bNormal);
        this.Controls.Add(this.bFormatted);
        this.Controls.Add(this.bPaste);
        this.Controls.Add(this.bImage);
        this.Controls.Add(this.bInsertHtml);
        this.Controls.Add(this.bHeading);
        this.Controls.Add(this.listHeadings);
        this.Controls.Add(this.bSaveHtml);
        this.Controls.Add(this.bOpenHtml);
        this.Controls.Add(this.readonlyCheck);
        this.Controls.Add(this.bStyle);
        this.Controls.Add(this.bViewHtml);
        this.Controls.Add(this.bForeground);
        this.Controls.Add(this.bBackground);
        this.Controls.Add(this.bEditHTML);
        this.Controls.Add(this.bToolbar);
        this.Controls.Add(this.bBasrHref);
        this.Controls.Add(this.bOverWrite);
        this.Name = "EditorTestForm";
        this.Text = "Html Editor";
        this.Load += new System.EventHandler(this.EditorTestForm_Load);
        this.ResumeLayout(false);

    }

    void UcControlList_OnSelectedDocTypeChanged(long? docTypeId)
    {
        OnSelectedDocTypeChanged(docTypeId);
    }

    private void UcControlList_OnSelectedHtmlControl(IHtmlWritable selectedItem)
    {
        htmlEditorControl.ItemToInsert = selectedItem;
    }
    #endregion
    #endregion

    public void LoadHtmlElements(List<IHtmlElement> elements)
    {
        UcControlList.LoadHtmlElements(elements);
    }

    #region Eventos
    private void bToolbar_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.ToolbarVisible = !this.htmlEditorControl.ToolbarVisible;
        this.htmlEditorControl.Focus();
    }

    private void bBackground_Click(object sender, EventArgs e)
    {
        using (ColorDialog dialog = new ColorDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color = dialog.Color;
                this.htmlEditorControl.BodyBackColor = color;
            }
        }
        this.htmlEditorControl.Focus();
    }

    private void bForeground_Click(object sender, EventArgs e)
    {
        using (ColorDialog dialog = new ColorDialog())
        {

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color color = dialog.Color;
                this.htmlEditorControl.BodyForeColor = color;
            }
        }
        this.htmlEditorControl.Focus();
    }

    private void bEditHTML_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.HtmlContentsEdit();
        this.htmlEditorControl.Focus();
    }

    private void bViewHtml_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.HtmlContentsView();
        this.htmlEditorControl.Focus();
    }

    private void bStyle_Click(object sender, EventArgs e)
    {
        string cssFile = Path.GetFullPath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"default.css");
        if (File.Exists(cssFile))
        {
            this.htmlEditorControl.LinkStyleSheet(cssFile);
            MessageBox.Show(this, cssFile, "Style Sheet Linked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show(this, cssFile, "Style Sheet Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        this.htmlEditorControl.Focus();
    }

    private void bScript_Click(object sender, EventArgs e)
    {
        string scriptFile = Path.GetFullPath(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"default.js");
        if (File.Exists(scriptFile))
        {
            this.htmlEditorControl.LinkScriptSource(scriptFile);
            MessageBox.Show(this, scriptFile, "Script Source Linked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show(this, scriptFile, "Script Source Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        this.htmlEditorControl.Focus();

    }

    private void readonlyCheck_CheckedChanged(object sender, EventArgs e)
    {
        this.htmlEditorControl.ReadOnly = this.readonlyCheck.Checked;
        this.htmlEditorControl.Focus();
    }

    private void bOverWrite_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.ToggleOverWrite();
        this.htmlEditorControl.Focus();
    }

    private void bSaveHtml_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.SaveFilePrompt();
        this.htmlEditorControl.Focus();
    }

    private void bOpenHtml_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.OpenFilePrompt();
        this.htmlEditorControl.Focus();
    }

    private void bHeading_Click(object sender, EventArgs e)
    {
        int headingRef = this.listHeadings.SelectedIndex + 1;
        if (headingRef > 0)
        {
            HtmlHeadingType headingType = (HtmlHeadingType)headingRef;
            this.htmlEditorControl.InsertHeading(headingType);
        }
        this.htmlEditorControl.Focus();
    }

    private void bFormatted_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.InsertFormattedBlock();
    }

    private void bNormal_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.InsertNormalBlock();
    }

    private void bInsertHtml_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.InsertHtmlPrompt();
        this.htmlEditorControl.Focus();
    }

    private void bImage_Click(object sender, EventArgs e)
    {

        // set initial value states
        string fileName = string.Empty;
        string filePath = string.Empty;

        // define the file dialog
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Title = "Select Image";
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = true;
            if (workingDirectory != String.Empty) dialog.InitialDirectory = workingDirectory;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = Path.GetFileName(dialog.FileName);
                filePath = Path.GetFullPath(dialog.FileName);
                workingDirectory = Path.GetDirectoryName(dialog.FileName);

                if (fileName != "")
                {
                    // have a path for a image I can insert
                    this.htmlEditorControl.InsertImage(filePath);
                }
            }
        }
        this.htmlEditorControl.Focus();
    }

    private void bBasrHref_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.AutoWordWrap = !this.htmlEditorControl.AutoWordWrap;
        this.htmlEditorControl.Focus();
    }

    private void bPaste_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.InsertTextPrompt();
        this.htmlEditorControl.Focus();
    }

    private void EditorTestForm_Load(object sender, EventArgs e)
    {
        SetFlatStyleSystem(this);
    }

    private void bMicrosoft_Click(object sender, EventArgs e)
    {
        this.htmlEditorControl.NavigateToUrl(@"http://msdn.microsoft.com");
    }

    private void bUrl_Click(object sender, EventArgs e)
    {
        //string href = Microsoft.VisualBasic.Interaction.InputBox("Enter Href for Navigation:", "Href", string.Empty, -1, -1);
        //if (href != string.Empty) this.htmlEditorControl.LoadFromUrl(href);
    }

    private void htmlEditorControl_HtmlException(object sender, HtmlExceptionEventArgs args)
    {
        // obtain the message and operation
        // concatenate the message with any inner message
        string operation = args.Operation;
        Exception ex = args.ExceptionObject;
        string message = ex.Message;
        if (ex.InnerException != null)
        {
            if (ex.InnerException.Message != null)
            {
                message = string.Format("{0}\n{1}", message, ex.InnerException.Message);
            }
        }
        // define the title for the internal message box
        string title;
        if (operation == null || operation == string.Empty)
        {
            title = "Unknown Error";
        }
        else
        {
            title = operation + " Error";
        }
        // display the error message box
        MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void bLoadFile_Click(object sender, EventArgs e)
    {
        // create an open file dialog
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            // define the dialog structure
            dialog.DefaultExt = "html";
            dialog.Title = "Open FIle";
            dialog.AddExtension = true;
            dialog.Filter = "Html files (*.html,*.htm)|*.html;*htm|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            if (workingDirectory != String.Empty) dialog.InitialDirectory = workingDirectory;
            // show the dialog and see if the users enters OK
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.htmlEditorControl.LoadFromFile(dialog.FileName);
            }
        }
    }

    private void htmlEditorControl_HtmlNavigation(object sender, HtmlNavigationEventArgs e)
    {
        e.Cancel = false;
    }

    private void tbtable_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddTable();
        htmlEditorControl.Focus();
    }

    private void btSelect_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddSelect();
        htmlEditorControl.Focus();
    }

    private void btTextArea_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddTextArea();
        htmlEditorControl.Focus();
    }

    private void btCheckBoxes_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddCheckBox();
        htmlEditorControl.Focus();
    }

    private void btRadioButtons_Click(object sender, EventArgs e)
    {
        htmlEditorControl.AddRadioButton();
        htmlEditorControl.Focus();
    }

    private void btText_Click(object sender, EventArgs e)
    {
        htmlEditorControl.InsertTextPrompt();
        htmlEditorControl.Focus();
    }

    private void btControlList_Click(object sender, EventArgs e)
    {
    }

    private void htmlEditorControl_InsertedHtmlControl()
    {
        UcControlList.DeleteSelectedControl();
    }


    public event SelectedDocTypeChanged OnSelectedDocTypeChanged;
    public delegate void SelectedDocTypeChanged(Int64? docTypeId);

    #endregion

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.DoEvents();
        Application.Run(new VirtualFormsBuilder());
    }

    /// <summary>
    /// Obtains the text resource from an embedded file 
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    private string GetResourceText(string filename)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resource = string.Empty;

        // resources are named using a fully qualified name
        string streamName = this.GetType().Namespace + @"." + filename;
        using (Stream stream = assembly.GetManifestResourceStream(streamName))
        {
            // read the contents of the embedded file
            using (StreamReader reader = new StreamReader(stream))
            {
                resource = reader.ReadToEnd(); ;
            }
        }

        return resource;

    }
    /// <summary>
    /// set the flat style of the dialog based on the user setting
    /// </summary>
    /// <param name="parent"></param>
    private void SetFlatStyleSystem(Control parent)
    {
        // iterate through all controls setting the flat style
        foreach (Control control in parent.Controls)
        {
            // Only these controls have a FlatStyle property
            ButtonBase button = control as ButtonBase;
            GroupBox group = control as GroupBox;
            Label label = control as Label;
            TextBox textBox = control as TextBox;
            if (button != null) button.FlatStyle = FlatStyle.System;
            else if (group != null) group.FlatStyle = FlatStyle.System;
            else if (label != null) label.FlatStyle = FlatStyle.System;

            // Set contained controls FlatStyle, too
            SetFlatStyleSystem(control);
        }

    }

}