using System;
using System.Collections;
using System.Collections.Generic;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers
{
   [Serializable()]
    public class GenericExecutionResponse
    {
        public   long ruleId { get; set; }
        public   RulePendingEvents pendigEvent { get; set; }
        public   RuleExecutionResult executionResult { get; set; }
        public   Hashtable Params { get; set; } = new Hashtable();
        public   bool refreshRule { get; set; }
        public List<long> PendingChildRules { get; set; } = new List<long>();

        public List<ITaskResult> results { get; set; } 
        public Hashtable Vars { get; set; } = new Hashtable();

        public GenericExecutionResponse(long ruleId1, RulePendingEvents pendigEvent1, RuleExecutionResult executionResult1, Hashtable params1, bool refreshRule1, List<long> PendingChildRules1, ref System.Collections.Generic.List<ITaskResult> results1)
        {
            this.ruleId = ruleId1;
            this.pendigEvent = pendigEvent1;
            this.executionResult = executionResult1;
            this.Params = params1;
            this.refreshRule = refreshRule1;
            this.PendingChildRules = PendingChildRules1;

            this.results = results1;
        }
    }
}