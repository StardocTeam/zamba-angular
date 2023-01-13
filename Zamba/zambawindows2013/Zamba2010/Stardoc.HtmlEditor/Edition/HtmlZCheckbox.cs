using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;

namespace Stardoc.HtmlEditor.Edition
{
    internal partial class HtmlZCheckbox : Form
    {
        #region Constantes
        /// <summary>
        /// Titulo del Form cuando esta en estado View
        /// </summary>
        private const String TITLE_STATE_VIEW = "Resumen elemento CHECKBOX";
        /// <summary>
        /// Titulo del Form cuando esta en estado Edit
        /// </summary>
        private const String TITLE_STATE_UPDATE = "Editar elemento CHECKBOX";
        /// <summary>
        /// Titulo del Form cuando esta en estado Insert
        /// </summary>
        private const String TITLE_STATE_INSERT = "Insertar elemento CHECKBOX";
        #endregion

        #region Atributos
        private HtmlFormState _state = HtmlFormState.Insert;
        private Int64 _indexId;
        #endregion

        #region Propiedades
        public ZCheckBoxItem CheckedBox
        {
            get
            {
                ZCheckBoxItem CheckBox = new ZCheckBoxItem(tbInput.Text, chkChecked.Checked);
                CheckBox.Enabled = chkEnabled.Checked;
                CheckBox.IndexId = _indexId;

                return CheckBox;
            }
            set
            {
                tbInput.Text = value.Name;
                chkChecked.Checked = value.Checked;
                chkEnabled.Checked = value.Enabled;
                _indexId = value.IndexId;
            }
        }
        public HtmlFormState State
        {
            get { return _state; }
            set
            {
                _state = value;
                switch (value)
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
        public HtmlZCheckbox()
        {
            InitializeComponent();
        }

        public HtmlZCheckbox(ZCheckBoxItem checkBox, HtmlFormState state)
            : this()
        {
            State = state;
            CheckedBox = checkBox;
        }
        #endregion

        #region Eventos
        private void btAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void HtmlCheckbox_Load(object sender, EventArgs e)
        {
            ActiveControl = tbInput;
            if (_state == HtmlFormState.Insert)
                chkEnabled.Checked = true;

        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
