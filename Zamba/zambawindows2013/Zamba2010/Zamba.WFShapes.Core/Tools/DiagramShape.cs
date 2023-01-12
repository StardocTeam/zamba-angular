using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using Zamba.WFShapes;
//using Zamba.WFBusiness;
using Zamba.CommonLibrary;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Data;
using Zamba.Core.Enumerators;
using Zamba.WFShapes.Win;

namespace Zamba.WFShapes.Core
{
    //Declaro el delegado que le dice al mainform que se agrego un shape
    public delegate void AddedShape(IWFStep NewStep);
    public delegate void AddedRuleShape(IWFStep ruleStep, Int64 IDRule);
    public delegate void RemovedShape(IWFStep DelStep);
    public delegate void NameShape(IWFStep DelStep);
    public delegate void RemovedConnection(IWFRuleParent rule, IWFStep step);
    public delegate void NameConnection(Int64 id, String name);
    /// <summary>
    /// Clase que maneja los eventos del diagrama
    /// </summary>
    public class DiagramShape
    {
        #region Atributos
        private Int32 Id;
        #endregion
        protected List<IWFStep> step;
        private Boolean showTasks = false;
        //Declaro una variable del delegado
        private AddedShape dAddShape = null;
        private AddedRuleShape dAddRuleShape = null;
        private NameShape _dNameShape = null;
        private RemovedShape _dRemoveShape = null;
        private RemovedConnection _dRemoveConnection = null;
        private NameConnection _dNameConnection = null;

        public DiagramShape()
        {
            this.step = new List<IWFStep>();
        }
        public DiagramShape(Boolean showTask)
        {
            this.step = new List<IWFStep>();
            this.showTasks = showTask;
        }
        Int32 maxY = 0;
        Int32 maxX = 0;


