using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.Core;

namespace Zamba.IndexerService.Test
{
    [TestClass]
    public class PeekFiles4Indexs
    {
        [TestMethod]
        public void TestProcessQuequedFiles()
        {
           IndexServiceBusiness ISB = new IndexServiceBusiness(0,0);
            Assert.AreEqual(ISB.ProcessQuequedFiles(), true);
        }
    }
}
