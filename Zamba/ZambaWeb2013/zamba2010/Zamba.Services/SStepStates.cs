using System;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;

namespace Zamba.Services
{
    public class SStepStates:IService
    {
        #region IService Members

        public ServicesTypes ServiceType()
        {
            return ServicesTypes.StepStates; 
        }

        #endregion

        public SStepStates(ref IUser currentUser)
        {

        }

        private SStepStates()
        { }

        public string GetStateName(Int64 stateId)
        {
            return WFStepStatesComponent.GetStateName(stateId);
        }        
    }
}
