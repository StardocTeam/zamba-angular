using System;
using System.Collections.Generic;
using System.Text;

namespace Zamba.WFShapes.Win
{
public class Controller : ControllerBase
    {

        #region Constructor
        public Controller(IDiagramControl surface) : base(surface)
        {
            //create the view
            View = new View(surface);
            View.AttachToModel(Model);
        }
        #endregion
  
    }
}
