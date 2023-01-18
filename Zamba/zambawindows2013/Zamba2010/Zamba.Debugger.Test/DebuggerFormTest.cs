using Zamba.Debugger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.Core;

namespace Zamba.Debugger.Test
{
    
    
    /// <summary>
    ///This is a test class for DebuggerFormTest and is intended
    ///to contain all DebuggerFormTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DebuggerFormTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for LoadInfo
        ///</summary>
        [TestMethod()]
        public void LoadInfoTest()
        {
            System.Windows.Forms.Form F = new System.Windows.Forms.Form();

            DebuggerForm target = new DebuggerForm(); // TODO: Initialize to an appropriate value
            target.Show();            
            long WFId = 26002; // TODO: Initialize to an appropriate value
            long Stepid = 26004; // TODO: Initialize to an appropriate value
            long TaskId = 0; // TODO: Initialize to an appropriate value
            IRule Rule = null; // TODO: Initialize to an appropriate value
            
            long RuleId = 27264;
            Rule = WFRulesBussines.GetInstanceRuleById(RuleId,WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
            target.RuleExecuted(WFId, Stepid, TaskId, Rule);

 RuleId = 27265;
            Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
            target.RuleExecuted(WFId, Stepid, TaskId, Rule);
            
 RuleId = 27266;
 Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
 target.RuleExecuted(WFId, Stepid, TaskId, Rule);
            
  RuleId = 27267;
  Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
  target.RuleExecuted(WFId, Stepid, TaskId, Rule);
          
   RuleId = 27268;
   Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
   target.RuleExecuted(WFId, Stepid, TaskId, Rule);
           
    RuleId = 27269;
    Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
    target.RuleExecuted(WFId, Stepid, TaskId, Rule);
           
    RuleId = 27270;
    Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
    target.RuleExecuted(WFId, Stepid, TaskId, Rule);
           
     RuleId = 27271;
     Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
     target.RuleExecuted(WFId, Stepid, TaskId, Rule);
           
     RuleId = 27272;
     Rule = WFRulesBussines.GetInstanceRuleById(RuleId, WFRulesBussines.GetWFStepIdbyRuleID(RuleId));
     target.RuleExecuted(WFId, Stepid, TaskId, Rule);

            Assert.IsTrue(true);
        }
    }
}
