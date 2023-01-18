using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Zamba.Link
{
    public class MyRenderer : ToolStripProfessionalRenderer
    {
        public MyRenderer() : base(new MyColors()) { }
    }

    public class MyColors : System.Windows.Forms.ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.Gray; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.DarkGray; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.Gray; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.DarkGray; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.DarkGray; }
        }
    }
}
