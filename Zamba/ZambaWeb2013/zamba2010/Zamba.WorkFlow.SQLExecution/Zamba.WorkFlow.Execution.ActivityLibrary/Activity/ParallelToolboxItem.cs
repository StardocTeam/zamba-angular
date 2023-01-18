using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace Zamba.WFActivity.Xoml
{
    /// <summary>
    /// Define las activities que van a estar dentro de una activity
    /// </summary>
    [Serializable]
    [ToolboxItem(typeof(ParallelToolboxItem))]
    internal class ParallelToolboxItem : ActivityToolboxItem
    {
        public ParallelToolboxItem(Type type)
            : base(type)
        {
        }

        private ParallelToolboxItem(SerializationInfo info, StreamingContext context)
        {
            this.Deserialize(info, context);
        }

        protected override IComponent[] CreateComponentsCore(IDesignerHost host)
        {
            CompositeActivity activity = new Parallel();
            ParallelBranch branch1 = new ParallelBranch(true);
            ParallelBranch branch2 = new ParallelBranch(false);

            activity.Activities.Add(branch1);
            activity.Activities.Add(branch2);

            return new IComponent[] { activity };
        }
    }
}