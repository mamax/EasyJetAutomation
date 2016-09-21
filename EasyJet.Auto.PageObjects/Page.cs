using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyJet.Auto.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;

namespace EasyJet.Auto.PageObjects {

	public abstract class Page {

		//Driver = PropertiesCollection.driver() = Driver;
		//protected IWebElement _element;
		//WebElementExceptions extens = new WebElementExceptions()

		public void SwitchToFrame( string inlineFrame ) {
			PropertiesCollection.driver.SwitchTo().Frame( inlineFrame );
		}

		protected void Click(IWebElement element) {
			element.Click();
		}

		protected void ClearAndType( IWebElement element, String text) {
			element.Clear();
			element.SendKeys( text );
			element.SendKeys(Keys.Enter);
		}

		public void SelectOptionByValue( By element, String text ) {
			try {
				SelectElement slct = new SelectElement( PropertiesCollection.driver.FindElement( element ) );
				slct.SelectByValue( text );

			} catch( StaleElementReferenceException ) {
				WebElementExceptions.WaitForPageLoaded();
				SelectElement slct = new SelectElement( PropertiesCollection.driver.FindElement( element ) );
				slct.SelectByValue( text );
			}

		}

		public Boolean ElementIsPresent( By element ) {
			try {
				PropertiesCollection.driver.FindElement( element );
				return true;
			} catch( Exception e ) {
				if( e is NoSuchElementException || e is WebDriverTimeoutException ) {
					return false;
				}
				throw;
			}
		}

		public Boolean ElementIsVisible( By element ) {
			try {
				return PropertiesCollection.driver.FindElement( element ).Displayed;
			} catch( Exception e ) {
				if( e is NoSuchElementException || e is ElementNotVisibleException || e is WebDriverTimeoutException ) {
					return false;
				}
				throw;
			}
		}

		

	}
}
