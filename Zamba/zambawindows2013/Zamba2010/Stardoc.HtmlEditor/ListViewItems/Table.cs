using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Stardoc.HtmlEditor.Enumerators;
using Stardoc.HtmlEditor.HtmlControls;

namespace Stardoc.HtmlEditor.ListViewItems
{
    internal sealed class Table
              : ListViewItem, IHtmlWritable
    {
        #region Propiedades
        public TableItem InnerControl
        {
            get { return (TableItem)this.Tag; }
            set
            {
                this.Tag = (TableItem)value;
                base.Text = value.Name;
            }
        }
        public HtmlControlType Type
        {
            get { return InnerControl.Type(); }
        }
        #endregion

        #region Constructores
        public Table(Int32 imageIndex)
        {
            this.ImageIndex = imageIndex;
        }
        public Table(Int32 imageIndex, String toolTip)
            : this(imageIndex)
        {
            this.ToolTipText = toolTip;
        }
        public Table(Int32 imageIndex, String toolTip, TableItem innerControl)
            : this(imageIndex, toolTip)
        {
            InnerControl = innerControl;
            SubItems.Add(new ListViewSubItem(this, innerControl.Name));
        }

        #endregion    }

        public string ToHtml()
        {
            return HtmlParser.ParseTable(this.InnerControl);
        }
    }
}