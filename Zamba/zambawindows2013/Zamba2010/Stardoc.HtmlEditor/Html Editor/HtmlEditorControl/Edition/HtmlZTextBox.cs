using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlZTextBox
    : HtmlTextBox
{
    #region Atributos
    private Int64 _indexId;
    #endregion

    #region Propiedades
    public ZTextBoxItem TextBox
    {
        get
        {
            return new ZTextBoxItem(base.TextBox, _indexId);
        }
        set
        {
            base.TextBox = (TextBoxItem)value;
            _indexId = value.IndexId;
        }
    }
    #endregion

    #region Constructores
    internal HtmlZTextBox()
        : base()
    { }
    internal HtmlZTextBox(ZTextBoxItem item, HtmlFormState state)
        : this()
    {
        base.State = state;
        this.TextBox = item;
    }
    #endregion
}