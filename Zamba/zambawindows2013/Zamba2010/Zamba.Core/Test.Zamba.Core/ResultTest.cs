using Zamba.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Zamba.Core
{
    
    
    /// <summary>
    ///This is a test class for ResultTest and is intended
    ///to contain all ResultTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ResultTest
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
        ///A test for GetIndexById
        ///</summary>
        [TestMethod()]
        public void GetIndexByIdTest()
        {
            IResult target = new Result(); // TODO: Initialize to an appropriate value
            int Id = 0; // TODO: Initialize to an appropriate value
            IIndex actual;
            actual = target.get_GetIndexById(Id);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
