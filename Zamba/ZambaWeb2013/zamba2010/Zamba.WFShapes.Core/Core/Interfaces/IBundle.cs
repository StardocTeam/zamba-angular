using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface IBundle : IDiagramEntity
    {
        CollectionBase<IDiagramEntity> Entities { get;}


      
    }
}
