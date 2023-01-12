using Zamba.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Zamba.Bussines
{
    
    
    /// <summary>
    ///This is a test class for WFRulesBussinesTest and is intended
    ///to contain all WFRulesBussinesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WFRulesBussinesTest
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
        ///A test for ReconocerRuleValueFunctions
        ///</summary>
        [TestMethod()]
        public void ReconocerRuleValueFunctionsTest()
        {
            System.DateTime datetimeNow = System.DateTime.Now;
            string codedText = "se agrega un dia a la fecha actual <ZF>ZAddDays," + datetimeNow  + ",1</ZF> ";
            codedText += " se agrega un mes a la fecha actual <ZF>ZAddMonths," + datetimeNow + ",1</ZF> ";
            codedText += " se agrega un año a la fecha actual <ZF>ZAddYears," + datetimeNow  + ",1</ZF> ";
            codedText += " se suma 3 + 2 + 6 <ZF>ZSum,3,2,6</ZF> ";
            codedText += " se calcula promedio entre 30, 50, y 150 <ZF>ZAverage,30,50,150</ZF> ";
            codedText += " se calcula maximo entre 30, 150, y 50 <ZF>ZMax,30,150,50</ZF> ";
            codedText += " se calcula minimo entre 30, 150, y 50 <ZF>ZMin,30,150,50</ZF> ";
            
            string codedTextExpected = string.Empty;
            WFRulesBussines.ReconocerRuleValueFunctions(ref codedText);

        }
    }
}
