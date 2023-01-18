using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zamba.FileTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Zamba.FileTools.Tests
{
	[TestClass()]
	public class SpireToolsTests

	{
		SpireTools target = null;

		public SpireToolsTests()
		{
			target = new SpireTools();
		}

		[TestMethod()]
		public void GetTextFromExcelTest()
		{
			SpireTools target = new SpireTools();
			String file = "Resources/Excel.xlsx";
			DataTable result = target.GetExcelAsDataSet(file);
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Rows.Count > 0);
		}
	}
}