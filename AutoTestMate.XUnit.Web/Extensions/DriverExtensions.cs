using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AutoTestMate.XUnit.Infrastructure.Core;
using AutoTestMate.XUnit.Web.Core;
using AutoTestMate.XUnit.Web.Exception;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace AutoTestMate.XUnit.Web.Extensions
{
	public static class DriverExtensions
	{
		public static string ScreenShotSaveFile(this IWebDriver driver, string directory, string testName)
		{
			try
			{
				Directory.CreateDirectory(directory);
				string fileName = Path.Combine(directory, $"{testName}_{DateTime.Now:yyyy-MM-dd_HH.mm}.png");
				var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
				screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
				return fileName;
			}
			catch
			{
				//cannot take screenshot, posssibly due to assert error
				return string.Empty;
			}
		}

		/// <summary>
		///     Waits for a specified function to be satisfied within the specified timeout.
		/// </summary>
		/// <param name="driver">The Web Driver</param>
		/// <param name="func">The function to be satisfied</param>
		/// <param name="limit">The maximum wait time in seconds</param>
		public static void Wait(this IWebDriver driver, Func<IWebDriver, IWebElement> func, int limit)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(limit));
			wait.Until(func);
		}

		public static void Wait(this IWebDriver driver, By by, int limit)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(limit));
			wait.Until(d => d.FindElement(by));
		}

		public static bool TryWait(this IWebDriver driver, By by, int limit)
		{
			try
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(limit));
				wait.Until(d => d.FindElement(by));
				return true;
			}
			catch
			{
				return false;
			}
		}

    	public static void Wait(this IWebDriver driver, Func<IWebDriver, bool> func, int limit)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(limit));
			wait.Until(func);
		}

		public static void Wait(this IWebDriver driver, Func<IWebDriver, ReadOnlyCollection<IWebElement>> func,
			int limit)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(limit));
			wait.Until(func);
		}

        public static void CloseAlert(this IWebDriver driver, ILoggingUtility loggingUtility)
        {
            var alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        public static string CheckDOMForErrors(IWebDriver driver, int timeoutInSeconds)
		{
			if (driver.GetType() == typeof(InternetExplorerDriver))
			{
				throw new UnsupportedDriverTypeException(typeof(InternetExplorerDriver), "This extension is incompatible with Internet Explorer, please use another driver.");
			}

			if (timeoutInSeconds > 0)
			{
				var errorStrings = new List<string>
				{
					"SyntaxError",
					"EvalError",
					"InternalError",
					"ReferenceError",
					"RangeError",
					"TypeError",
					"URIError"
				};

				var jsErrors = driver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));

				if (jsErrors.Any())
				{
					return "JavaScript error(s):" + "\n" + jsErrors.Aggregate("", (s, entry) => s + entry.Message + "\n");
				}

				return null;
			}

			throw new ArgumentException("timeoutInSeconds must be greater than 0");
		}

		public static IJavaScriptExecutor JavaScript(this IWebDriver driver)
		{
			return (driver as IJavaScriptExecutor);
		}

		/// <summary>
		/// 
		/// Selenium's default behavior for FindElement is to throw an exception if an element is not found. 
		/// HasElement catches this exception to allow us to test for elements without stopping execution of 
		/// the entire program.
		/// 
		/// Similary you can write an extension function for IWebElement to test of elements nested in another 
		/// element.
		/// 
		/// </summary>
		/// <param name="driver"></param>
		/// <param name="by"></param>
		/// <returns></returns>
		public static bool HasElement(this IWebDriver driver, By by)
		{
			try
			{
				driver.FindElement(by);
			}
			catch (NoSuchElementException)
			{
				return false;
			}

			return true;
		}

		public static void WaitForAjaxCallCompletion(this IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
		{
			var jsExecutor = driver as IJavaScriptExecutor;

			for (var i = 0; i < timeoutSecs * 2; i++)
			{
				var ajaxIsComplete = (bool)jsExecutor.ExecuteScript("return jQuery.active == 0");
				if (ajaxIsComplete) return;
			}

			if (throwException)
			{
				throw new System.Exception("WebDriver timed out waiting for AJAX call to complete");
			}
		}
	}
}