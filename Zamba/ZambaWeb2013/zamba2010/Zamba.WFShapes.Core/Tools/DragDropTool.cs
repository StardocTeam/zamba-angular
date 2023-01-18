using System;
using System.Diagnostics;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Zamba.Core;
using Zamba.WFShapes.Core;

using Zamba.CommonLibrary;

namespace Zamba.WFShapes
{
public class DragDropTool : AbstractTool, IMouseListener, IDragDropListener
    {

        private InputBox Dialogo;

        #region Fields
        Cursor feedbackCursor;
        #endregion

        #region Constructor
        public DragDropTool(string name) : base(name)
        { 
        }
        #endregion

        #region Methods

        protected override void OnActivateTool()
        {
            Controller.View.CurrentCursor = Cursors.SizeAll;
        }

        public void MouseDown(MouseEventArgs e)
        {
          
        }

        public void MouseMove(MouseEventArgs e)
        {
           
        }
        public void MouseUp(object sender,MouseEventArgs e)
        {
            
        }
        #endregion

        public void OnDragDrop(DragEventArgs e)
        {
            //this.Dialogo = new InputBox("Ingrese el nombre de la Etapa", "Agregar Etapa","");

            //string nameShape;
            //Dialogo.ShowDialog();
            //nameShape = Dialogo.Texto;
            //Dialogo.Dispose();
            //if (nameShape != "" && nameShape != null)
            //{
            //    //WorkFlow WF = null;
            //    Point p;

            //    Control control = (Control)this.Controller.ParentControl;
            //    p = control.PointToClient(new Point(e.X, e.Y));

            //    IShape shape = null;
            //    IDataObject iDataObject = e.Data;
                            
            //    if(iDataObject.GetDataPresent(typeof(string)))
            //    {
            //        foreach(string shapeType in Enum.GetNames(typeof(ShapeTypes)))
            //        {
            //            if(shapeType.ToString().ToLower() == iDataObject.GetData(typeof(string)).ToString().ToLower())
            //            {
            //            //Todo: obtener el id
            //                Int32 Id = 1;
            //                shape = ShapeFactory.GetShape(Id,shapeType);
            //                break;
            //            }
            //        }
            //    }
                
            //    if(iDataObject.GetDataPresent(typeof(Bitmap)))
            //    {
            //        return;
            //    }

            //    if(shape != null)
            //    {
            //        DiagramShape nuevoShape = new DiagramShape();

            //        WF = nuevoShape.fillWF();
            //        WFStep NewStep = new WFStep(WF, ToolsBusiness.GetNewID(ZClass.IdTypes.WFSTEP), nameShape, "", new Point(p.X, p.Y), 5, 0, 0, false, shape.ShapeColor.Name,150, 50);
            //        WFStepBusiness.AddStep(NewStep, WF);

            //        ((ShapeBase)shape).WFStep = NewStep;
            //        shape.Height = NewStep.Height;
            //        shape.Width = NewStep.Width;
            //        shape.Location = new Point(p.X, p.Y);
            //        shape.WFStep.Location = shape.Location;
                    
            //        shape.ShapeColor = Color.Empty;
                    
            //        AddShapeCommand cmd = new AddShapeCommand(this.Controller, shape, p);

            //        //Guardo la posicion,el tamaño y el color del shape del nuevoshape
            //        shape.WFStep.color = "";
            //        Zamba.Core.WFStepsComponent.UpdateStepPosition(shape.WFStep);
            //        Zamba.Data.WFStepsFactory.UpdateStepColor(shape.WFStep);
            //        Zamba.Core.WFStepsComponent.UpdateStepSize(shape.WFStep);
                    
            //        //todo: levantar evento de agregado de etapa
            //        //this.Controller.UndoManager.AddUndoCommand(cmd);
            //        cmd.Redo();
            //        //feedbackCursor = null;
                   
            //    }
            //}
        }

        public void OnDragLeave(EventArgs e)
        {
            
        }

        public void OnDragOver(DragEventArgs e)
        {   
        }


        public void OnDragEnter(DragEventArgs e)
        {
            AnalyzeData(e);


        }

        private void AnalyzeData(DragEventArgs e)
        {
            IDataObject iDataObject = e.Data;
            if(iDataObject.GetDataPresent(typeof(string)))
            {
                foreach(string shapeType in Enum.GetNames(typeof(ShapeTypes)))
                {
                    if(shapeType.ToString().ToLower() == iDataObject.GetData(typeof(string)).ToString().ToLower())
                    {
                        feedbackCursor = CursorPallet.DropShape;
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }

                feedbackCursor = CursorPallet.DropText;
                e.Effect = DragDropEffects.Copy;
                return;


            }
            if(iDataObject.GetDataPresent(typeof(Bitmap)))
            {
                feedbackCursor = CursorPallet.DropImage;
                e.Effect = DragDropEffects.Copy;
                return;

            }
        }


        public void GiveFeedback(GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            Cursor.Current = feedbackCursor;
        }
     
    
    }

}
