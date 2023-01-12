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
    internal partial class HtmlZTextAreaEditor : Form
    {
        #region Constantes
        private const Decimal DEFAULT_ROW_COUNT = 2;
        private const Decimal DEFAULT_COLUMN_COUNT = 20;

        private const Int32 VALIDATIONS_COUNT = 2;
        private const String ERROR_ZERO_ROW_COUNT = "El campo cantidad de filas debe ser un número mayor a 0";
        private const String ERROR_ZERO_COLUMN_COUNT = "El campo cantidad de columnas debe ser un número mayor a 0";
        private const String TITLE_STATE_VIEW = "Resumen elemento TEXTAREA";
        private const String TITLE_STATE_UPDATE = "Edicion elemento TEXTAREA";
        private const String TITLE_STATE_INSERT = "Creacion elemento TEXTAREA";
        #endregion

        #region Atributos
        private HtmlFormState _state;
        private Int64 _indexId;    
        #endregion

        #region Propiedades
        /// <summary>
        /// Estado del controls
        /// </summary>
        private HtmlFormState State
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

        public ZTextAreaItem TextArea
        {
            get
            {
                ZTextAreaItem TextArea = new ZTextAreaItem(tbName.Text ,   (Int64)nmRows.Value, (Int64)nmColumns.Value, tbText.Text);
                TextArea.IndexId = _indexId;
                TextArea.Enabled = chkEnabled.Checked;
                return TextArea;
            }
            set
            {
                tbName.Text = value.Name;
                nmRows.Value = (decimal)value.RowCount;
                nmColumns.Value = (decimal)value.ColumnCount;
                tbText.Text = value.InnerText;
                _indexId = value.IndexId;
                chkEnabled.Checked = value.Enabled;
            }
        }
        #endregion

        #region Constructores
        public HtmlZTextAreaEditor()
        {
            InitializeComponent();
        }

        public HtmlZTextAreaEditor(ZTextAreaItem textArea, HtmlFormState state)
            : this()
        {
            TextArea = textArea;
            State = state;
        } 
        #endregion

        #region Eventos
        private void TextAreaEditor_Load(object sender, EventArgs e)
        {
            nmRows.Value = DEFAULT_ROW_COUNT;
            nmColumns.Value = DEFAULT_COLUMN_COUNT;
            ActiveControl = tbText;

            if (_state == HtmlFormState.Insert)
                chkEnabled.Enabled = true;
        }
        private void btAccept_Click(object sender, EventArgs e)
        {
            List<String> ErrorMessages = new List<String>();

            if (!IsInputValid(ErrorMessages))
            {
                ErrorHandler ErrorHandler = new ErrorHandler(ErrorMessages);
                ErrorHandler.ShowDialog();
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            Int32 ColumnSize = 0, RowSize = tbText.Lines.Length;

            foreach (String Line in tbText.Lines)
            {
                if (Line.Length > ColumnSize)
                    ColumnSize = Line.Length;
            }

            if (RowSize > (Int32)nmRows.Value)
                nmRows.Value = (Decimal)RowSize;
            if (ColumnSize > (Int32)nmColumns.Value)
                nmColumns.Value =(Decimal) ColumnSize;
        }
        #endregion

        private Boolean IsInputValid(List<String> errorMessages)
        {
            Boolean IsValid = true;
            if (null == errorMessages)
                errorMessages = new List<string>(VALIDATIONS_COUNT);

            if (nmRows.Value == 0)
            {
                IsValid = false;
                errorMessages.Add(ERROR_ZERO_ROW_COUNT);
            }

            if (nmColumns.Value == 0)
            {
                IsValid = false;
                errorMessages.Add(ERROR_ZERO_COLUMN_COUNT);
            }

            return IsValid;
        }

    }
}
