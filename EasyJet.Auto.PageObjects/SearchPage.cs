using System;
using System.Net.Mime;
using System.Threading;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EasyJet.Auto.PageObjects
{
    public class SearchPage : Page {

		#region Methods

		private SearchPage SwitchToPodV3Frame() {
			PropertiesCollection.driver.SwitchTo().DefaultContent();
			SwitchToFrame( "searchPodV3Frame" );
			WebElementExceptions.WaitForPageLoaded();
			return this;
		}

		public SearchPage SelectAdultsNum( string text ) {
			SwitchToPodV3Frame();

			SelectOptionByValue( DropDown_Adults(), text );
			return this;
		}


		public SearchPage SelectInfantsNum( string text ) {
			SwitchToPodV3Frame();
			SelectOptionByValue( DropDown_Infants(), text );
			return this;
		}

		public SearchPage SelectChildrenNum( string numberOfChildren ) {
			SwitchToPodV3Frame();
			SelectOptionByValue( Drop_Down_Children(), numberOfChildren );
			return this;
		}

		public BookingPage ShowFlightsClick() {
			SwitchToPodV3Frame();
			Button_ShowFlights().Click();
			return new BookingPage();
		}

		public SearchPage SelectFlightFrom(string from){
			SwitchToPodV3Frame();
			ClearAndType( Input_From(), from );
			return this;
		}

		public SearchPage SelectFlightTo( string to ) {
			SwitchToPodV3Frame();
			ClearAndType( Input_To(), to );
			return this;
		}

		public SearchPage SetOutBoundDate( string day, string month, string year ) {
			SwitchToPodV3Frame();

			Calendar_From().Click();
			SetDataTime( year, month, day );
			return this;
		}

		private void SetDataTime( string year, string month, string day ) {
			SetYear( year );
			SetMonth( ( Int32.Parse( month ) - 1 ).ToString() );
			SetDay( day );
		}

		public SearchPage SetReturnDate( string day, string month, string year ) {
			SwitchToPodV3Frame();

			if( !Button_Close().Displayed ) {
				if( !ReturnFly().Enabled ) {
					ReturnFly().Click();
				}

				Calendar_To().Click();
			}

			SetDataTime( year, month, day );
			return this;
		}

		private SearchPage SetYear( string year ) {
			SwitchToPodV3Frame();
			Drop_Down_Year().SelectByValue( year );
			return this;
		}

		private SearchPage SetDay( string day ) {
			Button_Day( day ).Click();
			return this;
		}

		private SearchPage SetMonth( string month ) {
			SwitchToPodV3Frame();
			Drop_Down_Month().SelectByValue( month );
			return this;
		}

		public bool HasOopsMessage() {
			return OopsMessage().Displayed;
		}

		public void SetOneWayJourney() {
			SwitchToPodV3Frame();

			if( Return_Button().Selected ) {
				Return_Button().Click();
			}
		}

		#endregion Methods

		#region Controls

		private By DropDown_Adults() {
			return By.Id( "numberOfAdults" );
		}

		private By DropDown_Infants() {
			return By.Id( "numberOfInfants" );
		}

		private By Drop_Down_Children() {
			return By.Id( "numberOfChildren" );
		}

		private IWebElement Button_ShowFlights() {
			return PropertiesCollection.driver.FindElement( By.Id( "searchPodSubmitButton" ) );
		}

		private IWebElement Input_From() {
			return PropertiesCollection.driver.FindElement( By.Id( "acOriginAirport" ) );
		}

		private IWebElement Input_To() {
			return PropertiesCollection.driver.FindElement( By.Id( "acDestinationAirport" ) );
		}

		private IWebElement Calendar_From() {
			return PropertiesCollection.driver.FindElement( By.Id( "oDateCalendar" ) );
		}

		private IWebElement Calendar_To() {
			return PropertiesCollection.driver.FindElement( By.Id( "rDateCalendar" ) );
		}

		private IWebElement ReturnFly() {
			return PropertiesCollection.driver.FindElement( By.Id( "isReturn" ) );
		}

		private SelectElement Drop_Down_Year() {
			return new SelectElement( PropertiesCollection.driver.FindElement( By.Id( "ui-datepicker-year" ) ) );
		}

		private SelectElement Drop_Down_Month() {
			return new SelectElement( PropertiesCollection.driver.FindElement( By.Id( "ui-datepicker-month" ) ) );
		}

		private IWebElement Button_Day( string day ) {
			return PropertiesCollection.driver.FindElement( By.XPath( String.Format( "//a[text()='{0}']", day ) ) );
		}

		private IWebElement Button_Close() {
			return PropertiesCollection.driver.FindElement( By.Id( "datesBoxCloseButton" ) );
		}

		private IWebElement OopsMessage() {
			return PropertiesCollection.driver.FindElement( By.Id( "searchPodValidationErrorBoxBorder" ) );
		}

		private IWebElement Return_Button() {
			return PropertiesCollection.driver.FindElement( By.Id( "isReturn" ) );
		}

		#endregion Controls

	}
}
