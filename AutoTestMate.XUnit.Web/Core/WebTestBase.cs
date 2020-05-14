using System.Net.Http;
using AutoTestMate.XUnit.Infrastructure.Constants;
using AutoTestMate.XUnit.Infrastructure.Core;
using AutoTestMate.XUnit.Infrastructure.Enums;
using AutoTestMate.XUnit.Services.Core;
using AutoTestMate.XUnit.Web.Extensions;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;

namespace AutoTestMate.XUnit.Web.Core
{
	public abstract class WebTestBase : CoreTestBase, IClassFixture<WebTestManager>
	{
        public virtual HttpClient HttpClient => ((WebTestManager)TestManager).HttpClient;
        // ReSharper disable once PublicConstructorInAbstractClass
        public WebTestBase(ITestManager testManager, ITestOutputHelper output) : base(testManager, output)
        {

        }
        public override void OnTestInitialise()
        {
            try
            {
                TestManager.OnTestMethodInitialise();
            }
            catch (System.Exception exp)
            {
                TestManager.TestResult.SetFailedTest(exp); 

                if (LoggingUtility == null || ConfigurationReader == null) throw;

                LoggingUtility.Error(Constants.Exceptions.ExceptionMsgSetupError + exp.Message);

                throw;
            }
        }

        public override void Dispose()
		{
			try
			{
                //TODO: Implement the below functionality 
                if (XunitContext.Context.TestException != null)
                {
                    TestManager.TestResult.SetFailedTest(XunitContext.Context.TestException);
                }

                if (TestManager.TestResult.TestResultValue != TestResultType.Success)
				{
					Output.WriteLine(Generic.TestErrorMessage);

					if (LoggingUtility == null || ConfigurationReader == null) return;

                    var outputPath =
                        !string.IsNullOrWhiteSpace(
                            ConfigurationReader.GetConfigurationValue(Constants.Configuration
                                .ConfigKeyOutputFileScreenshotsDirectory))
                            ? $"{ConfigurationReader.GetConfigurationValue(Constants.Configuration.ConfigKeyOutputFileScreenshotsDirectory)}"
                            : "";

					if (((WebTestManager)TestManager).IsDriverNull) return;

                    Output.WriteLine($"Attempting to capture screenshot to: {outputPath}");

					var captureScreenShot = Driver.ScreenShotSaveFile(outputPath, CurrentTest.DisplayName);

					if (string.IsNullOrWhiteSpace(captureScreenShot)) return;

                    //TestContext.AddTestAttachment(captureScreenShot);

					LoggingUtility.Error(captureScreenShot);
				}
				else
				{

                   Output.WriteLine(Generic.TestSuccessMessage);
				}
			}
			catch (System.Exception exp)
			{
                TestManager.TestResult.SetFailedTest(exp);
                HandleException(exp);
			}
			finally
			{
				TestManager.OnTestCleanup();
			}

            base.Dispose();
        }

		public IWebDriver Driver => ((WebTestManager)TestManager).Browser;
    }
}




