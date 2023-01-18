using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel;
using Zamba.Core;
using System.Workflow.Activities;
using Zamba.Core.Enumerators;
using Zamba.WFActivity.Xoml;

namespace Zamba.WorkFlow.Execution.WorkFlow
{
    /// <summary>
    /// SequentialWorkFlow with a taskResult[] property
    /// </summary>
    public class WorkFlowModel : System.Workflow.Activities.SequentialWorkflowActivity
    {
        List<Zamba.Core.ITaskResult> results;
        public List<Zamba.Core.ITaskResult> Results
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                foreach (IResultActivity activity in this.EnabledActivities)
                {
                    activity.Results = value;
                }
            }
        }
       
        private Int64 id;
        /// <summary>
        /// Step_id
        /// </summary>
        public Int64 ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private Int64 wfstepid;
        public Int64 WFStepId
        {
            get
            {
                return wfstepid;
            }
            set
            {
                wfstepid = value;
            }
        }

        private TypesofRules ruletype;
        public TypesofRules RuleType
        {
            get
            {
                return ruletype;
            }
            set
            {
                ruletype = value;
            }
        }

        /// <summary>
        /// Guarda los ids de las reglas borradas para eliminarlas luego en la base
        /// </summary>
        public List<Int64> DeletedIds = new List<Int64>();

        protected override void OnActivityChangeAdd(ActivityExecutionContext executionContext, Activity addedActivity)
        {
            base.OnActivityChangeAdd(executionContext, addedActivity);

        }
        protected override void OnListChanged(ActivityCollectionChangeEventArgs e)
        {
            base.OnListChanged(e);
            for (int i = 0; i <= e.AddedItems.Count - 1; i++)
            {
                //e.AddedItems[i].Parent = this;
                ((IResultActivity)e.AddedItems[i]).OnItemListChanging += new ItemListChanging(this.ItemListChange);
            }

            ItemListChange(e);
        }
        
        /// <summary>
        /// Entra el evento listchange de los childs
        /// </summary>
        /// <param name="e"></param>
        private void ItemListChange(ActivityCollectionChangeEventArgs e)
        {
            foreach (Activity activity in e.AddedItems)
            {
                if (activity is IResultActivity)
                if (((IResultActivity)activity).RuleType == 0)
                {
                    InputBox input = new InputBox("Ingrese el nombre de la Actividad", "Nombre", ((IResultActivity)activity).RuleClass);
                    input.ShowDialog();
                    if (input.Texto != string.Empty)
                    {
                            ZRule parentactivity = null;
                        IRule rule = null;

                            if (e.Index > 0)
                            {
                                parentactivity = ((ZRule)((CompositeActivity)e.Owner).Activities[e.Index - 1]);
                                rule = Zamba.Core.WFRulesBusiness.CreateNewRule(((IResultActivity)activity).RuleClass, input.Texto, this.wfstepid, TypesofRules.Regla, parentactivity.ruleId, ((IResultActivity)activity).RuleType);

                            }
                            else if (activity.Parent is WorkFlowModel)
                            rule = Zamba.Core.WFRulesBusiness.CreateNewRule(((IResultActivity)activity).RuleClass, input.Texto, this.wfstepid, TypesofRules.Regla, 0, this.RuleType);
                       else if (activity.Parent.ToString().Contains("Xoml.Parallel")==false)
                        {
                            rule = Zamba.Core.WFRulesBusiness.CreateNewRule(((IResultActivity)activity).RuleClass, input.Texto, this.wfstepid, TypesofRules.Regla, ((IResultActivity)activity.Parent.Activities[activity.Parent.Activities.Count - 2]).ruleId, ((IResultActivity)activity.Parent.Activities[activity.Parent.Activities.Count - 2]).RuleType);
                        }
                        else if (activity.Parent.ToString().Contains("Xoml.ParallelBranch") && activity.Parent.Activities.Count==1)
                            rule = Zamba.Core.WFRulesBusiness.CreateNewRule(((IResultActivity)activity).RuleClass, input.Texto, this.wfstepid, TypesofRules.Regla, 0, TypesofRules.AccionUsuario);
                        else
                            rule = Zamba.Core.WFRulesBusiness.CreateNewRule(((IResultActivity)activity).RuleClass, input.Texto, this.wfstepid, TypesofRules.Regla, ((IResultActivity)activity.Parent.Activities[activity.Parent.Activities.Count - 2]).ruleId, ((IResultActivity)activity.Parent.Activities[activity.Parent.Activities.Count - 2]).RuleType);
                        

                        if (rule != null)
                        {
                            ((IResultActivity)activity).ruleId = rule.ID;
                            ((IResultActivity)activity).Name = rule.Name + " (" + ((IResultActivity)activity).ruleId + ")";
                            ((IResultActivity)activity).RuleType = rule.RuleType;
                            ((IResultActivity)activity).RuleClass = rule.RuleClass;
                            ((IResultActivity)activity).WFStepId = rule.WFStepId;
                        }
                    }
                    else
                        activity.Parent.Activities.Remove(activity);
                    //if (activity is IfBranch == false)
                    //{
                        //CompositeActivity parentActivity = (CompositeActivity)e.Owner;
                    //if (parentActivity != null){
                    //parentActivity.Activities.Remove((Activity)activity);
                    //ParallelActivity parallelActivity = new ParallelActivity(activity.Name);
                    //parallelActivity.Activities.Add((Activity)activity);
                    //parentActivity.Activities.Add(parallelActivity);
               
                    //}
                //}
                }
            }
            foreach (Activity activity in e.RemovedItems)
            {
                if (activity is IResultActivity)
                    removeRule((IResultActivity)activity);
            }
        }

        /// <summary>
        /// Agrega el id a la lista de activities eliminados
        /// </summary>
        /// <param name="activity"></param>
        private void removeRule(Core.IResultActivity activity)
        {
            if (activity.GetType().GetProperty("Activities") != null)
                {
                    foreach (IResultActivity child in ((CompositeActivity)activity).Activities)
                    {
                        removeRule(child); ;
                    }
                }
            DeletedIds.Add(activity.ruleId);
        }
  }
}