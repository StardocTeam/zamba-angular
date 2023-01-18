using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Zamba.WFShapes
{
    public interface IPaintable
    {
        Rectangle Rectangle
        {
            get;           
        }
        void Paint(Graphics g);
    }
}
