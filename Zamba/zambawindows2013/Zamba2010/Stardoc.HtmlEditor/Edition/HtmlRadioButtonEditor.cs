using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor
{
    internal partial class HtmlRadioButtonEditor
        : Form
    {
        #region Atributos
        private HtmlFormState _state = HtmlFormState.Insert;
        #endregion

        #region Constantes
        /// <summary>
        /// Valor que tiene un elemento input para que sea un radio button
        /// </summary>
        private const String HTML_RADIO_TYPE = "radio";
        /// <summary>
        /// Titulo del Form cuando esta en estado View
        /// </summary>
        private const String TITLE_STATE_VIEW = "Resumen del elemento Radio Button";
        /// <summary>
        /// Titulo del Form cuando esta en estado Update
        /// </summary>
        private const String TITLE_STATE_UPDATE = "Edición del elemento Radio Button";
        /// <summary>
        /// Titulo del Form cuando esta en estado Insert
        /// </summary>
        private const String TITLE_STATE_INSERT = "Insertar elemento Radio Button";
        #endregion

        #region Constructores
        public HtmlRadioButtonEditor()
        {
            InitializeComponent();
        }
        public HtmlRadioButtonEditor(RadioButtonItem radio, HtmlFormState state)
            : this()
        {
            Radio = radio;
            _state = state;
        }
        #endregion

        #region Propiedades
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

        public RadioButtonItem Radio
        {
            get
            {
                return new RadioButtonItem(tbName.Text, tbCategory.Text, chkChecked.Checked, chkEnabled.Checked);
            }
            set
            {
                tbCategory.Text = value.Category;
                tbName.Text = value.Name;
                chkEnabled.Checked = value.Enabled;
                chkChecked.Checked = value.Checked;
            }
        }
        #endregion

        #region Eventos
        private void btAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void HtmlRadioButtonEditor_Load(object sender, EventArgs e)
        {
            ActiveControl = tbName;

            //Pongo opciones por default
            if (_state == HtmlFormState.Insert)
                chkEnabled.Checked = true;
        }
        #endregion
    }
}