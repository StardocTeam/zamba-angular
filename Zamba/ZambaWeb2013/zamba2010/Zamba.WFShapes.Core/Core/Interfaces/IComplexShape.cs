using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface IComplexShape : IShape
    {
        CollectionBase<IShapeMaterial> Children
        {            
            get;
            set;
        }
    }
}
