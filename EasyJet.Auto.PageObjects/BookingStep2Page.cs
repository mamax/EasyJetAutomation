using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EasyJet.Auto.PageObjects {

	public class BookingStep2Page : Page{

		private IWebElement Button_AddHoldItems() {
			return PropertiesCollection.driver.FindElement( By.Id( "addDefaultHoldBaggage" ) );
		}

		private IWebElement Final_Price() {
			return PropertiesCollection.driver.FindElement( By.XPath( "//div[@class='amount subtotal']" ) );
		}

		private SelectElement Drop_Down_HoldBag() {
			return new SelectElement( PropertiesCollection.driver.FindElement( By.Id( "bagIndex_1" ) ) );
		}


		public void AddHoldBag( string p ) {
			Button_AddHoldItems().Click();
			Drop_Down_HoldBag().SelectByValue( p );
			Thread.Sleep( 200 );
		}

		public string GetFinalPrice() {
			return Final_Price().Text;
		}
	}
}
