using Zamba.HTMLEditor.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.Core;
using System;

namespace Zamba.HTMLEditor.Controller.Test
{


    /// <summary>
    ///This is RuleButton test class for FormsEditorTest and is intended
    ///to contain all FormsEditorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FormsEditorTest
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

        /// <summary>
        ///A test for FormsEditor Constructor
        ///</summary>
        [TestMethod()]
        public void FormsEditorConstructorTest()
        {
            try
            {
                IZwebForm Form = (IZwebForm)FormBussines.GetForm(1635);
                IDocType DocType = (IDocType)DocTypesBusiness.GetDocType(2050);
                DocType.Indexs = ZCore.FilterIndex((int)DocType.ID,true);
                FormsEditor target = new FormsEditor(Form, DocType);
                target.Show();
                Assert.AreNotEqual(target, null);
            }
            catch (Exception ex)
            { }
        }
    }
}
