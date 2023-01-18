using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface ILayer
    {
        CollectionBase<IDiagramEntity> Entities { get;}
    }
}
