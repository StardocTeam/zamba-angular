using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.Edition
{
    internal partial class HtmlZRadioButtonEditor : Form
    {
        #region Atributos
        private HtmlFormState _state = HtmlFormState.Insert;
        private Int64 _indexId;
        #endregion

        #region Constantes
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
        public HtmlZRadioButtonEditor()
        {
            InitializeComponent();
        }
        public HtmlZRadioButtonEditor(ZRadioButtonItem radio, HtmlFormState state)
        {
            InitializeComponent();
            Radio = radio;
            State = state;
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

        public ZRadioButtonItem Radio
        {
            get
            {
                ZRadioButtonItem RadioButton = new ZRadioButtonItem(tbName.Text, tbCategory.Text, chkChecked.Checked);

                RadioButton.IndexId = _indexId;
                RadioButton.Enabled = chkEnabled.Checked;

                if (rdbYes.Checked)
                    RadioButton.SelectionValue = true;
                else if (rdbNo.Checked)
                    RadioButton.SelectionValue = false;

                return RadioButton;
            }
            set
            {
                tbCategory.Text = value.Category;
                tbName.Text = value.Name;
                _indexId = value.IndexId;

                chkChecked.Checked = value.Checked;
                chkEnabled.Checked = value.Enabled;

                if (value.SelectionValue)
                    rdbYes.Checked = true;
                else
                    rdbNo.Checked = true;
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
        private void HtmlZRadioButtonEditor_Load(object sender, EventArgs e)
        {

            ActiveControl = tbName;

            if (_state == HtmlFormState.Insert)
                chkEnabled.Checked = true;
        }
        #endregion
    }
}
