using System;
using System.Collections.Generic;

using System.Text;
using Zamba.Core;



namespace Zamba.WfRemotingComponent
{  

  public class ZRuleServEngine : MarshalByRefObject , Zamba.Core.IZRemoting
    {

        #region IZRemoting Members

        public List<ITaskResult> ExecuteRule(Int64 ruleId, Int64 stepId, List<ITaskResult> results) 
        {
            WFRulesBusiness WFRS = new WFRulesBusiness();
            return WFRS.ExecuteRule(ruleId, stepId, results);
        }

        public bool  IsRunning()
        {
 	        return true;
        }

        public void  Maximizar()
        {
 	        
        }


        #endregion
      
        #region IZRemoting Members


        public object ExecuteRule(long RuleId, ref List<ITaskResult> lista)
        {
            throw new NotImplementedException();
        }

        #endregion


        public bool Run(string Action, string Argument, System.Collections.Hashtable obs)
        {
            return true;
        }
    }
    

    


  
}
