using System;
using System.Collections.Generic;
using Zamba.Core;

namespace Zamba.Services
{
    public class Steps : IService
    {
        #region Singleton
        private static Steps _step = null;

        private Steps()
        {
        }

        public static IService GetInstance()
        {
            if (_step == null)
                _step = new Steps();

            return _step;
        }
        #endregion
        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Steps;
        }
        #region Get
        /// <summary>
        /// Get the list of IWFStep from some workflows.
        /// </summary>
        /// <param name="workflowIds"></param>
        /// <returns></returns>
        public static List<IWFStep> GetStepsByWorkflows(List<long> workflowIds)
        {
            return WFStepBusiness.GetStepsByWorkflows(workflowIds);
        }

        /// <summary>
        /// Returns all steps from a Workflow
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public static List<IWFStep> GetStepsByWorkflow(Int64 workflowId)
        {
            return WFStepBusiness.GetStepsByWorkflow(workflowId);
        }

        /// <summary>
        /// Returns Steps by its ids
        /// </summary>
        /// <param name="stepIds"></param>
        /// <returns></returns>
        public static List<IWFStep> GetSteps(List<Int64> stepIds)
        {
            return WFStepBusiness.GetSteps(stepIds);
        }

        /// <summary>
        /// Return a Step by its ID
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public static IWFStep GetStep(Int64 stepId)
        {
            return WFStepBusiness.GetStepById(stepId,false);
        }

        public static List<IWFStep> GetAllSteps(Int64 workflowid)
        {
            List<IWFStep> steplist = new List<IWFStep>();
            steplist = WFStepBusiness.GetStepsByWorkflow(workflowid);
            return steplist;
        }
        #endregion
        #region ABM
        /// <summary>
        /// Removes a Step
        /// </summary>
        /// <param name="step"></param>
        public static void RemoveStep(ref IWFStep step)
        {
            WFStepBusiness.DelStep(ref step);
        }

        /// <summary>
        /// Removes a Step
        /// </summary>
        /// <param name="stepId"></param>
        public static void RemoveStep(Int64 stepId)
        {
            IWFStep step = null;

            step = GetStep(stepId);
            RemoveStep(ref step);

            step = null;
        }

        /// <summary>
        /// Updates a Step
        /// </summary>
        /// <param name="step"></param>
        public static void UpdateStep(ref IWFStep step)
        {
            WFStepBusiness.UpdateStep(ref step);
        }

        /// <summary>
        /// Inserts a Step to a Workflow
        /// </summary>
        /// <param name="step"></param>
        /// <param name="workflow"></param>
        public static void InsertStep(IWFStep step, IWorkFlow workflow)
        {
            WFStepBusiness.AddStep(step, workflow);
        }

        public static void SetTipedDsBySteps(Int64 stepId)
        {
            //Andres - no compilaba 
            //Zamba.WFBusiness.WFStepBusiness.fill
        }
        #endregion
        #endregion
        ////<summary>
        ////Return the average consumed minutes from the tasks betweenCheckIn and CheckOut in one step.
        ////</summary>
        ////<param name="stepId"></param>
        ////<returns></returns>
        public static Int32 GetAverageTaskTime(Int64 stepId)
        {
            Int32 AverageMinutes = 0;

            return AverageMinutes;
        }

        /// <summary>
        /// Sets the initial step of a Workflow.
        /// </summary>
        /// <param name="step"></param>
        public static void SetInitialStep(IEditStepNode step)
        {
            WFBusiness.SetInitialStep(step);
        }
    }
}