        public void loadWF(Zamba.WFShapes.Win.DiagramControl Diagrama, IWorkFlow wf)
        {
            try
            {
                if (wf != null)
                {
                    foreach (IWFStep s in wf.Steps.Values)
                    {
                        this.step.Add(s);
                        IShape StepShape = this.agregarShape(s, Diagrama);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void loadWFStep(Zamba.WFShapes.Win.DiagramControl Diagrama, IWFStep WFStep)
        {
            try
            {
                if (WFStep != null)
                {

                    //AddStartShape
                    IShape StartShape = null;
                    //AddActionsShape
                    IShape ActionsShape = null;
                    //AddEventsShape
                    IShape EventsShape = null;
                    //AddProcessShape
                    IShape ProcessShape = null;
                    //AddProcessShape
                    IShape ServiceShape = null;
                    //AddEndShape
                    IShape EndShape = null;

                    bool IsFirst = true;
                    string Name = string.Empty;
                    DataTable dt = null;

                    DataTable DTStepsRulesOptions = WFRulesBusiness.GetRuleOptionsDT(false, WFStep.ID);
                    DataView DVRuleOptions = default(DataView);
                    if (DTStepsRulesOptions != null)
                    {
                        DVRuleOptions = new DataView(DTStepsRulesOptions);
                    }

                    try
                    {
                        foreach (DsRules.WFRulesRow r in WFStep.DSRules.WFRules.Rows)
                        {
                            if ((DVRuleOptions != null))
                                DVRuleOptions.RowFilter = "RuleId = " + r.Id;


                            if (r.ParentId == 0)
                            {

                                switch ((TypesofRules)r.ParentType)
                                {
                                    case TypesofRules.Entrada:
                                        AddRuleShape(StartShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.Eventos:
                                        AddRuleShape(EventsShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.AccionUsuario:
                                       

                                        AddRuleShape(ActionsShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);

                                        //Actualiza el nombre
                                        try
                                        {
                                            //dt = WFRulesBusiness.GetRuleOption(r.step_Id, r.Id, 0, Icons.Warning, 0, true);
                                            //if ((dt != null) && dt.Rows.Count > 0)
                                            //{
                                            //    Name = dt.Rows[0].Item["ObjExtraData"].ToString();
                                            //}
                                        }
                                        catch (Exception ex)
                                        {
                                            ZClass.raiseerror(ex);
                                        }

                                        //if (string.IsNullOrEmpty(Name))
                                        //{
                                        //if (string.Compare(r._Class, "DOExecuteRule") == 0 && r.Name.StartsWith("Ejecutar Regla "))
                                        //{
                                        //    UserActionNode.UpdateUserActionNodeName(r.Name.Replace("Ejecutar Regla ", string.Empty));
                                        //}
                                        //else
                                        //{
                                        //    UserActionNode.UpdateUserActionNodeName(r.Name);
                                        //}
                                        //}
                                        //else
                                        //{
                                        // UserActionNode.UpdateUserActionNodeName(Name);
                                        //}
                                        // Name = string.Empty;
                                        break;
                                    case TypesofRules.Floating:
                                        AddRuleShape(ProcessShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.Actualizacion:
                                        AddRuleShape(ServiceShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.Planificada:
                                        AddRuleShape(ServiceShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.ValidacionEntrada:
                                        AddRuleShape(StartShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.Salida:
                                        AddRuleShape(EndShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                    case TypesofRules.ValidacionSalida:
                                        AddRuleShape(EndShape, WFStep.DSRules, r, true, (TypesofRules)r.Type, 0, (TypesofRules)r.ParentType, WFStep, DVRuleOptions, Diagrama);
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    finally
                    {
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void loadWFStep(Zamba.WFShapes.Win.DiagramControl Diagrama, IWFStep WFStep, TypesofRules ParentType)
        {
            try
            {
                if (WFStep != null)
                {

                    //AddStartShape
                  //  IShape StartShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);
                    //AddActionsShape
                  //  IShape ActionsShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);
                    //AddEventsShape
                 //   IShape EventsShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);
                    //AddProcessShape
                 //   IShape ProcessShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);
                    //AddProcessShape
                  //  IShape ServiceShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);
                    //AddEndShape
                 //   IShape EndShape = agregarShape("Start", "classShape", new Point(10, 10), Diagrama, WFStep);

                    bool IsFirst = true;
                    string Name = string.Empty;
                    DataTable dt = null;

                    DataTable DTStepsRulesOptions = WFRulesBusiness.GetRuleOptionsDT(false, WFStep.ID);
                    DataView DVRuleOptions = default(DataView);
                    if (DTStepsRulesOptions != null)
                    {
                        DVRuleOptions = new DataView(DTStepsRulesOptions);
                    }

                    DataView DvRules = new DataView(WFStep.DSRules.WFRules);
                    DvRules.RowFilter = String.Format("ParentType = {0}", (int)ParentType);

                    try
                    {
                        foreach (DataRow r in DvRules.ToTable().Rows)
                        {
                            var r1 = WFStep.DSRules.WFRules.NewWFRulesRow();
                            r1.ItemArray = r.ItemArray;

                            if ((DVRuleOptions != null))
                                DVRuleOptions.RowFilter = "RuleId = " + r1.Id;


                            switch ((TypesofRules)r1.ParentType)
                            {
                                case TypesofRules.Entrada:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.Eventos:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.AccionUsuario:
                                    

                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);

                                    //Actualiza el nombre
                                    try
                                    {
                                        //dt = WFRulesBusiness.GetRuleOption(r.step_Id, r.Id, 0, Icons.Warning, 0, true);
                                        //if ((dt != null) && dt.Rows.Count > 0)
                                        //{
                                        //    Name = dt.Rows[0].Item["ObjExtraData"].ToString();
                                        //}
                                    }
                                    catch (Exception ex)
                                    {
                                        ZClass.raiseerror(ex);
                                    }

                                    //if (string.IsNullOrEmpty(Name))
                                    //{
                                    //if (string.Compare(r._Class, "DOExecuteRule") == 0 && r.Name.StartsWith("Ejecutar Regla "))
                                    //{
                                    //    UserActionNode.UpdateUserActionNodeName(r.Name.Replace("Ejecutar Regla ", string.Empty));
                                    //}
                                    //else
                                    //{
                                    //    UserActionNode.UpdateUserActionNodeName(r.Name);
                                    //}
                                    //}
                                    //else
                                    //{
                                    // UserActionNode.UpdateUserActionNodeName(Name);
                                    //}
                                    // Name = string.Empty;
                                    break;
                                case TypesofRules.Floating:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.Actualizacion:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.Planificada:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.ValidacionEntrada:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.Salida:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                                case TypesofRules.ValidacionSalida:
                                    AddRuleShape(null, WFStep.DSRules, r1, true, (TypesofRules)r1.Type, 0, (TypesofRules)r1.ParentType, WFStep, DVRuleOptions, Diagrama);
                                    break;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    finally
                    {
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private IShape AddRuleShape(IShape parentShape, DsRules DSRules, DsRules.WFRulesRow RuleRow, bool ruleEnable, TypesofRules ruleType, Int64 ruleParentId, TypesofRules ruleParentType,
        IWFStep WFStep, DataView DVRuleOptions, Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            try
            {
                Icons icon = Icons.YellowBall;

                if ((DVRuleOptions != null))
                {
                    DataTable DTRuleOptions = DVRuleOptions.ToTable();

                    ruleEnable = true;
                    bool DisableChildRules = false;
                    bool RefreshTask = false;
                    Int32 IconId = (Int32)Icons.YellowBall;

                    if (DTRuleOptions.Rows.Count > 0)
                    {
                        foreach (DataRow o in DTRuleOptions.Rows)
                        {
                            switch (int.Parse(o["ObjectId"].ToString()))
                            {
                                case 0:
                                    ruleEnable = Convert.ToBoolean(o["ObjectId"]);
                                    break;
                                case 59:
                                    DisableChildRules = Convert.ToBoolean(o["ObjectId"]);
                                    break;
                                case 42:
                                    RefreshTask = Convert.ToBoolean(o["ObjectId"]);
                                    break;
                                case 63:
                                    IconId = int.Parse(o["ObjValue"].ToString());
                                    break;
                            }
                        }
                    }
                    icon = WFRulesBusiness.GetIcon(ruleEnable, RuleRow._Class, DisableChildRules, RefreshTask, IconId, RuleRow.Name);
                }

                //agrega la regla al nodo y verifica si tiene child para agregar
                //RuleNode ruleNode = new RuleNode(ruleId, ruleName, ruleClass, ruleEnable, ruleType, ruleParentId, ruleParentType, stepId, icon);
                //parentNode.Nodes.Add(ruleNode);
                Point Location = WFStepBusiness.GetShapePosition((Int64)RuleRow.Id);

                if (Location == null || (Location.X == 0 && Location.Y == 0))
                {
                    if (parentShape != null)
                        Location = new Point(parentShape.Location.X + 20, parentShape.Location.Y + 70);
                    else
                        Location = new Point(10, 10);
                }
                IShape ruleshape = this.agregarShape(RuleRow, Diagrama, WFStep, Location);

                if (parentShape != null)
                    this.agregarflecha(parentShape, ruleshape, Diagrama);

                if (DSRules != null)
                {
                    foreach (DsRules.WFRulesRow r in DSRules.WFRules.Select("ParentId = " + RuleRow.Id))
                    {
                        if ((DVRuleOptions != null))
                            DVRuleOptions.RowFilter = "RuleId = " + r.Id;
                        AddRuleShape(ruleshape, DSRules, r, ruleEnable, (TypesofRules)r.Type, (Int64)RuleRow.Id, (TypesofRules)RuleRow.Type, WFStep, DVRuleOptions, Diagrama);
                    }
                }

                return ruleshape;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        public void AddChildRule(DiagramControl Diagrama, IRule parentRule,IShape parentShape, IRule newRule, IWFStep WFStep)
        {
            Icons icon = Icons.YellowBall;

                bool ruleEnable = true;
                bool DisableChildRules = false;
                bool RefreshTask = false;
                Int32 IconId = (Int32)Icons.YellowBall;

                icon = WFRulesBusiness.GetIcon(ruleEnable, newRule.RuleClass, DisableChildRules, RefreshTask, IconId, newRule.Name);

            Point Location;
            if (newRule.RuleClass.ToLower().Contains("ifbranch")) {
                if (newRule.IfType == true)
                {
                    Location = new Point(parentShape.Location.X, parentShape.Location.Y + 80);
                }
                else
                {
                    Location = new Point(parentShape.Location.X + 75, parentShape.Location.Y + 80);
                }
            }
                        else
            {
                 Location = new Point(parentShape.Location.X + 20, parentShape.Location.Y + 80);
            }
            var r = WFStep.DSRules.WFRules.NewWFRulesRow();
            r.Id = newRule.ID;
            r.Name = newRule.Name;
            r.step_Id = WFStep.ID;
            r.Type = (decimal)newRule.RuleType;
            r.ParentId = newRule.ParentRule.ID;
            r.ParentType = (decimal)newRule.ParentType;
            r._Class = newRule.RuleClass;
            r.Enable = newRule.Enable ? 1 : 0;
            r.Version = newRule.Version;
            WFStep.DSRules.WFRules.AddWFRulesRow(r);
            WFStep.DSRules.WFRules.AcceptChanges();

            IShape ruleshape = this.agregarShape(r, Diagrama, WFStep, Location);

            if (parentShape != null)
                this.agregarflecha(parentShape, ruleshape, Diagrama);
                        
                foreach (IRule childRule in newRule.ChildRules)
                {
                    AddChildRule(Diagrama, newRule, ruleshape, childRule, WFStep);
                }
        }

      
        public void ActualizarTamaño(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                {
                    ComplexShapeBase oShape = (ComplexShapeBase)shape;
                    if (oShape.Location.X + oShape.Width > maxX) maxX = oShape.Location.X + oShape.Width;
                    if (oShape.Location.Y + oShape.Height > maxY) maxY = oShape.Location.Y + oShape.Height;
                }
            Diagrama.Size = new Size(maxX + 10, maxY + 10);
        }


        /// <summary>
        /// Agrega un Shape y le asigna los valores del WFStep
        /// </summary>
        /// <param name="s">step del shape</param>
        /// <param name="Diagrama">diagrama que va a contener al shape</param>
        public IShape agregarShape(string Name, string classshape,  Zamba.WFShapes.Win.DiagramControl Diagrama, IZambaCore ZambaObject)
        {
            try
            {
                IShape shape = null;

                // Se le asigna al shape el menu y el WFStep
                shape = ShapeFactory.GetShape(++Id, classshape);

                // ((ClassShape)shape).Title = Name;

                if (shape != null)
                {
                    AddShapeCommand cmd = new AddShapeCommand(Diagrama.Controller, shape, ZambaObject.Location);
                    bool bolColor = false;
                    Int32 height = 0;

                    shape.Height = 48 + height;
                    shape.Width = 90;

                    cmd.Redo();                
                    shape.Location = shape.ZambaObject.Location;
                    shape.ShapeColor = Color.Empty;
                    shape.ZambaObject = ZambaObject;
                    shape.WFStepId = ((IRule)ZambaObject).WFStepId;
                    shape.WorkflowId = Zamba.Core.WFBusiness.GetWorkflowByStepId(((IRule)ZambaObject).WFStepId).ID;
                }
                return shape;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        /// <summary>
        /// Agrega un Shape y le asigna los valores del WFStep
        /// </summary>
        /// <param name="s">step del shape</param>
        /// <param name="Diagrama">diagrama que va a contener al shape</param>
        public IShape agregarShape(IWFStep s, Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            try
            {               
                IShape shape = null;

                // Se le asigna al shape el menu y el WFStep
                shape = ShapeFactory.GetShape(++Id, "classshape");
                ((ClassShape)shape).ShowTasks = showTasks;
                ((ShapeBase)shape).ZambaObject = s;
                shape.WorkflowId = s.WorkId;
                shape.WFStepId = s.ID;

                if (showTasks == true)
                {
                    ((ClassShape)shape).Title = s.Name + " (" + s.TasksCount + ")"; ;
                }
                else
                    ((ClassShape)shape).Title = s.Name;

                if (shape != null)
                {
                    AddShapeCommand cmd = new AddShapeCommand(Diagrama.Controller, shape, s.Location);
                    bool bolColor = false;
                    Int32 height = 0;
                    if (s.Description.Length > 13 && shape.Width < s.Description.Length * 8.38)
                        height = 12;
                    if (shape.ZambaObject.Height > 48 + height)
                    {
                        shape.Height = shape.ZambaObject.Height;
                    }
                    else
                        shape.Height = 48 + height;
                    if (shape.ZambaObject.Width > 90)
                    {
                        shape.Width = shape.ZambaObject.Width;
                    }
                    else
                        shape.Width = 90;
                    cmd.Redo();
                    shape.Location = shape.ZambaObject.Location;
                    if (shape.ZambaObject.color.Trim() != "")
                        for (int i = 0; i < 200; i++)
                        {
                            if (Color.FromKnownColor((KnownColor)i).Name == shape.ZambaObject.color)
                            {
                                shape.ShapeColor = Color.FromKnownColor((KnownColor)i);
                                bolColor = true;
                            }
                        }
                    if (bolColor == false)
                        shape.ShapeColor = Color.Empty;
                }
                return shape;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Agrega un Shape y le asigna los valores del WFStep
        /// </summary>
        /// <param name="s">step del shape</param>
        /// <param name="Diagrama">diagrama que va a contener al shape</param>
        public IShape agregarShape(DsRules.WFRulesRow r, Zamba.WFShapes.Win.DiagramControl Diagrama, IWFStep s, Point Location)
        {
            try
            {
                IShape shape = null;

                // Se le asigna al shape el menu y el WFStep
                shape = ShapeFactory.GetShape(++Id, "ruleshape");
                // ((RuleShape)shape).ShowTasks = showTasks;
                ((RuleShape)shape).RuleRow = r;
                ((RuleShape)shape).Title = r.Name;

                if (shape != null)
                {
                    shape.ZambaObject = WFRulesBusiness.GetInstanceRuleById((Int64)r.Id, false);
                    shape.WFStepId = s.ID;
                    shape.WorkflowId = s.WorkId;
                    shape.Location = Location;
                    shape.ZambaObject.Location = Location;
                    AddShapeCommand cmd = new AddShapeCommand(Diagrama.Controller, shape, Location);
                    bool bolColor = false;
                    Int32 height = 0;
                    if (r.Name.Length > 13 && shape.Width < r.Name.Length * 8.38)
                        height = 12;
                    if (r._Class.ToLower().Contains("ifbranch"))
                    {
                        shape.ZambaObject.Height = 24;
                            shape.Height = 24;
                        shape.ZambaObject.Width = 70;
                        shape.Width = 70;
                    }
                    else
                    {
                        if (shape.ZambaObject.Height > 48 + height)
                        {
                            shape.Height = shape.ZambaObject.Height;
                        }
                        else
                            shape.Height = 48 + height;
                        if (shape.ZambaObject.Width > 150)
                        {
                            shape.Width = shape.ZambaObject.Width;
                        }
                        else
                            shape.Width = 150;

                    }
                    cmd.Redo();
                    shape.Location = shape.ZambaObject.Location;
                    if (shape.ZambaObject.color.Trim() != "")
                        for (int i = 0; i < 200; i++)
                        {
                            if (Color.FromKnownColor((KnownColor)i).Name == shape.ZambaObject.color)
                            {
                                shape.ShapeColor = Color.FromKnownColor((KnownColor)i);
                                bolColor = true;
                            }
                        }
                    if (bolColor == false)
                        shape.ShapeColor = Color.Empty;
                }
                return shape;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Une todos los shapes de un WorkFlow con flechas
        /// </summary>
        /// <param name="Diagrama">Diagrama que contiene los shapes</param>
        public void DibujarFlechaWF(Zamba.WFShapes.Win.DiagramControl Diagrama, IWorkFlow wf)
        {
            try
            {
                IShape shape1 = null;
                IShape shape2 = null;

                int i = 0;

                if (wf != null)
                {

                    ArrayList listCon;
                    Diagrama.Controls.Clear();

                    //Obtengo un List con todas las conexiones de los shapes
                    listCon = WfShapesBusiness.FillTransitions(wf);

                    if (listCon != null)
                    {
                        i = 0;
                        for (i = 0; i < listCon.Count; ++i)
                        {
                            string[] tmpCadena = (string[])listCon[i];
                            shape1 =
                                 this.GetClassShape(tmpCadena[0], Diagrama.Controller.Model);
                            shape2 =
                                 this.GetClassShape(tmpCadena[1], Diagrama.Controller.Model);

                            this.agregarflecha(shape1, shape2, Diagrama);
                            int lastAdd = Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count;
                            Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[lastAdd - 1].Id = int.Parse(tmpCadena[2]);
                            //if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[lastAdd - 1].EntityName == "Default Connection")
                            //{
                            //    IConnection conexion = (IConnection)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[lastAdd - 1];
                            //    conexion.label.Text = WfShapesBusiness.GetRuleNameById(int.Parse(tmpCadena[2]));
                            //    if (conexion.label.Text == "" || conexion.label.Text == "Regla")
                            //    {
                            //       if (shape2 != null) conexion.label.Text = "Envio a " + shape2.WFStep.Name;
                            //    }
                            //    conexion.label.BackColor = System.Drawing.Color.Transparent;
                            //    if (shape1 != null && shape2 != null)
                            //    {
                            //    conexion.label.Location = new Point((shape1.Connectors[C1].Point.X + shape2.Connectors[C2].Point.X) / 2, (shape1.Connectors[C1].Point.Y + shape2.Connectors[C2].Point.Y) / 2);
                            //    Diagrama.Controls.Add(conexion.label);
                            //    }
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }


        Int32 C1;
        Int32 C2;

        /// <summary>
        /// Elije los conectares mas cercanos entre los 2 workflows
        /// </summary>
        /// <param name="shape1">shape origen</param>
        /// <param name="shape2">shape destino</param>
        /// <param name="C1">devuelve conector del shape oigen</param>
        /// <param name="C2">devuelve conector del shape destino</param>
        public void ElegirConector(IShape shape1, IShape shape2, out Int32 C1, out Int32 C2)
        {
            C1 = 0;
            C2 = 0;

            if (shape1 != null && shape2 != null)
            {
                Point p1 = shape1.Location;
                Point p2 = shape2.Location;

                int width1 = shape1.Width;
                int width2 = shape2.Width;

                // Si c1 abajo de c2
                if ((p2.X + width2 > p1.X) && (p1.Y > p2.Y) && (p1.X + width1) > p2.X)
                {
                    C1 = 0;
                    C2 = 2;
                }
                // Si c1 arriba de c2
                else if ((p1.X + width1 > p2.X) && (p2.Y > p1.Y) && (p2.X + width2) > p1.X)
                {
                    C1 = 2;
                    C2 = 0;
                }
                // Si c1 a la derecha de c2
                else if (p2.X > p1.X)
                {
                    C1 = 1;
                    C2 = 3;
                }
                // Si c1 a la izquierda de c2
                else if (p1.X > p2.X)
                {
                    C1 = 3;
                    C2 = 1;
                }
            }
        }


        /// <summary>
        /// Agrega una flecha entre los 2 shapes indicados
        /// </summary>
        /// <param name="shape1"></param>
        /// <param name="shape2"></param>
        public void agregarflecha(IShape shape1, IShape shape2, Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            Point initialPoint;

            if (shape1 != null && shape2 != null)
            {
                this.ElegirConector(shape1, shape2, out C1, out C2);

                initialPoint = shape1.Connectors[C1].Point;
                CompoundCommand package = new CompoundCommand(Diagrama.Controller);

                IConnector startConnector = Selection.FindConnectorAt(shape1.Connectors[C1].Point);
                IConnector endConnector = Selection.FindConnectorAt(shape2.Connectors[C2].Point);
                #region Create the new connectio
                Connection cn = new Connection(initialPoint, shape2.Connectors[C1].Point, Diagrama.Controller.Model, shape1.ZambaObject.ID, shape2.ZambaObject.ID);
                AddConnectionCommand newcon = new AddConnectionCommand(Diagrama.Controller, cn);
                package.Commands.Add(newcon);
                #endregion

                #region Initial attachment?
                if (startConnector != null)
                {
                    BindConnectorsCommand bindStart = new BindConnectorsCommand(Diagrama.Controller, startConnector, cn.From);
                    package.Commands.Add(bindStart);
                }
                #endregion

                #region Final attachment?
                if (endConnector != null)
                {
                    BindConnectorsCommand bindEnd = new BindConnectorsCommand(Diagrama.Controller, endConnector, cn.To);//Ver si Controller = Diagrama
                    package.Commands.Add(bindEnd);
                }
                #endregion
                package.Text = "New connection";
                Diagrama.Controller.UndoManager.AddUndoCommand(package);

                ////do it all
                package.Redo();
            }
        }

        /// <summary>
        /// Devuelve el shape del diagrama que contiene el mismo id de WFStep
        /// </summary>
        /// <param name="s">WFStep a buscar</param>
        /// <param name="Diagrama">Diagrama donde se va a buscar el WFStep</param>
        /// <returns></returns>
        public IShape GetClassShape(string Id, Zamba.WFShapes.IModel Diagrama)
        {
            try
            {
                if (Diagrama != null)
                {
                    foreach (IDiagramEntity shape in Diagrama.Pages[0].DefaultLayer.Entities)
                        if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                        {
                            IShape shape1 = (IShape)shape;
                            if (shape1.ZambaObject.ID.ToString() == Id) return shape1;
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
        /// elimina un shape
        /// </summary>
        /// <param name="Diagrama">diagrama que contiene el shape</param>
        public void EliminarShape(IShape shape, WFShapes.Win.DiagramControl Diagrama)
        {
            try
            {
                if (shape != null)
                {
                    IWFStep s = (IWFStep)shape.ZambaObject;
                    WfShapesBusiness.DelStep(s);
                    IWorkFlow WF = (IWorkFlow)WFBusiness.GetWFbyId(s.WorkId, false);
                    WF.Steps.Remove(shape.ZambaObject.ID);
                    //shape.WFStep.WorkFlow.Steps.Remove(shape.WFStep.ID);
                    Diagrama.Controller.Model.RemoveShape(shape);
                    if (_dRemoveShape != null)
                        _dRemoveShape(s);
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        /// <summary>
        /// elimina una regla 
        /// </summary>
        /// <param name="Diagrama">diagrama que contiene la regla</param>
        public void EliminarRule(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            try
            {
                IWorkFlow WF = null;
                if (Diagrama.SelectedItems.Count > 0)
                {
                    IConnection conexion = (IConnection)Diagrama.SelectedItems[0];
                    foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                        if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                            if (shape.ZambaObject.ID == conexion.Id1)
                            {
                                IWFStep s = (IWFStep)shape.ZambaObject;

                                for (Int32 i = 0; i < s.DSRules.WFRules.Count; i++)
                                    if (s.DSRules.WFRules[i].Id == conexion.Id)
                                    {
                                        WfShapesBusiness.DeleteRuleByID(conexion.Id);
                                        //Si tiene delegado
                                        if (_dRemoveConnection != null)
                                            _dRemoveConnection((IWFRuleParent)s.DSRules.WFRules.Rows[i], s);
                                        else
                                            s.DSRules.WFRules.RemoveWFRulesRow(s.DSRules.WFRules[i]);
                                        break;
                                    }
                                break;
                            }
                    // Actualizo los diagramas
                    if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count > 0)
                    {
                        IShape myshape = (IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0];
                        IWFStep mys = (IWFStep)myshape.ZambaObject;
                        WF = (IWorkFlow)WFBusiness.GetWFbyId(mys.WorkId, false);
                        //WF = (IWorkFlow)((IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0]).WFStep.WorkFlow;
                    }
                    if (WF != null)
                    {
                        Diagrama.ClearDiagram();
                        this.loadWF(Diagrama, WF);
                        this.DibujarFlechaWF(Diagrama, WF);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        /// <summary>
        /// Recibe el evento eliminar del contextmenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void eliminar_Click(object sender, EventArgs e)
        {

            try
            {
                Zamba.WFShapes.Tools.ShapeToolStripMenuItem menu =
                                    (Zamba.WFShapes.Tools.ShapeToolStripMenuItem)sender;

                var Diagrama = (Zamba.WFShapes.Win.DiagramControl)menu.Data;

                bool bolSec = false;
                IShape shape1 = null;

                // Si alguno de los shapes esta asignado para eliminar lo guardo para borrarlo luego
                foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                    if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                    {
                        if (shape.IsSelected == true)
                        {
                            if (shape1 == null)
                                shape1 = (IShape)shape;
                            else
                            {
                                int ind1 = Diagrama.Controller.Model.Paintables.IndexOf(shape);
                                int ind2 = Diagrama.Controller.Model.Paintables.IndexOf(shape1);
                                if (ind1 > ind2)
                                    shape1 = (IShape)shape;
                            }
                            bolSec = true;
                        }
                    }


                // Si hay un shape marcado para eliminar lo borro sino la conexion
                if (bolSec == true)
                {
                    if (shape1 != null)
                    {
                        EliminarShape(shape1, Diagrama);
                        IWorkFlow WF = null;
                        if (Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities.Count > 0)
                        {
                            IShape myshape = (IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0];
                            IWFStep mys = (IWFStep)myshape.ZambaObject;
                            WF = (IWorkFlow)WFBusiness.GetWFbyId(mys.WorkId, false);
                            //WF = (IWorkFlow)((IShape)Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities[0]).WFStep.WorkFlow;
                        }
                        if (WF != null)
                        {
                            Diagrama.ClearDiagram();
                            loadWF(Diagrama, WF);
                            DibujarFlechaWF(Diagrama, WF);
                        }
                    }
                }
                else
                {
                    if (Diagrama.SelectedItems.Count > 0)
                        if (Diagrama.SelectedItems[0].EntityName == "Default Connection")
                            EliminarRule(Diagrama);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Recibe el evento cambiar nombre del ContextMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cambiarNombre_Click(object sender, EventArgs e)
        {
            try
            {
                Zamba.WFShapes.Tools.ShapeToolStripMenuItem menu =
                                    (Zamba.WFShapes.Tools.ShapeToolStripMenuItem)sender;

                Zamba.WFShapes.Win.DiagramControl Diagrama = (Zamba.WFShapes.Win.DiagramControl)menu.Data;

                bool bolSec = false;
                IShape shape1 = null;

                // Si alguno de los shapes esta asignado para eliminar lo guardo
                foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                    if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                    {
                        if (shape.IsSelected == true)
                        {
                            if (shape1 == null)
                                shape1 = (IShape)shape;
                            else
                            {
                                int ind1 = Diagrama.Controller.Model.Paintables.IndexOf(shape);
                                int ind2 = Diagrama.Controller.Model.Paintables.IndexOf(shape1);
                                if (ind1 > ind2)
                                    shape1 = (IShape)shape;
                            }
                            bolSec = true;
                        }
                    }


                // Si hay un shape marcado para eliminar lo borro sino la conexion
                if (bolSec == true)
                {
                    if (shape1 != null)
                    {
                        cambiarNombreShape(shape1);
                        Diagrama.Refresh();
                    }
                }
                else
                {
                    if (Diagrama.SelectedItems.Count > 0)
                        if (Diagrama.SelectedItems[0].EntityName == "Default Connection")
                            cambiarNombreRule(Diagrama);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Recibe el elemento cambiar color del ContextMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CambiarColorClick(object sender, EventArgs e)
        {
            try
            {
                Zamba.WFShapes.Tools.ShapeToolStripMenuItem menu =
                                    (Zamba.WFShapes.Tools.ShapeToolStripMenuItem)sender;

                Zamba.WFShapes.Win.DiagramControl Diagrama = (Zamba.WFShapes.Win.DiagramControl)menu.Data;

                IShape shape1 = null;

                //Encuentra el shape que disparo el context menu
                foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                    if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                        if (shape.IsSelected == true)
                        {
                            if (shape1 == null)
                                shape1 = (IShape)shape;
                            else
                            {
                                int ind1 = Diagrama.Controller.Model.Paintables.IndexOf(shape);
                                int ind2 = Diagrama.Controller.Model.Paintables.IndexOf(shape1);
                                if (ind1 > ind2)
                                    shape1 = (IShape)shape;
                            }
                        }

                cambiarColorShape(shape1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// recibe el evento cambiar Icono del shape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CambiarIconoClick(object sender, EventArgs e)
        {
            try
            {
                var menu =
                                    (Zamba.WFShapes.Tools.ShapeToolStripMenuItem)sender;

                Zamba.WFShapes.Win.DiagramControl Diagrama = (Zamba.WFShapes.Win.DiagramControl)menu.Data;

                IShape shape1 = null;

                //Encuentra el shape que disparo el context menu
                foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                    if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                        if (shape.IsSelected == true)
                        {
                            if (shape1 == null)
                                shape1 = (IShape)shape;
                            else
                            {
                                int ind1 = Diagrama.Controller.Model.Paintables.IndexOf(shape);
                                int ind2 = Diagrama.Controller.Model.Paintables.IndexOf(shape1);
                                if (ind1 > ind2)
                                    shape1 = (IShape)shape;
                            }
                        }

                cambiarIconoShape(shape1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Recibe el evento editar del ContextMenu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void editar_Click(object sender, EventArgs e)
        {
            try
            {
                Zamba.WFShapes.Tools.ShapeToolStripMenuItem menu =
                                 (Zamba.WFShapes.Tools.ShapeToolStripMenuItem)sender;

                Zamba.WFShapes.Win.DiagramControl Diagrama = (Zamba.WFShapes.Win.DiagramControl)menu.Data;

                IShape shape1 = null;

                //Encuentra el shape que disparo el context menu
                foreach (IDiagramEntity shape in Diagrama.Controller.Model.Pages[0].DefaultLayer.Entities)
                    if (shape != null && (shape.EntityName == "Class Shape" || shape.EntityName == "Rule Shape"))
                    {
                        if (!(shape.IsSelected != true))
                        {
                            if (shape1 == null)
                                shape1 = (IShape)shape;
                            else
                            {
                                int ind1 = Diagrama.Controller.Model.Paintables.IndexOf(shape);
                                int ind2 = Diagrama.Controller.Model.Paintables.IndexOf(shape1);
                                if (ind1 > ind2)
                                    shape1 = (IShape)shape;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Cambia el nombre de un shape
        /// </summary>
        /// <param name="Diagrama"></param>
        public void cambiarNombreShape(IShape shape)
        {
            try
            {
                if (shape != null)
                {
                    string strname = PreguntarNombre(((ClassShape)shape).Title);
                    if (strname != "")
                    {
                        shape.ZambaObject.Name = strname;
                        if (showTasks == true)
                        {
                            ((ClassShape)shape).Title = shape.ZambaObject.Name + " (" + shape.ZambaObject.TasksCount + ")"; ;
                        }
                        else
                            ((ClassShape)shape).Title = shape.ZambaObject.Name;

                        WfShapesBusiness.UpdateStep(((IWFStep)shape.ZambaObject));
                        _dNameShape(((IWFStep)shape.ZambaObject));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        /// <summary>
        /// Cambia el color de un shape
        /// </summary>
        /// <param name="Diagrama"></param>
        public void cambiarColorShape(IShape shape)
        {
            try
            {
                if (shape != null)
                {
                    Color color = PreguntarColor();
                    if (color != Color.Empty)
                    {
                        shape.ShapeColor = color;
                        shape.ZambaObject.color = color.Name;
                        WfShapesBusiness.UpdateStepColor(shape.ZambaObject.color, shape.ZambaObject.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        /// <summary>
        /// Cambia el icono de un Shape
        /// </summary>
        /// <param name="shape"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void cambiarIconoShape(IShape shape)
        {
            try
            {
                if (shape != null)
                {
                    string newPath = this.traerIcono();
                    if (newPath == null) throw new NotImplementedException();
                    if (newPath.Trim() != "")
                    {
                        var icono = new ClickableIconMaterial();//Image.FromFile(NewPath);
                        icono.Icon = (Bitmap)Image.FromFile(newPath);
                        if (icono != null)
                        {
                            Size size = new Size(18, 18);
                            ClassShape shapeBase = (ClassShape)shape;
                            icono.Transform(new Rectangle(new Point(shapeBase.Rectangle.X + 5, shapeBase.Rectangle.Y + 5), size));//icono.Icon.Size));
                            icono.Gliding = false;

                            //complexShapeBase contiene los hijos del shape
                            ComplexShapeBase complexShapeBase = (ComplexShapeBase)shapeBase;

                            if (complexShapeBase.Children.Count > 2)
                                complexShapeBase.Children.Remove(complexShapeBase.Children[2]);
                            complexShapeBase.Children.Add(icono);

                            // Se registra la imagen del step en zamba
                            if (System.IO.File.Exists(newPath))
                            {
                                string type = shape.ZambaObject.ID.ToString();
                                if (WfShapesBusiness.GetIconsPathString(type) == null)
                                    WfShapesBusiness.SetIconspath(type, newPath);
                                else
                                    WfShapesBusiness.UpdateIconspath(type, newPath);
                            }
                            else
                                throw new System.Exception("No existe la ruta especificada");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        /// <summary>
        /// Cambia el nombre de la regla
        /// </summary>
        /// <param name="Diagrama"></param>
        public void cambiarNombreRule(Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            try
            {
                if (Diagrama.SelectedItems.Count > 0)
                {
                    IConnection conexion = (IConnection)Diagrama.SelectedItems[0];
                    string strName = PreguntarNombre(conexion.label.Text);
                    if (strName != "")
                    {
                        conexion.label.Text = strName;
                        WfShapesBusiness.UpdateRuleNameByID(Diagrama.SelectedItems[0].Id, conexion.label.Text);
                        _dNameConnection(conexion.Id, conexion.label.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }

        }

        /// <summary>
        /// muestra un inputbox y devuelve el nombre
        /// </summary>
        /// <returns></returns>
        public string PreguntarNombre(string name)
        {

            InputBox Dialogo = new InputBox("Asigne un nombre al proceso", "Nombre:", name);

            Dialogo.ShowDialog();
            return Dialogo.Texto;
            Dialogo.Dispose();
        }


        /// <summary>
        /// Muestra los colores en un combo y devuelve el seleccionado
        /// </summary>
        /// <returns></returns>
        public Color PreguntarColor()
        {

            InputBox Dialogo;
            string[] arrayCol = new string[173];

            for (int i = 1; i < 174; i++)
            {
                arrayCol[i - 1] = Color.FromKnownColor((KnownColor)i).Name;
            }

            Dialogo = new InputBox("Elija un Color para la Etapa",
                        "Colores:",
                        arrayCol);

            Dialogo.ShowDialog();

            string nameColor = Dialogo.Texto;

            Color color = Color.Empty;
            for (int i = 0; i < 200; i++)
            {
                if (Color.FromKnownColor((KnownColor)i).Name == nameColor)
                    color = Color.FromKnownColor((KnownColor)i);
            }

            Dialogo.Dispose();

            return color;
        }

        /// <summary>
        /// Permite al usuario elegie el icono y devuelve el Path del mismo
        /// </summary>
        /// <returns></returns>
        public string traerIcono()
        {

            OpenFileDialog OpenDialog = new OpenFileDialog();

            OpenDialog.Title = "SELECCIONAR NUEVA IMAGEN";
            OpenDialog.Multiselect = false;
            OpenDialog.Filter =
                "Imagenes|*.bmp;*.ico;*.jpg;*.gif;*.tif;*.tiff;*.jpeg;*.psp;";
            OpenDialog.CheckFileExists = true;
            OpenDialog.CheckPathExists = true;
            OpenDialog.AddExtension = true;
            OpenDialog.ShowDialog();
            string resultado = OpenDialog.FileName;

            if (resultado != null)
            {
                String newPath = OpenDialog.FileName;
                return newPath;
            }

            return "";
        }

        /// <summary>
        /// Verifica si el punto esta dentro del shape
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="puntero"></param>
        /// <returns></returns>
        public bool verificarPuntoShape(IShape shape, Point puntero)
        {
            /// p1----------p2 
            ///  |          |
            /// p3----------p4
            if (shape != null)
            {
                Point p1 = new Point(shape.Location.X, shape.Location.Y);
                Point p2 = new Point(shape.Location.X + shape.Width, shape.Location.Y);
                Point p3 = new Point(shape.Location.X, shape.Location.Y + shape.Height);
                Point p4 = new Point(shape.Location.X + shape.Width, shape.Location.Y + shape.Height);

                if ((p1.X < puntero.X && p1.Y < puntero.Y) &&
                    (p2.X > puntero.X && p2.Y < puntero.Y) &&
                    (p3.X < puntero.X && p3.Y > puntero.Y) &&
                    (p4.X > puntero.X && p4.Y > puntero.Y))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public void newShape(IWorkFlow WF, Zamba.WFShapes.Win.DiagramControl Diagrama)
        {
            InputBox Dialogo;
            Dialogo = new InputBox("Ingrese el nombre de la Etapa", "Agregar Etapa", "");

            string nameShape;
            Dialogo.ShowDialog();
            nameShape = Dialogo.Texto;
            Dialogo.Dispose();
            if (nameShape != "" && nameShape != null)
            {
                Point p = new Point(Diagrama.Width / 2, Diagrama.Height / 2);

                IWFStep NewStep = new WFStep((int)WF.ID, 0, nameShape, "", "", new Point(p.X, p.Y), 0, 0, 0, false, "", 150, 50, 0, 0);
                NewStep.Location = new Point(p.X, p.Y);

                //Agrego el step en el diagrama, el Workflow y la base
                WfShapesBusiness.InsertStep(NewStep, WF.Name);
                agregarShape(NewStep, Diagrama);
                WF.Steps.Add(NewStep.ID, NewStep);
                //Asigno el estado por defecto
                WFStepState NewState = new WFStepState(ToolsBusiness.GetNewID(IdTypes.WFSTEPSTATE), NewStep.Name,
                                                       "Estado por Defecto", true);
                NewStep.States.Add(NewState);
                WFStepStatesBusiness.AddState((Int32)NewState.ID, "Estado por Defecto", NewStep.Name, 1, NewStep);


                //Llamo al delegado para que agregue el step en el arbol)
                if (this.dAddShape != null)
                    dAddShape(NewStep);

                //todo: levantar evento de agregado de etapa
                //this.Controller.UndoManager.AddUndoCommand(cmd);
                //cmd.Redo();
                //feedbackCursor = null;
            }
        }

        public void newShape(IWFStep WFStep, Zamba.WFShapes.Win.DiagramControl Diagrama, string RuleClass)
        {
            InputBox Dialogo;
            Dialogo = new InputBox("Ingrese el nombre de la Regla", "Agregar Regla", "");

            string nameShape;
            Dialogo.ShowDialog();
            nameShape = Dialogo.Texto;
            Dialogo.Dispose();
            if (nameShape != "" && nameShape != null)
            {
                Point p = new Point(Diagrama.Width / 2, Diagrama.Height / 2);

                IRule NewRule = WFRulesBusiness.GetNewRule(10, nameShape, WFStep, WFStep.ID, RuleClass, TypesofRules.Regla);
                //"", new Point(p.X, p.Y), 5, 0, 0, false, "", 150, 50);

                // NewRule.Location = new Point(p.X, p.Y);


                NewRule = WFRulesBusiness.CreateNewRule(NewRule.Name, NewRule.Name, WFStep.ID, NewRule.RuleType, NewRule.ParentRule.ID, NewRule.ParentType);
                UserBusiness.Rights.SaveAction(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Create, "Se genero la Regla '" + NewRule.Name + "', con ID=" + NewRule.ID.ToString() + " para la Etapa: " + WFStep.Name);


                var r = WFStep.DSRules.WFRules.NewWFRulesRow();
                r.Id = NewRule.ID;
                r.Name = NewRule.Name;
                r.step_Id = WFStep.ID;
                r.Type = (decimal)NewRule.RuleType;
                r.ParentId = NewRule.ParentRule.ID;
                r.ParentType = (decimal)NewRule.ParentType;
                r._Class = NewRule.RuleClass;
                r.Enable = NewRule.Enable ? 1 : 0;
                r.Version = NewRule.Version;
                WFStep.DSRules.WFRules.AddWFRulesRow(r);
                WFStep.DSRules.WFRules.AcceptChanges();

                agregarShape(r, Diagrama, WFStep, p);

                //Llamo al delegado para que agregue el step en el arbol)
                if (this.dAddRuleShape != null)
                    dAddRuleShape(WFStep, NewRule.ID);

                //todo: levantar evento de agregado de etapa
                //this.Controller.UndoManager.AddUndoCommand(cmd);
                //cmd.Redo();
                //feedbackCursor = null;
            }
        }
        #region Eventos
        /// <summary>
        /// Manejo de la variable del delegado AddedShape
        /// </summary>
        public event AddedShape OnAddShape
        {
            add
            {
                this.dAddShape += value;
            }
            remove
            {
                this.dAddShape -= value;
            }
        }

        public event AddedRuleShape OnAddRuleShape
        {
            add
            {
                this.dAddRuleShape += value;
            }
            remove
            {
                this.dAddRuleShape -= value;
            }
        }
        /// <summary>
        /// Manejo de la variable del delegado removedShape
        /// </summary>
        public event RemovedShape OnRemoveShape
        {
            add
            {
                this._dRemoveShape += value;
            }
            remove
            {
                this._dRemoveShape -= value;
            }
        }

        /// <summary>
        /// Manejo de la variable del delegado removedConnection
        /// </summary>
        public event RemovedConnection OnRemoveConnection
        {
            add
            {
                this._dRemoveConnection += value;
            }
            remove
            {
                this._dRemoveConnection -= value;
            }
        }

        public event NameShape OnNameShape
        {
            add
            {
                this._dNameShape += value;
            }
            remove
            {
                this._dNameShape -= value;
            }
        }

        public event NameConnection OnNameConnection
        {
            add
            {
                this._dNameConnection += value;
            }
            remove
            {
                this._dNameConnection -= value;
            }
        }
        #endregion
    }
}
