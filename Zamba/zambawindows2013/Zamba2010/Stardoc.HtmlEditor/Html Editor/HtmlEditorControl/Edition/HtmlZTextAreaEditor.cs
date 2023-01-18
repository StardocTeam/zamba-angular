using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.HtmlControls;
using Stardoc.HtmlEditor.Enumerators;

internal partial class HtmlZTextAreaEditor
        : HtmlTextAreaEditor
{
    #region Atributos
    private Int64 _indexId;
    #endregion

    #region Propiedades
    public ZTextAreaItem TextArea
    {
        get
        {
            return new ZTextAreaItem(base.TextArea, _indexId);
        }
        set
        {
            base.TextArea = (TextAreaItem)value;
            _indexId = value.IndexId;
        }
    }
    #endregion

    #region Constructores
    public HtmlZTextAreaEditor()
        : base()
    { }
    public HtmlZTextAreaEditor(ZTextAreaItem textArea, HtmlFormState state)
        : this()
    {
        this.TextArea = textArea;
        base.State = state;
    }
    #endregion
}
