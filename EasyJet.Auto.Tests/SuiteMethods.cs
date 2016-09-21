using System;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EasyJet.Auto.Tests {

	public class SuiteMethods {

		//Enter Text
		public void EnterText(string element, string value, string elementType) {

			if( elementType == "Id" )
				PropertiesCollection.driver.FindElement( By.Id( element ) ).SendKeys(value);
			if( elementType == "Name" )
				PropertiesCollection.driver.FindElement( By.Name( element ) ).SendKeys( value );
		}

		public static void Click(string element, string elementType ) {

			if( elementType == "Id" )
				PropertiesCollection.driver.FindElement( By.Id( element ) ).Click();
			if( elementType == "Name" )
				PropertiesCollection.driver.FindElement( By.Name( element ) ).Click();
		}

		public static void SelectDropDown(string element, string value, string elementType ) {

			if( elementType == "Id" )
				new SelectElement(PropertiesCollection.driver.FindElement(By.Id(element))).SelectByText( value );
			if( elementType == "Name" )
				new SelectElement( PropertiesCollection.driver.FindElement( By.Name( element ) ) ).SelectByText( value );

		}

		public static void SwitchToFrame( string inlineFrame ) {
			PropertiesCollection.driver.SwitchTo().Frame( inlineFrame );
		}
	}
}
