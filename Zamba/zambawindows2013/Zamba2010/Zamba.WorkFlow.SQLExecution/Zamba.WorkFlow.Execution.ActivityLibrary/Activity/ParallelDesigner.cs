using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Defines the designer for the ParallelIf activity
    /// </summary>
    //[ActivityDesignerTheme(typeof(IfTheme))]
    public class ParallelDesigner : ParallelActivityDesigner
    {
        /// <summary>
        /// Si se pueden agregar nuevos branchs
        /// </summary>
        /// <param name="insertLocation"></param>
        /// <param name="activitiesToInsert"></param>
        /// <returns></returns>
        public override bool CanInsertActivities(HitTestInfo insertLocation, ReadOnlyCollection<System.Workflow.ComponentModel.Activity> activitiesToInsert)
        {
            return false;
        }

        /// <summary>
        /// Si se pueden mover las actividades
        /// </summary>
        /// <param name="moveLocation"></param>
        /// <param name="activitiesToMove"></param>
        /// <returns></returns>
        public override bool CanMoveActivities(HitTestInfo moveLocation, ReadOnlyCollection<System.Workflow.ComponentModel.Activity> activitiesToMove)
        {
            return true;
        }

        /// <summary>
        /// Si se pueden borrar las actividades
        /// </summary>
        /// <param name="activitiesToRemove"></param>
        /// <returns></returns>
        public override bool CanRemoveActivities(ReadOnlyCollection<System.Workflow.ComponentModel.Activity> activitiesToRemove)
        {
            return true;
        }

        private void OnAddBranch(object sender, EventArgs e)
        {
            CompositeActivity activity1 = this.OnCreateNewBranch();
            CompositeActivity activity2 = base.Activity as CompositeActivity;
            if ((activity2 != null) && (activity1 != null))
            {
                int num1 = this.ContainedDesigners.Count;
                System.Workflow.ComponentModel.Activity[] activityArray1 = new System.Workflow.ComponentModel.Activity[] { activity1 };
                CompositeActivityDesigner.InsertActivities(this, new ConnectorHitTestInfo(this, HitTestLocations.Designer, activity2.Activities.Count), new List<System.Workflow.ComponentModel.Activity>(activityArray1).AsReadOnly(), string.Format("Adding branch {0}", activity1.GetType().Name));
                if ((this.ContainedDesigners.Count > num1) && (this.ContainedDesigners.Count > 0))
                {
                    this.ContainedDesigners[this.ContainedDesigners.Count - 1].EnsureVisible();
                }
            }
        }

        protected override void OnExecuteDesignerAction(DesignerAction designerAction)
        {
            // OK, this is a bit pants...
            if (designerAction.UserData.Contains("LESS_THAN_TWO"))
            {
                CompositeActivity parent = this.Activity as CompositeActivity;

                if (null != parent)
                {
                    while ( parent.Activities.Count < 2 )
                        OnAddBranch(this, EventArgs.Empty);
                }
            }
            else
                base.OnExecuteDesignerAction(designerAction);
        }
    }
}