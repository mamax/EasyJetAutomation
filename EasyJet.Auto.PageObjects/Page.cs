using System;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EasyJet.Auto.PageObjects {

	public abstract class Page {

		protected static IWebDriver Driver;

		protected Page( IWebDriver driver ) {
			Driver = driver;
			Driver = PropertiesCollection.driver;
		}

		protected void SwitchToFrame( string inlineFrame ) {
			Driver.SwitchTo().Frame( inlineFrame );
		}

		protected void ClearAndType( IWebElement element, String text) {
			element.Clear();
			element.SendKeys( text );
			element.SendKeys(Keys.Enter);
		}

		protected void SelectOptionByValue( By element, String text ) {
			try {
				WaitForElementEnabledAndDisplayed( element );
				SelectElement slct = new SelectElement( Driver.FindElement( element ) );
				slct.SelectByValue( text );

			} catch( StaleElementReferenceException ) {
				WebElementExceptions.WaitForPageLoaded();
				SelectElement slct = new SelectElement( Driver.FindElement( element ) );
				slct.SelectByValue( text );
			}
		}

		protected Boolean ElementIsPresent( By element ) {
			try {
				Driver.FindElement( element );
				return true;
			} catch( Exception e ) {
				if( e is NoSuchElementException || e is WebDriverTimeoutException ) {
					return false;
				}
				throw;
			}
		}

		protected Boolean ElementIsVisible( By element ) {
			try {
				return Driver.FindElement( element ).Displayed;
			} catch( Exception e ) {
				if( e is NoSuchElementException || e is ElementNotVisibleException || e is WebDriverTimeoutException ) {
					return false;
				}
				throw;
			}
		}

		protected Boolean WaitForElementEnabledAndDisplayed( By element ) {
			try {
				return Driver.FindElement( element ).Enabled && Driver.FindElement( element ).Displayed;
			} catch( Exception e ) {
				if( e is NoSuchElementException || e is ElementNotVisibleException ) {
					return false;
				}
				throw;
			}
		}

	}
}
