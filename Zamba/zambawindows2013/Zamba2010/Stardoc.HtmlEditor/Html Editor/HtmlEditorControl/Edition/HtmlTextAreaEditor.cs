﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Stardoc.HtmlEditor;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlTextAreaEditor 
    : Form
{
    #region Constantes
    protected const Decimal DEFAULT_ROW_COUNT = 2;
    protected const Decimal DEFAULT_COLUMN_COUNT = 20;

    protected const Int32 VALIDATIONS_COUNT = 2;
    protected const String ERROR_ZERO_ROW_COUNT = "El campo cantidad de filas debe ser un número mayor a 0";
    protected const String ERROR_ZERO_COLUMN_COUNT = "El campo cantidad de columnas debe ser un número mayor a 0";
    protected const String TITLE_STATE_VIEW = "Resumen elemento TEXTAREA";
    protected const String TITLE_STATE_UPDATE = "Edicion elemento TEXTAREA";
    protected const String TITLE_STATE_INSERT = "Creacion elemento TEXTAREA";
    #endregion

    #region Atributos
    protected HtmlFormState _state;
    #endregion

    #region Propiedades
    /// <summary>
    /// Cantidad de filas establecidas
    /// </summary>
    protected Int64 RowCount
    {
        get
        {
            return (Int32)nmRows.Value;
        }
        set
        {
            nmRows.Value = (decimal)value;
        }
    }
    /// <summary>
    /// Cantidad de columnas establecidas
    /// </summary>
    protected Int64 ColumnCount
    {
        get
        {
            return (Int32)nmColumns.Value;
        }
        set
        {
            nmColumns.Value = (decimal)value;
        }
    }
    /// <summary>
    /// Texto interno del control
    /// </summary>
    protected String InnerText
    {
        get
        {
            return tbText.Text;
        }
        set
        {
            tbText.Text = value;
        }
    }
    /// <summary>
    /// Estado del controls
    /// </summary>
    protected HtmlFormState State
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

    public TextAreaItem TextArea
    {
        get { return new TextAreaItem(RowCount, ColumnCount, InnerText, chkEnabled.Checked, chkRequired.Checked); }
        set
        {
            RowCount = value.RowCount;
            ColumnCount = value.ColumnCount;
            InnerText = value.InnerText;
            chkEnabled.Checked = value.Enabled;
            chkRequired.Checked = value.Required;
        }
    }
    #endregion

    #region Constructores
    public HtmlTextAreaEditor()
    {
        InitializeComponent();
    }

    public HtmlTextAreaEditor(TextAreaItem textArea, HtmlFormState state)
        : this()
    {
        TextArea = textArea;
        State = state;
    }
    #endregion

    #region Eventos
    private void TextAreaEditor_Load(object sender, EventArgs e)
    {
        if (_state == HtmlFormState.Insert)
            chkEnabled.Checked = true;
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

        if (RowSize > RowCount)
            RowCount = RowSize;
        if (ColumnSize > ColumnCount)
            ColumnCount = ColumnSize;
    }
    #endregion

    protected Boolean IsInputValid(List<String> errorMessages)
    {
        Boolean IsValid = true;
        if (null == errorMessages)
            errorMessages = new List<string>(VALIDATIONS_COUNT);

        if (RowCount == 0)
        {
            IsValid = false;
            errorMessages.Add(ERROR_ZERO_ROW_COUNT);
        }

        if (ColumnCount == 0)
        {
            IsValid = false;
            errorMessages.Add(ERROR_ZERO_COLUMN_COUNT);
        }

        return IsValid;
    }
}