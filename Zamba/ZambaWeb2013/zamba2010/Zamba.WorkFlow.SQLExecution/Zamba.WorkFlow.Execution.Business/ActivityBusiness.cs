using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting;
using System.Workflow.ComponentModel;
using Zamba.Core;
using Zamba.Core.Enumerators;
using Zamba.Data;
using Zamba.WFActivity.Xoml;
using Zamba.WorkFlow.Execution.WorkFlow;
using Zamba.WorkFlow.Factories;

namespace Zamba.WorkFlow.Business
{
    /// <summary>
    /// Clase que maneja la logica de Xoml
    /// <history>
    /// Marcelo created
    /// Marcelo modified 23/06/2008
    /// </history>
    /// </summary>
    public class ActivityBusiness
    {
        /// <summary>
        /// Return a Dataset with all the Activities from the Workflow
        /// </summary>
        /// <_param name="id">Id of the Workflow</_param>
        /// <returns>Dataset</returns>
        public static DataSet GetStepActivities(Int64 id)
        {
            DataSet ds = ActivityFactory.GetWFActivities(id);
            return ds;
        }

        /// <summary>
        /// Update the Activities on the database
        /// </summary>
        /// <_param name="id">Activity ID</_param>
        /// <_param name="Name">Activity Name</_param>
        /// <_param name="typeID">Activity Type</_param>
        /// <_param name="step_id">Step ID</_param>
        /// <_param name="ownerID">Owner Activivy ID</_param>
        public static void UpdateRule(Int64 id, string name, String @class, Int64 stepId, Int64 parentId,
                                      Int32 parentType, Int32 version, Boolean enable, Int32 type)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (@class == null) throw new ArgumentNullException("class");
            ActivityFactory.UpdateRule(id, name, @class, stepId, parentId, parentType, version, enable, type);
        }

