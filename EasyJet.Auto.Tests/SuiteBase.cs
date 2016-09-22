using EasyJet.Auto.Utilities;
using NUnit.Framework;

namespace EasyJet.Auto.Tests {

	public class SuiteBase {

		[SetUp]
		public void TestInitialize() {
			Browser.Start();
		}

		[TearDown]
		public void TestCleanup() {
			Browser.Quit();
		}
	}
}
