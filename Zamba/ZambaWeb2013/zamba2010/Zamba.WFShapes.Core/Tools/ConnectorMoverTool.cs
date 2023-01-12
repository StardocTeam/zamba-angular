using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Zamba.Core;

namespace Zamba.WFShapes
{
public class ConnectorMoverTool : AbstractTool, IMouseListener
    {

        #region Fields
        private Point initialPoint;
        private Point lastPoint;
        private IConnector fetchedConnector;
        private bool motionStarted;

        #endregion

        #region Constructor
        public ConnectorMoverTool(string name) : base(name)
        {
        }
        #endregion

        #region Methods

        protected override void OnActivateTool()
        {
            Controller.View.CurrentCursor = CursorPallet.Select;
            motionStarted = false;
            fetchedConnector = null;
        }

        IShape shape = null;

        public void MouseDown(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("The argument object is 'null'");
            if (e.Button == MouseButtons.Left && Enabled && !IsSuspended)
            {
               fetchedConnector = Selection.FindShapeConnector(e.Location);
                if (fetchedConnector != null)
                {

                    initialPoint = e.Location;
                    lastPoint = initialPoint;
                    motionStarted = true;
                }
            }
           shape = Selection.FindShape(e.Location);
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("The argument object is 'null'");
            Point point = e.Location;
            if(IsActive && motionStarted)
            {

                fetchedConnector.Move(new Point(point.X - lastPoint.X, point.Y - lastPoint.Y));
                
                lastPoint = point;
            }            
        }
        public void MouseUp(object sender,MouseEventArgs e)
        {
            if (IsActive)
            {
                DeactivateTool();
                if(fetchedConnector == null)
                    return;
                Bundle bundle = new Bundle(Controller.Model);
                bundle.Entities.Add(fetchedConnector);
                MoveCommand cmd = new MoveCommand(this.Controller, bundle, new Point(lastPoint.X - initialPoint.X, lastPoint.Y - initialPoint.Y));
                Controller.UndoManager.AddUndoCommand(cmd);
                //not necessary to perform the Redo action of the command since the mouse-move already moved the bundle!
            }
            
            //Si el conector no esta en un shape actualizo el diagrama
                IShape shape1 = shape;
                shape = Selection.FindShape(e.Location);

                if (shape1 != null)
                    if (shape1 != shape || shape == null)
                    {
                        Zamba.WFShapes.Win.DiagramControl Diagrama =
                                (Zamba.WFShapes.Win.DiagramControl)sender;

                        if (Diagrama != null)
                        {
                            IWorkFlow WF = null;
                            if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count > 0)
                            {
                                IShape myshape;
                                myshape = (IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0];
                                WF =(IWorkFlow) WFBusiness.GetWFbyId(myshape.WorkflowId,false);                                
                            }
                            if (WF != null)
                            {
                                Zamba.WFShapes.Core.DiagramShape oDiagramShape
                                    = new Zamba.WFShapes.Core.DiagramShape();
                                Diagrama.ClearDiagram();
                                oDiagramShape.loadWF(Diagrama,WF);
                                oDiagramShape.DibujarFlechaWF(Diagrama,WF);
                            }
                        }
                    }
        }
        #endregion
    }

}
