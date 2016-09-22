using System;
using System.Threading;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;

namespace EasyJet.Auto.PageObjects {
	public class BookingPage : Page {

		public BookingPage( IWebDriver driver )
			: base( driver ) {
			Driver = driver;
			Driver = PropertiesCollection.driver;
		}

		public bool HasFlights() {
			return ElementIsPresent( Link_Flight() );
		}

		private IWebElement JourneyPrice() {
			return Driver.FindElement( By.XPath( "//div[@class='amount subtotal']" ) );
		}

		private IWebElement Button_Continue() {
			return Driver.FindElement( By.Id( "btnTopContinue" ) );
		}

		private By Link_Flight() {
			return By.XPath( "//div[@class='day selected']/ul/li/a" );
		}

		public string GetJourneyPrice() {
			return JourneyPrice().Text;
		}

		public BookingStep2Page SelectJourney() {
			Driver.FindElement( Link_Flight() ).Click();

			while( ElementIsVisible( Spinner_Loader() ) ) {
				Thread.Sleep( 500 );
			}
			WebElementExceptions.WaitForPageLoaded();
			return new BookingStep2Page( Driver );
		}

		public BookingStep2Page ClickContinue() {
			Button_Continue().Click();
			return new BookingStep2Page( Driver );
		}

		private By Spinner_Loader() {
			return By.ClassName( "loaderImageContainer" ) ;
		}
	}
}
