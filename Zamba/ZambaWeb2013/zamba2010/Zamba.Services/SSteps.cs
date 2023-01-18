using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Core.WF.WF;

namespace Zamba.Services
{
    public class SSteps : IService
    {

        private Zamba.Core.WFStepBusiness WFStepBusiness;
        private WFBusiness WFBusiness;
        private WFStepStatesComponent WFStepStatesComponent;
             
        public SSteps()
        {
            this.WFStepBusiness = new WFStepBusiness();
            this.WFBusiness = new WFBusiness();
            this.WFStepStatesComponent = new WFStepStatesComponent();
        }
        
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
        public List<IWFStep> GetStepsByWorkflows(List<long> workflowIds)
        {
            return WFStepBusiness.GetStepsByWorkflows(workflowIds);
        }

        /// <summary>
        /// Returns all steps from a Workflow
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public List<IWFStep> GetStepsByWorkflow(Int64 workflowId)
        {
            return WFStepBusiness.GetStepsByWorkflow(workflowId);
        }

        /// <summary>
        /// Returns Steps by its ids
        /// </summary>
        /// <param name="stepIds"></param>
        /// <returns></returns>
        public List<IWFStep> GetSteps(List<Int64> stepIds)
        {
            return WFStepBusiness.GetSteps(stepIds);
        }

        /// <summary>
        /// Return a Step by its ID
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IWFStep GetStep(Int64 stepId)
        {
            return WFStepBusiness.GetStepById(stepId);
        }

        public List<IWFStep> GetAllSteps(Int64 workflowid)
        {
            List<IWFStep> steplist = new List<IWFStep>();
            steplist = WFStepBusiness.GetStepsByWorkflow(workflowid);
            return steplist;
        }
        #endregion

        #endregion

        ////<summary>
        ////Return the average consumed minutes from the tasks betweenCheckIn and CheckOut in one step.
        ////</summary>
        ////<param name="stepId"></param>
        ////<returns></returns>
        public Int32 GetAverageTaskTime(Int64 stepId)
        {
            Int32 AverageMinutes = 0;
            return AverageMinutes;
        }

        /// <summary>
        /// Sets the initial step of a Workflow.
        /// </summary>
        /// <param name="step"></param>
        public void SetInitialStep(IEditStepNode step)
        {
            WFBusiness.SetInitialStep(step);
        }

        public string GetStepNameById(Int64 StepId)
        {
            return WFStepBusiness.GetStepNameById(StepId);        
        }

        public IWFStepState GetStepStateById(Int32 StateId) 
        {
            return WFStepStatesComponent.GetStepStateById(StateId);
        }

        public List<IZBaseCore> GetStepUsersIdsAndNames(long WFStepId)
        {
            return WFStepBusiness.GetStepUsersIdsAndNames( WFStepId, false);
        }

    }
}