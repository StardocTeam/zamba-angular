using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor
{

    internal class HrefEditor : Form
    {
        #region Atributos
        private Button btInsert;
        private Button btRemove;
        private Button btCancel;
        private Label lbText;
        private Label lbHref;
        private TextBox tbText;
        private TextBox tbLink;
        private Label lbTarget;
        private ComboBox cmbTargets;
        private Container components = null;
        private HtmlFormState _state;

        #endregion

        #region Constantes
        private const String TITLE_STATE_VIEW = "Resumen elemento HREF";
        private const String TITLE_STATE_UPDATE = "Edición elemento HREF";
        private const String TITLE_STATE_INSERT = "Insertar elemento HREF";
        #endregion

        #region Propiedades
        public string HrefText
        {
            get
            {
                return this.tbText.Text;
            }
            set
            {
                this.tbText.Text = value;
            }

        }
        public NavigateActionOption HrefTarget
        {
            get
            {
                return (NavigateActionOption)this.cmbTargets.SelectedIndex;
            }
        }
        public string HrefLink
        {
            get
            {
                return this.tbLink.Text.Trim();
            }
            set
            {
                this.tbLink.Text = value.Trim();
            }
        }
        public HtmlFormState State
        {
            get { return _state; }
            set
            {
                _state = value;
                switch (_state)
                {
                    case HtmlFormState.View:
                        this.Text = TITLE_STATE_VIEW;
                        break;
                    case HtmlFormState.Update:
                        this.Text = TITLE_STATE_UPDATE;
                        break;
                    case HtmlFormState.Insert:
                        this.Text = TITLE_STATE_INSERT;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Constructores
        public HrefEditor()
        {
            InitializeComponent();

            this.cmbTargets.Items.AddRange(Enum.GetNames(typeof(NavigateActionOption)));
            this.cmbTargets.SelectedIndex = 0;
        }
        #endregion

        #region Eventos
        private void btInsert_Click(object sender, EventArgs e)
        {

        }
        private void btRemove_Click(object sender, EventArgs e)
        {

        }
        private void btCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(HrefEditor));
            this.btInsert = new Button();
            this.btRemove = new Button();
            this.btCancel = new Button();
            this.lbText = new Label();
            this.lbHref = new Label();
            this.tbText = new TextBox();
            this.tbLink = new TextBox();
            this.lbTarget = new Label();
            this.cmbTargets = new ComboBox();
            this.SuspendLayout();
            // 
            // btInsert
            // 
            this.btInsert.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btInsert.Location = new Point(184, 106);
            this.btInsert.Size = new Size(75, 23);
            this.btInsert.TabIndex = 0;
            this.btInsert.Text = "Insertar Href";
            this.btInsert.Click += new EventHandler(this.btInsert_Click);
            // 
            // btRemove
            // 
            this.btRemove.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btRemove.Location = new Point(264, 106);
            this.btRemove.Size = new Size(80, 23);
            this.btRemove.TabIndex = 1;
            this.btRemove.Text = "Remover Href";
            this.btRemove.Click += new EventHandler(this.btRemove_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btCancel.Location = new Point(352, 106);
            this.btCancel.Size = new Size(75, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancelar";
            this.btCancel.Click += new EventHandler(this.btCancel_Click);
            // 
            // lbText
            // 
            this.lbText.Location = new Point(8, 16);
            this.lbText.Size = new Size(40, 23);
            this.lbText.TabIndex = 3;
            this.lbText.Text = "Texto:";
            // 
            // lbHref
            // 
            this.lbHref.Location = new Point(8, 48);
            this.lbHref.Size = new Size(40, 23);
            this.lbHref.TabIndex = 4;
            this.lbHref.Text = "Href:";
            // 
            // tbText
            // 
            this.tbText.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.tbText.Location = new Point(56, 16);
            this.tbText.ReadOnly = true;
            this.tbText.Size = new Size(368, 20);
            this.tbText.TabIndex = 5;
            // 
            // tbLink
            // 
            this.tbLink.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.tbLink.Location = new Point(56, 48);
            this.tbLink.Size = new Size(368, 20);
            this.tbLink.TabIndex = 6;
            // 
            // lbTarget
            // 
            this.lbTarget.Location = new Point(8, 80);
            this.lbTarget.Size = new Size(54, 23);
            this.lbTarget.TabIndex = 7;
            this.lbTarget.Text = "Destino:";
            // 
            // cmbTargets
            // 
            this.cmbTargets.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTargets.Location = new Point(56, 77);
            this.cmbTargets.Size = new Size(121, 21);
            this.cmbTargets.TabIndex = 8;
            // 
            // HrefEditor
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.AcceptButton = this.btInsert;
            this.CancelButton = this.btCancel;
            this.ClientSize = new Size(432, 136);
            this.Controls.Add(this.cmbTargets);
            this.Controls.Add(this.lbTarget);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.lbHref);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btRemove);
            this.Controls.Add(this.btInsert);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HrefEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Insertar elemento HREF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}