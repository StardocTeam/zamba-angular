using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface IGroup : IDiagramEntity
    {
        CollectionBase<IDiagramEntity> Entities { get; set;}

        void CalculateRectangle();
    }
}
