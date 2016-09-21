using System;
using EasyJet.Auto.PageObjects;
using EasyJet.Auto.Utilities;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace EasyJet.Auto.Tests
{
	[TestFixture]
    public class SearchTest
    {

		[SetUp]
		public void Initialize() {
			PropertiesCollection.driver = new ChromeDriver();
			Console.WriteLine( "Open the Browser" );
			PropertiesCollection.driver.Navigate().GoToUrl( "http://101.test/TestPages/HomePage.html?lang=EN" );
			//driver.Manage().Window.Maximize();
		}

		[Test]
		public void TestUnableToSearchWithMoreInfantsThanAdults() {

			SearchPage searchPage = new SearchPage();
			searchPage.SelectAdultsNum( "2" ).SelectInfantsNum( "3" );

			Assert.IsTrue( searchPage.HasOopsMessage(), "Oops message does not appears" );
		}

		[Test]
		public void TestAbleToFlightFor2AdultsAnd1Children() {

			SearchPage searchPage = new SearchPage();
			searchPage.SelectFlightFrom( "Luton" );
			searchPage.SelectFlightTo( "Barcelona" );
			var fromDate = DataTimeHelper.AddDaysToCurrentDate( 3 );
			searchPage.SetOutBoundDate( fromDate.Day.ToString(), fromDate.Month.ToString(), fromDate.Year.ToString() );

			var toDate = fromDate.AddDays( 5 );
			searchPage.SetReturnDate( toDate.Day.ToString(), toDate.Month.ToString(), toDate.Year.ToString() );

			searchPage.SelectAdultsNum( "2" ).SelectChildrenNum( "1" ).SelectInfantsNum( "0" );
			BookingPage bookingPage = searchPage.ShowFlightsClick();

			Assert.IsTrue( bookingPage.HasFlights(), "You can not fly with such preferences" );
		}

		[Test]
		public void TestAbleToFlyOnFridays() {

			SearchPage searchPage = new SearchPage();
			searchPage.SelectFlightFrom( "Luton" );
			searchPage.SelectFlightTo( "Porto" );
			searchPage.SetOneWayJourney();
			var date = DataTimeHelper.GetFridayDate();
			searchPage.SetOutBoundDate( date.Day.ToString(), date.Month.ToString(), date.Year.ToString() );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ShowFlightsClick();

			Assert.IsFalse( bookingPage.HasFlights(), "You can not fly on Friday" );
		}


		[Test]
		public void TestConfirmFlight() {

			SearchPage searchPage = new SearchPage();
			searchPage.SelectFlightFrom( "Gatwick" );
			searchPage.SelectFlightTo( "Bordeaux" );
			searchPage.SetOneWayJourney();
			searchPage.SetOutBoundDate( "1", "3", "2017" );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ShowFlightsClick();
			bookingPage.SelectJourney();
			Assert.IsTrue( bookingPage.GetJourneyPrice().Contains( "£41.49" ), "Cost of the flight is not true" );
		}

		[Test]
		public void TestConfirmFlightWithLuggage() {

			SearchPage searchPage = new SearchPage();
			searchPage.SelectFlightFrom( "Gatwick" );
			searchPage.SelectFlightTo( "Bordeaux" );
			searchPage.SetOneWayJourney();
			searchPage.SetOutBoundDate( "1", "3", "2017" );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ShowFlightsClick();
			bookingPage.SelectJourney();
			var price = bookingPage.GetJourneyPrice();
			BookingStep2Page bookingStep2Page = bookingPage.ClickContinue();
			bookingStep2Page.AddHoldBag( "23" );
			var priceWithLuggage = bookingStep2Page.GetFinalPrice();
			Assert.IsTrue( (bool?)( GetDoubleValue( priceWithLuggage ) > GetDoubleValue( price ) ), "Cost of the flight has increased with adding luggage" );
		}

		[TearDown]
		public void CleanUp() {
			PropertiesCollection.driver.Quit();
			Console.WriteLine( "Close the Browser" );
		}

		private double GetDoubleValue( string price ) {
			var value = price.Remove(0,1);
			var returnValue = Double.Parse( value );
			return returnValue;
		}
    }
}
