using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.ViewModels
{
    public class LoadTreeVM
    {
        public LoadTreeVM(string WfName, List<StepVM> steps)
        {
            this.WfName = WfName;
            Steps = steps;
        }
        public string WfName { get; set; }
        public List<StepVM> Steps { get; set; }
    }
    public class StepVM
    {
        public StepVM(string stepName, int taskCount)
        {
            StepName = stepName;
            TaskCount = TaskCount;
        }
        public string StepName { get; set; }
        public int TaskCount { get; set; }
    }
}