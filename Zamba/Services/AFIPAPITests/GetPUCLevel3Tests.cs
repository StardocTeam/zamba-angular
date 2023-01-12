using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass()]
    public class GetPUCLevel3Tests
    {
        [TestMethod()]
        public void GetTest()
        {
            GetPUCLevel3 PUC3 = new GetPUCLevel3();
            var response = PUC3.Get("20244971416");
            Assert.IsTrue(response.IndexOf("legnani") != -1);
        }
    }
}