using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.Core;
//using Zamba.WFBusiness;


namespace Zamba.WFShapes
{
    /// <summary>
    /// Maneja los eventos del mouse del diagrama
    /// </summary>
    public class CapturaShapeMouse
    {
        /// <summary>
        /// Graba la posicion y el tamaño en la base de datos al soltar el shape
        /// </summary>
        /// <param name="sender">El Shape a grabar</param>
        /// <param name="e">Se utilizan la posicion X e Y.</param>
        public void mouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Zamba.WFShapes.Win.DiagramControl instanciaDiagramControl = 
                    (Zamba.WFShapes.Win.DiagramControl)sender;

                if (instanciaDiagramControl.SelectedItems.Count > 0) 
                {
                    if (instanciaDiagramControl.SelectedItems[0].EntityName == "Class Shape" || instanciaDiagramControl.SelectedItems[0].EntityName == "Rule Shape")
                    {
                        IShape shape = (IShape)instanciaDiagramControl.SelectedItems[0];
                        
                        // Si grabo la posicion
                        if (bolSize == false)
                        {
                            Point A = new Point(e.X, e.Y);
                            Dif.X = Dif.X - shape.ZambaObject.Location.X;
                            Dif.Y = Dif.Y - shape.ZambaObject.Location.Y;
                            A.X = A.X - Dif.X;
                            A.Y = A.Y - Dif.Y;

                            if (A.X < 20) A.X = 20;
                            if (A.Y < 20) A.Y = 20;

                            if (shape.ZambaObject.Location != A)
                            {
                                shape.ZambaObject.Location = A;
                                if (shape.ZambaObject is IWFStep)
                                {
                                    WfShapesBusiness.UpdateStepPosition(((IWFStep)shape.ZambaObject));
                                }
                                else
                                {
                                    WfShapesBusiness.UpdateShapePosition(shape.ZambaObject);
                                }
                                ActualizarTamaño(instanciaDiagramControl);
                            }
                        }
                        //Si grabo el tamaño
                        else
                        {
                            ComplexShapeBase complexShapeBase = (ComplexShapeBase)shape;
                            //complexShapeBase contiene los hijos del shape

                            //Si no esta expandido
                            if (complexShapeBase.Children[1].Visible == false)
                            {
                                bolSize = false;
                                shape.ZambaObject.Height = shape.Height;
                                shape.ZambaObject.Width = shape.Width;

                                if (shape.ZambaObject is IWFStep)
                                {
                                    WfShapesBusiness.UpdateStepSize(((IWFStep)shape.ZambaObject));
                                }
                                else
                                {
                                    WfShapesBusiness.UpdateShapeSize(shape.ZambaObject);
                                }
                             
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private Cursor previousCursor;
        /// <summary>
        /// Traigo el Shape al frente al moverlo, si estoy en la esquina inferior cambio el cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                //mando el shape adelante
                Zamba.WFShapes.Win.DiagramControl instanciaDiagramControl = (Zamba.WFShapes.Win.DiagramControl)sender;
                instanciaDiagramControl.ActivateTool("SendToFront Tool");

                if (instanciaDiagramControl.SelectedItems.Count > 0)
                {
                    if (instanciaDiagramControl.SelectedItems[0] is IShape)
                    {
                        IShape shape = (IShape)instanciaDiagramControl.SelectedItems[0];
                        AddShapeCommand cmd = new AddShapeCommand(instanciaDiagramControl.Controller, shape, shape.Location);

                        ComplexShapeBase complexShapeBase = (ComplexShapeBase)shape;
                        //complexShapeBase contiene los hijos del shape

                        //Si no esta expandido
                        if (complexShapeBase.Children[1].Visible == false)
                        {

                            // cambio el cursor
                            if (bolSize == false)
                            {
                                if ((shape.Location.X + shape.Width - e.X) < 5 && (shape.Location.X + shape.Width - e.X) > -15 && (shape.Location.Y + shape.Height - e.Y) < 15 && (shape.Location.Y + shape.Height - e.Y) > -15)
                                {
                                    shape.Model.RaiseOnCursorChange(Cursors.SizeNWSE);
                                }
                            }
                            // cambio el tamaño de las shapes
                            else if (bolSize == true)
                            {
                                if (e.Y - shape.Location.Y > 48)
                                    shape.Height = e.Y - shape.Location.Y;
                                else
                                    shape.Height = 48;
                                if (e.X - shape.Location.X > 90)
                                    shape.Width = e.X - shape.Location.X;
                                else
                                    shape.Width = 90;

                            }
                            cmd.Redo();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        Point Dif = new Point();
        bool bolSize;
        
        /// <summary>
        /// Guardo el puntero para usar en el mouseUp y maneja el despliegue de los contextmenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Dif.X = e.X;
                Dif.Y = e.Y;
               
                Zamba.WFShapes.Win.DiagramControl instanciaDiagramControl =
                           (Zamba.WFShapes.Win.DiagramControl)sender;

                
                if (e.Button.ToString() == "Right")
                {
                            
                    Zamba.WFShapes.Core.DiagramShape oDiagramShape = 
                                new Zamba.WFShapes.Core.DiagramShape();

                    bool bolSec = false;

                   //reviso todos los shapes, si el puntero esta dentro de alguno muestro el context menu 
                   foreach (IDiagramEntity shape in instanciaDiagramControl.Controller.Model.Pages[0].DefaultLayer.Entities)
                        if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                        {
                            bool bolPoint2 = oDiagramShape.verificarPuntoShape((IShape)shape, new Point(e.X, e.Y));
                            if (bolPoint2 == true)
                            {
                                
                                shape.contextMenu.Show((System.Windows.Forms.Control)sender, new Point(e.X, e.Y));
                                shape.IsSelected = true;
                                long id = shape.ZambaObject.ID;
                                bolSec = true;
                            }
                            else
                                shape.IsSelected = false;
                        }
                    
                    // Si hay una conexion selecionada le muestro el contextMenu
                    if (bolSec == false)
                    {
                        if (instanciaDiagramControl.SelectedItems.Count > 0)
                            if (instanciaDiagramControl.SelectedItems[0].EntityName == "Default Connection")
                            {
                                IConnection conexion = (IConnection)instanciaDiagramControl.SelectedItems[0];
                                conexion.contextMenu.Show((System.Windows.Forms.Control)sender,new Point(e.X,e.Y));
                            }
                    }
                }
                else
                {
                    if (instanciaDiagramControl.SelectedItems.Count > 0)
                    {
                        if (instanciaDiagramControl.SelectedItems[0].EntityName == "Class Shape" || instanciaDiagramControl.SelectedItems[0].EntityName == "Rule Shape")
                        {
                            IShape shape = (IShape)instanciaDiagramControl.SelectedItems[0];
                            ///Si esta entre estos valores se esta modificando el tamaño
                            if ((shape.Location.X + shape.Width - e.X) < 5 &&
                                (shape.Location.X + shape.Width - e.X) > -15 &&
                                (shape.Location.Y + shape.Height - e.Y) < 15 &&
                                (shape.Location.Y + shape.Height - e.Y) > -15)
                            {
                                bolSize = true;
                            }
                         }
                    }
                }
            }

            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }
        
        /// <summary>
        /// Actualiza el diagrama con los datos del workflow
        /// </summary>
        /// <param name="diagrama"></param>
        private void Actualizar(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            IWorkFlow WF = null;
            Zamba.WFShapes.Core.DiagramShape oDiagramShape
                = new Zamba.WFShapes.Core.DiagramShape();
            if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count > 0)
            {
                IShape shape;
                shape = (IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0];
                WF =(IWorkFlow) WFBusiness.GetWFbyId(shape.WorkflowId,false);
                //WF = (IWorkFlow)((IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities).WFStep.WorkFlow;
            }
            if (WF != null)
            {
                Diagrama.ClearDiagram();
                oDiagramShape.loadWF(Diagrama,WF);
                oDiagramShape.DibujarFlechaWF(Diagrama,WF);
            }
        }

        private void ActualizarTamaño(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            Zamba.WFShapes.Core.DiagramShape oDiagramShape
             = new Zamba.WFShapes.Core.DiagramShape();
            oDiagramShape.ActualizarTamaño(Diagrama);
        }
    }
}