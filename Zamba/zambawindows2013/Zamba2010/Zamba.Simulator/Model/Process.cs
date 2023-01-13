using Zamba.Core;

namespace Zamba.Simulator.Model
{
    public class Process
    {
        public long RuleId { get; set; }
        public string Name { get; set; }

        public Process(long ruleId)
        {
            RuleId = ruleId;
            Name = WFRulesBusiness.GetRuleNameById(ruleId);

            if (string.IsNullOrEmpty(Name))
            {
                Name = "Error: Verifique la existencia del proceso.";
            }
        }
    }
}
