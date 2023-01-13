using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Zamba.WFShapes
{
    interface IKeyboardListener : IInteraction
    {

        void KeyDown(KeyEventArgs e);

        void KeyUp(KeyEventArgs e);

        void KeyPress(KeyPressEventArgs e);

    }
}
