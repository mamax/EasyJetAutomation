using System;
using System.ComponentModel;
using System.IO;
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

		private static IWebDriver WebDriver {
			get { return PropertiesCollection.driver ?? StartWebDriver(); }
		}

		public static void Start() {
			PropertiesCollection.driver = StartWebDriver();
		}

		private static IWebDriver StartWebDriver() {
			if( PropertiesCollection.driver != null ) return PropertiesCollection.driver;

			switch( SelectedBrowser ) {
				case Browsers.InternetExplorer:
					PropertiesCollection.driver = StartInternetExplorer();
					break;
				case Browsers.Chrome:
					PropertiesCollection.driver = StartChrome();
					break;
				default:
					throw new Exception( string.Format( "Unknown browser selected: {0}.", SelectedBrowser ) );
			}

			return WebDriver;
		}

		private static ChromeDriver StartChrome() {
			var chromeOptions = new ChromeOptions();
			var defaultDataFolder = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ) + @"\..\Local\Google\Chrome\User Data\Default";

			if( Directory.Exists( defaultDataFolder ) ) {
				WaitHelper.Try( () => DirectoryHelper.ForceDelete( defaultDataFolder ) );
			}

			return new ChromeDriver( Directory.GetCurrentDirectory(), chromeOptions );
		}

		private static InternetExplorerDriver StartInternetExplorer() {
			var internetExplorerOptions = new InternetExplorerOptions {
				IntroduceInstabilityByIgnoringProtectedModeSettings = true,
				InitialBrowserUrl = "about:blank",
				EnableNativeEvents = true
			};

			return new InternetExplorerDriver( Directory.GetCurrentDirectory(), internetExplorerOptions );
		}

		public static void Quit() {
			if( PropertiesCollection.driver == null ) return;

			PropertiesCollection.driver.Quit();
			PropertiesCollection.driver = null;
		}

	}
}
