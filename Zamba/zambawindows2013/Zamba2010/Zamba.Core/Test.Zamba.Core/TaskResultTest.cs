using Zamba.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Zamba.Core
{
    
    
    /// <summary>
    ///This is a test class for TaskResultTest and is intended
    ///to contain all TaskResultTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TaskResultTest
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
        ///A test for IndexByName
        ///</summary>
        [TestMethod()]
        public void IndexByNameTest()
        {
            ITaskResult target = new TaskResult(); // TODO: Initialize to an appropriate value
            string IndexName = string.Empty; // TODO: Initialize to an appropriate value
            IIndex expected = null; // TODO: Initialize to an appropriate value
            IIndex actual;
            actual = target.IndexByName(IndexName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
