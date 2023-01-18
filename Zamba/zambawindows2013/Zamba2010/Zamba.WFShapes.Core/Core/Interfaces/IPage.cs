using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes
{
    public interface IPage
    {
        #region Events
        event EventHandler<EntityEventArgs> OnEntityAdded;
        event EventHandler<EntityEventArgs> OnEntityRemoved;
        event EventHandler OnClear;
        #endregion

        #region Properties
        CollectionBase<ILayer> Layers { get;}
        ILayer DefaultLayer { get;}
        Ambience Ambience { get;}
        #endregion
        
    }
}
