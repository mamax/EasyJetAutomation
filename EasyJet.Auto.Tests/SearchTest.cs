﻿using EasyJet.Auto.PageObjects;
using EasyJet.Auto.Utilities;
using NUnit.Framework;

namespace EasyJet.Auto.Tests
{
	[TestFixture]
    public class SearchTest : SuiteBase
    {

		[Test]
		public void TestUnableToSearchWithMoreInfantsThanAdults() {

			SearchPage searchPage = new SearchPage( Driver );
			searchPage.Open();
			searchPage.SelectAdultsNum( "2" ).SelectInfantsNum( "3" );

			Assert.IsTrue( searchPage.HasOopsMessage(), "Oops message does not appears" );
		}

		[Test]
		public void TestAbleToFlightFor2AdultsAnd1Children() {

			SearchPage searchPage = new SearchPage( Driver );
			searchPage.Open();
			searchPage.SelectFlightFrom( "Luton" );
			searchPage.SelectFlightTo( "Barcelona" );
			var fromDate = DataTimeHelper.AddDaysToCurrentDate( 3 );
			searchPage.SetOutBoundDate( fromDate.Day.ToString(), fromDate.Month.ToString(), fromDate.Year.ToString() );

			var toDate = fromDate.AddDays( 5 );
			searchPage.SetReturnDate( toDate.Day.ToString(), toDate.Month.ToString(), toDate.Year.ToString() );

			searchPage.SelectAdultsNum( "2" ).SelectChildrenNum( "1" ).SelectInfantsNum( "0" );
			BookingPage bookingPage = searchPage.ClickShowFlights();

			Assert.IsTrue( bookingPage.HasFlights(), "You can not fly with such preferences" );
		}

		[Test]
		public void TestAbleToFlyOnFridays() {

			SearchPage searchPage = new SearchPage( Driver );
			searchPage.Open();
			searchPage.SelectFlightFrom( "Luton" );
			searchPage.SelectFlightTo( "Porto" );
			searchPage.SetOneWayJourney();
			var date = DataTimeHelper.GetFridayDate();
			searchPage.SetOutBoundDate( date.Day.ToString(), date.Month.ToString(), date.Year.ToString() );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ClickShowFlights();

			Assert.IsFalse( bookingPage.HasFlights(), "You can not fly on Friday" );
		}

		[Test]
		public void TestConfirmFlight() {

			SearchPage searchPage = new SearchPage( Driver );
			searchPage.Open();
			searchPage.SelectFlightFrom( "Gatwick" );
			searchPage.SelectFlightTo( "Bordeaux" );
			searchPage.SetOneWayJourney();
			searchPage.SetOutBoundDate( "1", "3", "2017" );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ClickShowFlights();
			bookingPage.SelectJourney();
			Assert.IsTrue( bookingPage.GetJourneyPrice().Contains( "£41.49" ), "Cost of the flight is not true" );
		}

		[Test]
		public void TestConfirmFlightWithLuggage() {

			SearchPage searchPage = new SearchPage( Driver );
			searchPage.Open();
			searchPage.SelectFlightFrom( "Gatwick" );
			searchPage.SelectFlightTo( "Bordeaux" );
			searchPage.SetOneWayJourney();
			searchPage.SetOutBoundDate( "1", "3", "2017" );
			searchPage.SelectAdultsNum( "1" ).SelectChildrenNum( "0" ).SelectInfantsNum( "0" );

			BookingPage bookingPage = searchPage.ClickShowFlights();
			bookingPage.SelectJourney();
			var price = bookingPage.GetJourneyPrice();
			BookingStep2Page bookingStep2Page = bookingPage.ClickContinue();
			bookingStep2Page.AddHoldBag( "23" );
			var priceWithLuggage = bookingStep2Page.GetFinalPrice();
			Assert.IsTrue( ConvertValue.GetDoubleValue( priceWithLuggage ) > ConvertValue.GetDoubleValue( price ), "Cost of the flight has not increased with adding luggage" );
		}

    }
}
