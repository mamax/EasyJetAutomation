using System;
using System.ComponentModel;
using EasyJet.Auto.Utilities.Properties;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace EasyJet.Auto.Utilities {

	[Serializable]
	public enum Browsers {
		[Description( "Internet Explorer" )]
		InternetExplorer,

		[Description( "Chrome" )]
		Chrome
	}

	public static class Browser {

		private static Browsers SelectedBrowser {
			get { return Settings.Default.Browser; }
		}

		private static IWebDriver WebDriver => PropertiesCollection.Driver ?? StartWebDriver();

	    public static void Start() {
			PropertiesCollection.Driver = StartWebDriver();
		}

		private static IWebDriver StartWebDriver() {
			if( PropertiesCollection.Driver != null ) return PropertiesCollection.Driver;

			switch( SelectedBrowser ) {
				case Browsers.InternetExplorer:
					PropertiesCollection.Driver = StartInternetExplorer();
					break;
				case Browsers.Chrome:
					PropertiesCollection.Driver = StartChrome();
					break;
				default:
					throw new Exception(String.Format( "Unknown browser selected: {0}.", SelectedBrowser ) );
			}

            PropertiesCollection.Driver.Manage().Window.Maximize();
            PropertiesCollection.Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            PropertiesCollection.Driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(20));
            PropertiesCollection.Driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(15));

            return WebDriver;
		}

		private static ChromeDriver StartChrome() {
			var chromeOptions = new ChromeOptions();

			return new ChromeDriver( chromeOptions );
		}

		private static InternetExplorerDriver StartInternetExplorer() {
			var internetExplorerOptions = new InternetExplorerOptions {
				IntroduceInstabilityByIgnoringProtectedModeSettings = true,
				InitialBrowserUrl = "about:blank",
				EnableNativeEvents = true
			};

			return new InternetExplorerDriver( internetExplorerOptions );
		}

		public static void Quit() {
			if( PropertiesCollection.Driver == null ) return;

			PropertiesCollection.Driver.Quit();
			PropertiesCollection.Driver = null;
		}

	}
}
