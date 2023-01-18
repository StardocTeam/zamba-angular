using System;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

internal partial class HtmlZCheckbox
    : HtmlCheckbox
{
    #region Atributos
    private Int64 _indexId;
    #endregion

    #region Propiedades
    public ZCheckBoxItem CheckedBox
    {
        get
        {
            return new ZCheckBoxItem(base.CheckedBox, _indexId);
        }
        set
        {
            base.CheckedBox = (CheckBoxItem)value;
            _indexId = value.IndexId;
        }
    }
    #endregion

    #region Constructores
    public HtmlZCheckbox()
        : base()
    { }

    public HtmlZCheckbox(ZCheckBoxItem checkBox, HtmlFormState state)
        : this()
    {
        this.CheckedBox = checkBox;
        base.State = state;
    }
    #endregion
}
