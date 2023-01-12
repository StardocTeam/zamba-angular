using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface ISimpleShape : IShape
    {
        string Text
        {
            get;
            set;
        }
    }
}
