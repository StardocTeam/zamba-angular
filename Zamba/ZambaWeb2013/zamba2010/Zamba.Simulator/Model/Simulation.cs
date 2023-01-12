using System.Collections.Generic;

namespace Zamba.Simulator.Model
{
    public class Simulation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastUpdate { get; set; }//datetime
        public string LastExecution { get; set; }//datetime
        public string LastResult { get; set; }//nuevo enum
        public bool IsAutomatic { get; set; }
        public List<TestCase> TestCases { get; set; }

        public Simulation()
        {
            TestCases = new List<TestCase>();
        }

        public Simulation(long id, string name, string description, string lastUpdate, string lastExecution, string lastResult, bool isAutomatic)
        {
            Id = id;
            Name = name;
            Description = description;
            LastUpdate = lastUpdate;
            LastExecution = lastExecution;
            LastResult = lastResult;
            IsAutomatic = isAutomatic;
            TestCases = new List<TestCase>();
        }
    }
}
