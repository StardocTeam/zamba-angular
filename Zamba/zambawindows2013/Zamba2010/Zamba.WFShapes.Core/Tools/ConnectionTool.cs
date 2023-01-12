using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


public delegate Boolean GeneraEvento_ConnectionmouseUpEventHandler(Zamba.WFShapes.IConnector p1, Zamba.WFShapes.IConnector p2, Zamba.WFShapes.Win.DiagramControl Diagrama);

namespace Zamba.WFShapes
{
   
public class ConnectionTool : AbstractTool, IMouseListener
    {

        #region Fields
        private Point initialPoint;
        private bool doDraw;
       
        // MouseUp de la herramienta Connection
        private GeneraEvento_ConnectionmouseUpEventHandler dMouseUp;
        // Nueva Instancia del class que controla los Move del Connection
        private Zamba.WFShapes.Tools.CapturaConnectionMouse escucha;
        
        #endregion

        #region Constructor
        public ConnectionTool(string name) : base(name)
        {
            this.escucha = new Zamba.WFShapes.Tools.CapturaConnectionMouse();
            this.e_OnMouseUp += new GeneraEvento_ConnectionmouseUpEventHandler(this.escucha.mouseUp);
        }
        #endregion

        #region Methods

        protected override void OnActivateTool()
        {
            Controller.View.CurrentCursor =CursorPallet.Grip;
            this.SuspendOtherTools(); 
            doDraw = false;
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("The argument object is 'null'");
            if (e.Button == MouseButtons.Left && Enabled && !IsSuspended)
            {
               
                    initialPoint = e.Location;
                    doDraw = true;
                               
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("The argument object is 'null'");
            Point point = e.Location;
            if(IsActive && doDraw)
            {
                Controller.View.PaintGhostLine(initialPoint, point);
                Controller.View.Invalidate(System.Drawing.Rectangle.Inflate(Controller.View.Ghost.Rectangle, 20, 20));
            }            
        }
        public void MouseUp(object sender,MouseEventArgs e)
        {
            if (IsActive)
            {
                DeactivateTool();

                //whatever comes hereafter, a compund command is the most economic approach
                CompoundCommand package = new CompoundCommand(this.Controller);

               
        
                //let's see if the connection endpoints hit other connectors
                //note that the following can be done because the actual connection has not been created yet
                //otherwise the search would find the endpoints of the newly created connection, which
                //would create a loop and a stack overflow!
                IConnector startConnector = Selection.FindConnectorAt(initialPoint);
                IConnector endConnector = Selection.FindConnectorAt(e.Location);

            
                #region Create the new connection
                //Todo Wf: ver los id de las etapas que se les pasa cuando se arrastra una conexion
                Connection cn = new Connection(this.initialPoint, e.Location, this.Controller.Model,0,0);
                AddConnectionCommand newcon = new AddConnectionCommand(this.Controller, cn);
                package.Commands.Add(newcon);
                #endregion 
			
                #region Initial attachment?
                if(startConnector != null)
                {
                    BindConnectorsCommand bindStart = new BindConnectorsCommand(this.Controller, startConnector, cn.From);
                    package.Commands.Add(bindStart);
                }
                #endregion 

                #region Final attachment?
                if(endConnector != null)
                {
                    BindConnectorsCommand bindEnd = new BindConnectorsCommand(this.Controller, endConnector, cn.To);
                    package.Commands.Add(bindEnd);
                }
                #endregion 
			    package.Text = "New connection";
                this.Controller.UndoManager.AddUndoCommand(package);

                //do it all
                package.Redo();
                
                try
                {

                    if (null != this.dMouseUp)
                    {
                        Boolean flagOK = this.dMouseUp(startConnector, endConnector, (Zamba.WFShapes.Win.DiagramControl)sender);
                        if (flagOK == true)
                        {
                            cn.Name = "DoDistribuir";
                            package.Redo();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //drop the painted ghost
                Controller.View.ResetGhost();
                //release other tools
                this.UnsuspendTools();
            }
        }
        #endregion
        
        //Habilito el MouseUp del Connection
        public event GeneraEvento_ConnectionmouseUpEventHandler e_OnMouseUp
        {
            add
            {
                this.dMouseUp += value;
            }
            remove
            {
                this.dMouseUp -= value;
            }
        }
    }

}