        /// <summary>
        /// Insert the Activities on the database
        /// </summary>
        /// <_param name="Ac">Activity</_param>
        /// <_param name="id">Activity ID</_param>
        /// <_param name="step_id">Step ID</_param>
        /// <_param name="ParentID">Owner Activivy ID</_param>
        public static void InsertRule(Int64 Id, String Name, String Class, Int64 step_id, Int64 ParentID,
                                      Int32 ParentType, Int32 Version, Boolean Enable, Int32 Type)
        {
            try
            {
                ActivityFactory.InsertRule(Id, Name, Class, step_id, ParentID, ParentType, Version, Enable, Type);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Actualiza el parametro especificado en la base
        /// </summary>
        /// <_param name="ItemPos">Posicion del parametro</_param>
        /// <_param name="RuleId">Id de la regla</_param>
        /// <_param name="Value">valor del parametro</_param>
        public static void UpdateParamItem(Int32 ItemPos, Int64 RuleId, String Value)
        {
            ActivityFactory.UpdateParamItem(ItemPos, RuleId, Value);
        }

        /// <summary>
        /// Delete all activities from a workflow
        /// </summary>
        /// <_param name="ID">Workflow ID</_param>
        public static void DeleteStepActivities(Int32 ID)
        {
            ActivityFactory.DeleteStepActivities(ID);
        }

        /// <summary>
        /// Get the lastID insert on the database
        /// </summary>
        /// <returns>LastID</returns>
        public static Int32 GetLastID()
        {
            return ActivityFactory.GetLastID();
        }

        /// <summary>
        /// Borra la regla
        /// </summary>
        /// <_param name="RuleId"></_param>
        public static void removeRule(Int64 RuleId)
        {
            WFRulesFactory.DeleteRule(RuleId);
        }

        /// <summary>
        /// Carga el workflow
        /// </summary>
        /// <_param name="workflow">workflow</_param>
        public static void GetBasicWorkflow(WorkFlowModel workflow, Int64 stepId)
        {
            try
            {
                Activity validacionEntrada =
                    getBasicTypeofRule("Validacion Entrada", TypesofRules.ValidacionEntrada, stepId);

                Activity entrada =
                    getBasicTypeofRule("Entrada", TypesofRules.Entrada, stepId);

                workflow.Activities.Add(validacionEntrada);
                workflow.Activities.Add(entrada);

                var regionEjecucion = (CompositeActivity) getActivity("Parallel", "Region Ejecucion");
                ((IResultActivity) regionEjecucion).RuleType = TypesofRules.ValidacionEntrada;

                Activity eventos =
                    getBasicTypeofRule("Eventos Tarea", TypesofRules.Eventos, stepId);

                Activity regionAccionUsuario =
                    getBasicTypeofRule("Acciones Usuario", TypesofRules.AccionUsuario, stepId);

                //CompositeActivity regionAccionUsuario = (CompositeActivity)getActivity("Parallel", "Region Acciones Usuario");
                //getUserAccions(regionAccionUsuario, (Int32)Zamba.Core.TypesofRules.AccionUsuario, dt, stepId);

                Activity actualizacion =
                    getBasicTypeofRule("Actualizacion", TypesofRules.Actualizacion, stepId);

                Activity planificada =
                    getBasicTypeofRule("Planificada", TypesofRules.Planificada, stepId);

                regionEjecucion.Activities.Add(eventos);
                regionEjecucion.Activities.Add(regionAccionUsuario);
                regionEjecucion.Activities.Add(actualizacion);
                regionEjecucion.Activities.Add(planificada);
                workflow.Activities.Add(regionEjecucion);

                Activity validacionSalida =
                    getBasicTypeofRule("Validacion Salida", TypesofRules.ValidacionSalida, stepId);

                Activity salida =
                    getBasicTypeofRule("Salida", TypesofRules.Salida, stepId);

                workflow.Activities.Add(validacionSalida);
                workflow.Activities.Add(salida);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Devuelve una instancia de la regla
        /// </summary>
        /// <_param name="type">tipo de la regla</_param>
        /// <_param name="name">nombre de la regla</_param>
        /// <returns></returns>
        private static Activity getActivity(String type, String name)
        {
            try
            {
                String Assembly = "Zamba.WFActivity.Xoml";
                ObjectHandle handle
                    = Activator.CreateInstance(Assembly, Assembly + "." + type);
                var rule = (Activity) handle.Unwrap();
                handle = null;
                rule.Name = name;
                return rule;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        /// <summary>
        /// Devuelve una instancia de la regla
        /// </summary>
        /// <_param name="type">tipo de la regla</_param>
        /// <_param name="name">nombre de la regla</_param>
        /// <returns></returns>
        private static Activity getActivity(String type)
        {
            try
            {
                String Assembly = "Zamba.WFActivity.Xoml";

                ObjectHandle handle
                    = Activator.CreateInstance(Assembly, Assembly + "." + type);
                var rule = (Activity) handle.Unwrap();
                handle = null;
                return rule;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        /// <summary>
        /// Devuelve una activity para los tipos basicos
        /// </summary>
        /// <_param name="type"></_param>
        /// <_param name="typeOf"></_param>
        /// <_param name="stepId"></_param>
        /// <returns></returns>
        private static Activity getBasicTypeofRule(string type, TypesofRules typeOf, Int64 stepId)
        {
            Activity activity = getActivity("TypeofRule", type);
            ((IResultActivity) activity).RuleType = typeOf;
            ((IResultActivity) activity).WFStepId = stepId;
            return activity;
        }

        /// <summary>
        /// Create the Activity and it's Childs
        /// </summary>
        /// <_param name="name">Name of the Activity</_param>
        /// <_param name="strType">Type of the Activity</_param>
        /// <_param name="ID">Id of the Activity</_param>
        /// <_param name="ds">Dataset que contiene los datos de todas las actividades</_param>
        /// <_param name="wf">Workflowmodel donde se van a cargar las actividades</_param> 
        /// <returns></returns>
        public static void getRegionRules(Int32 ID, DataView dv, Int64 stepId, CompositeActivity padre)
        {
            try
            {
                List<Int64> ruleInstancesList = new List<Int64>();
                IRule rule = WFRulesBusiness.GetInstanceRuleById(ID, stepId,true);
                ruleInstancesList.Clear();
                ruleInstancesList = null;

                if (rule != null)
                {
                    IResultActivity activity = null;
                    if (rule.RuleClass.ToUpper().StartsWith("DO"))
                        activity = new ZRule();
                    else if (string.Compare(rule.RuleClass.ToUpper(), "IFBRANCH") == 0)
                        activity = (IResultActivity) getActivity("ZCompositeRule");
                    else
                        activity = (IResultActivity) getActivity("ZParallelRule");
                    //Asigno las propiedades
                    activity.ruleId = ID;
                    activity.RuleType = TypesofRules.Regla; // RuleType;
                    activity.RuleClass = rule.RuleClass;
                    activity.WFStepId = rule.WFStepId;
                    //activity.rule.Version = Version;

                    //activity.rule.WFStepId = stepId;

                    if (String.Compare(padre.Name, "Accion de Usuario") == 0)
                    {
                        WFBusiness WFB = new WFBusiness();
                        padre.Name = WFB.GetUserActionName(rule.ID,rule.WFStepId,rule.Name,false).ToUpper();
                        WFB = null;
                    }

                    padre.Activities.Add((Activity) activity);

                    //Obtengo los parametros de la regla
                    //setParams(activity);

                    activity.Name = rule.Name + " (" + ID + ")";

                    //Cargo las hijas
                    dv.RowFilter = "ParentID=" + activity.ruleId;

                    if (dv.ToTable().Rows.Count > 1)
                    {
                        if (!(activity is CompositeActivity))
                            //Instancio una parallel 
                            getParallelRules(dv, stepId, padre, false);
                        else
                            padre = (CompositeActivity) activity;
                    }

                    if (activity is CompositeActivity)
                        padre = (CompositeActivity) activity;
                    //Obtengo las reglas
                    foreach (DataRow r in dv.ToTable().Rows)
                    {
                        //Set the region rules in the wf
                        getRegionRules(Int32.Parse(r["ID"].ToString()), dv, stepId, padre);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Crea la actividad paralela y sus hijas
        /// </summary>
        /// <_param name="name">Name of the Activity</_param>
        /// <_param name="strType">Type of the Activity</_param>
        /// <_param name="ID">Id of the Activity</_param>
        /// <_param name="ds">Dataset que contiene los datos de todas las actividades</_param>
        /// <_param name="wf">Workflowmodel donde se van a cargar las actividades</_param> 
        /// <param name="isUserAction">Si es accion de usuario, le cargo los nombres correspondientes</param>
        /// <returns></returns>
        public static void getParallelRules(DataView dv, Int64 stepId, CompositeActivity padre, Boolean isUserAction)
        {
            try
            {
                Activity activity = getActivity("Parallel");
                if (activity != null)
                {
                    ((IResultActivity) activity).WFStepId = stepId;
                    if (isUserAction)
                    {
                        activity.Name = "Acciones de Usuario";
                        ((IResultActivity) activity).RuleType = TypesofRules.AccionUsuario;
                    }
                    else
                        ((IResultActivity) activity).RuleType = TypesofRules.Regla;

                    padre.Activities.Add(activity);

                    foreach (DataRow r in dv.ToTable().Rows)
                    {
                        Activity activityIf = getActivity("ParallelBranch");
                        ((IResultActivity) activityIf).WFStepId = stepId;
                        ((IResultActivity) activityIf).RuleType = TypesofRules.AccionUsuario;
                        ((CompositeActivity) activity).Activities.Add(activityIf);

                        activityIf.Name = "Accion de Usuario";

                        getRegionRules(Int32.Parse(r["ID"].ToString()), dv, stepId, (CompositeActivity) activityIf);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        /// <summary>
        /// Returns an Array with the parameters of the rule
        /// </summary>
        /// <_param name="id">Id of the Rule</_param>
        /// <returns>Object[]</returns>
        public static Object[] GetRuleParams(Int64 id)
        {
            List<object> paramList = new List<object>();
            try
            {
                var ds = ActivityFactory.GetRuleParams(id);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                paramList.Add(row[0].ToString());
                }
                return paramList.ToArray();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        /// <summary>
        /// Carga el workflow
        /// </summary>
        /// <_param name="step_id">Id del step</_param>
        /// <_param name="workflow">workflow</_param>
        public static WorkFlowModel GetWorkflow(Int64 stepId, Int64 ParentType)
        {
            var wf = new WorkFlowModel();
            wf.WFStepId = stepId;
            wf.RuleType = ((TypesofRules) ParentType);
            try
            {
                //todo hacer un metodo que devuelva solo los del parent buscado
                DataSet ds = GetStepActivities(stepId);

                if (ds != null)
                {
                    var dv = new DataView(ds.Tables[0]);
                    dv.RowFilter = "ParentType=" + ParentType;
                    if (dv.ToTable().Rows.Count > 1)
                    {
                        //Instancio una parallel 
                        if (ParentType == 5)
                            getParallelRules(dv, stepId, wf, true);
                        else
                            getParallelRules(dv, stepId, wf, false);
                    }
                    //Obtengo las reglas
                    foreach (DataRow r in dv.ToTable().Rows)
                    {
                        //Set the region rules in the wf
                        getRegionRules(Int32.Parse(r["ID"].ToString()), dv, stepId, wf);
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return wf;
        }
    }
}