using EasyJet.Auto.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace EasyJet.Auto.Tests {

	public class SuiteBase {

		protected IWebDriver Driver = PropertiesCollection.driver;

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
