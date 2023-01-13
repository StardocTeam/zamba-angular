using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Zamba.WFShapes
{
    public interface IMouseListener : IInteraction
    {
        void MouseDown(MouseEventArgs e);
        void MouseMove(MouseEventArgs e);
        void MouseUp(object sender,MouseEventArgs e);
    }
}
