using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Zamba.WFShapes.Tools
{
    public class ShapeToolStripMenuItem : ToolStripMenuItem
    {
        private object data = null;

        public object Data {
            get {
                return this.data;
            }
            set {
                this.data = value;
            }
        }
    }
}
