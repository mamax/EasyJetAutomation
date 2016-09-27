using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;

namespace EasyJet.Auto.Utilities {

	public static class WebElementExceptions {

		public static void WaitForPageLoaded() {
			WaitForPageLoaded( 5, 50, (int)TimeSpan.FromSeconds( 200 ).TotalMilliseconds );
		}

		public static void WaitForPageLoaded( int timeSliceMs, int continuousChecks, int timeoutMs ) {
			if( !IsDocumentReadyStateComplete( PropertiesCollection.Driver ) ) {
				if( !PropertiesCollection.Driver.WaitUntil( IsDocumentReadyStateComplete, timeoutMs ) ) {

					return;
				}
			}

			WaitForPageSourceLoaded( timeSliceMs, continuousChecks, timeoutMs );
		}

		private static bool IsDocumentReadyStateComplete( IWebDriver webDriver ) {
			return webDriver.ExecuteJavaScript<bool>( "return (document.readyState == 'complete');" );
		}

		private static bool WaitUntil<T>( this T t, Func<T, bool> condition, int timeoutMs ) where T : ISearchContext {
			IWait<T> wait = new DefaultWait<T>( t ) { Timeout = TimeSpan.FromMilliseconds( timeoutMs ) };
			return wait.Until( condition );
		}

		private static void WaitForPageSourceLoaded( int timeSliceMs, int continuousChecks, int timeoutMs ) {
			var continuousChecksCounter = 0;

			while( timeoutMs > 0 ) {
				object htmlLengthBeforeSleep = ( (IJavaScriptExecutor)PropertiesCollection.Driver ).ExecuteScript( "return document.documentElement.innerHTML.length;" );
				Thread.Sleep( timeSliceMs );
				timeoutMs -= timeSliceMs;

				if( htmlLengthBeforeSleep.Equals( ( (IJavaScriptExecutor)PropertiesCollection.Driver ).ExecuteScript( "return document.documentElement.innerHTML.length;" ) ) ) {
					continuousChecksCounter++;
				} else {
					continuousChecksCounter = 0;
				}

				if( continuousChecksCounter < continuousChecks ) {
					continue;
				}

				return;
			}

		}
	}
}
