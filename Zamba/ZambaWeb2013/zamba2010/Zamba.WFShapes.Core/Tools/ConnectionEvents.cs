using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Zamba.CommonLibrary;
using Zamba.Core;
using Zamba.Core.Enumerators;

//using Zamba.WFBusiness;

namespace Zamba.WFShapes.Tools
{
    //public delegate void AddedConnection(WFRuleParent rule, WFStep step);
    
    /// <summary>
    /// Esta clase va manejar los eventos de la conexion
    /// </summary>
    public class CapturaConnectionMouse
    {
        InputBox Dialogo;
        //private AddedConnection dAddConnection = null;
        
        public Boolean mouseUp(IConnector p1, IConnector p2,Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            Boolean flagOK = false;
            try
            {
                if (p1 != null && p2 != null)
                {
                    if (p1 != p2)
                    {
                        IShape shape1 = VerificarPuntoInicio(p1, Diagrama.Controller.Model);
                        if (shape1 != null)
                        {
                            IShape shape2 = VerificarPuntoDestino(shape1, p2, Diagrama.Controller.Model);
                            if (shape2 != null)
                            {
                                Int64 intTarea = PreguntarTarea();
                                if (intTarea != 0)
                                {
                                    //string strName = PreguntarNombre();
                                    //if (strName != "")
                                    //{

                                        //Int32 Id = WfShapesBusiness.addRules(intTarea, strName, shape1.WFStep, shape2.WFStep);
                                        Int64 Id = WFRulesBusiness.addDoDistribuirRule(intTarea, "", ((IWFStep)shape1.ZambaObject), ((IWFStep)shape2.ZambaObject));

                                        //Le agrego los ID de la regla y los steps a la conexion
                                        int lastAdd = Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count;
                                        IConnection conexion = (IConnection)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[lastAdd - 1];
                                        conexion.Id = Id;
                                        conexion.Id1 = shape1.ZambaObject.ID;
                                        conexion.Id2 = shape2.ZambaObject.ID;

                                        //Agrego la regla al step
                                        List<Int64> ruleInstancesList = new List<Int64>();
                                        IWFRuleParent rule = WFRulesBusiness.GetInstanceRuleById(Id, shape1.ZambaObject.ID,  false);
                                        ruleInstancesList.Clear();
                                        ruleInstancesList = null;

                                        List<IWFRuleParent> rlist = new List<IWFRuleParent>();
                                    rlist.Add(rule);
                                    WFStep step =(WFStep) shape1.ZambaObject;
                                    //WFRulesBusiness.SetRulesInstances(rlist, ref step);
                                    
                                        IWorkFlow wf =(IWorkFlow) WFBusiness.GetWFbyId(shape1.WorkflowId,false);

                                        wf.Steps.Remove(shape1.ZambaObject.ID);
                                        //shape1.WFStep.WorkFlow.Steps.Remove(shape1.WFStep.ID);
                                        wf.Steps.Add(shape1.ZambaObject.ID, ((IWFStep)shape1.ZambaObject));
                                        //shape1.WFStep.WorkFlow.Steps.Add(shape1.WFStep.ID,shape1.WFStep);
                                        
                                        
                                        //Etiqueta de la conexion
                                        //conexion.label.Text = strName;
                                        //conexion.label.BackColor = System.Drawing.Color.Transparent;
                                        //conexion.label.Location = new Point((p1.Point.X + p2.Point.X) / 2, (p1.Point.Y + p2.Point.Y) / 2);
                                        //Diagrama.Controls.Add(conexion.label);

                                        flagOK = true;
                                    //}
                                }
                            }
                            else
                            {
                                MessageBox.Show("El punto de destino debe ser el conector de un shape distinto al de inicio", "ATENCION");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El punto de inicio debe ser el conector de un shape", "ATENCION");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El punto de destino debe ser distinto al punto de inicio", "ATENCION");
                    }
                }
                else
                {
                    MessageBox.Show("El punto de destino debe ser el conector de un shape", "ATENCION");
                }
           }

            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            Actualizar(Diagrama);
            return flagOK;
      }

        /// <summary>
        /// Verifica si el punto de inicio corresponde a algun shape
        /// </summary>
        /// <param name="p1">punto de inicio del connector</param>
        /// <param name="Diagrama">diagrama que contiene los shapes</param>
        /// <returns></returns>
        public IShape VerificarPuntoInicio(IConnector p1, Zamba.WFShapes.IModel Diagrama)
        {
            try
            {
                if (Diagrama != null)
                {
                    foreach (IDiagramEntity shape in Diagrama.Pages[0].DefaultLayer.Entities)
                        if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                        {
                            IShape shape1 = (IShape)shape;
                            for (int i = 0; i < 4; i++)
                                if (shape1.Connectors[i] == p1) return (IShape)shape1;
                        }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
                return null;
            }
            return null;
        }
        
        
        /// <summary>
        /// verifica que el punto de destino este en un shape que sea diferente al de origen
        /// </summary>
        /// <param name="shape1">shape de origen</param>
        /// <param name="p2">punto de destino</param>
        /// <param name="Diagrama">diagrama que contiene los shapes</param>
        /// <returns></returns>
        public IShape VerificarPuntoDestino(IShape shape1, IConnector p2, Zamba.WFShapes.IModel Diagrama)
        {
            try
            {
                if (Diagrama != null)
                {
                    foreach (IDiagramEntity shape in Diagrama.Pages[0].DefaultLayer.Entities)
                        if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                            if (shape != shape1)
                                for (int i = 0; i < 4; i++)
                                {
                                    IShape shape2 = (IShape)shape;
                                    if (shape2.Connectors[i] == p2) return (IShape)shape2;
                                }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
                return null;
            }
            return null;
        }
        
        /// <summary>
        /// Devuelve la tarea elegida por el usuario
        /// </summary>
        /// <returns></returns>
        public Int64 PreguntarTarea()
        {

            this.Dialogo =
                new InputBox(
                        "Seleccione la tarea a realizar",
                        "Tareas:",
                         Enum.GetNames(typeof(TypesofRules))
                );

            Dialogo.ShowDialog();

            TypesofRules valor = (TypesofRules)Enum.Parse(typeof(TypesofRules), Dialogo.ReturnItem, true);

            Dialogo.Dispose();

            return (Int32)valor;
        }

        /// <summary>
        /// Actualizo el diagrama para borrar las flechas que no se conectaron
        /// </summary>
        /// <param name="diagrama">Diagrama a actualizar</param>
        public void Actualizar(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            IWorkFlow WF = null;
            Zamba.WFShapes.Core.DiagramShape oDiagramShape
                    =  new Zamba.WFShapes.Core.DiagramShape();
            if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count > 0)
                if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0].EntityName == "Class Shape" || Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0].EntityName == "Rule Shape")
                {
                    IShape myshape = (IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0];
                    WF = (IWorkFlow)WFBusiness.GetWFbyId(myshape.WorkflowId,false);
                    //WF = (IWorkFlow)((IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0]).WFStep.WorkFlow;
                }
                Diagrama.ClearDiagram();
                if (WF != null)
                {
                    oDiagramShape.loadWF(Diagrama,WF);
                    oDiagramShape.DibujarFlechaWF(Diagrama,WF);
                }
        }
    }
}
