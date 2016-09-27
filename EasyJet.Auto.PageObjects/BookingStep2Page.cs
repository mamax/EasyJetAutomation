using System.Threading;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EasyJet.Auto.PageObjects {

	public class BookingStep2Page : Page{

		public BookingStep2Page( IWebDriver driver )
			: base( driver ) {
			Driver = driver;
			Driver = PropertiesCollection.Driver;
		}

		private IWebElement Button_AddHoldItems() {
			return Driver.FindElement( By.Id( "addDefaultHoldBaggage" ) );
		}

		private IWebElement Final_Price() {
			return Driver.FindElement( By.XPath( "//div[@class='amount subtotal']" ) );
		}

		private SelectElement Drop_Down_HoldBag() {
			return new SelectElement( Driver.FindElement( By.Id( "bagIndex_1" ) ) );
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
