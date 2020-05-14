using System;
using System.Net.Http;
using AutoTestMate.XUnit.Infrastructure.Constants;
using AutoTestMate.XUnit.Infrastructure.Core;
using AutoTestMate.XUnit.Infrastructure.Enums;
using Xunit;
using Xunit.Abstractions;

namespace AutoTestMate.XUnit.Services.Core
{
    public class ServiceTestBase : CoreTestBase, IClassFixture<ServiceTestManager>
    {
        public virtual HttpClient HttpClient => ((ServiceTestManager)TestManager).HttpClient;

        public ServiceTestBase(ITestManager testManager, ITestOutputHelper output) : base(testManager, output)
        {

        }

        public override void OnTestInitialise()
        {
            try
            {
                TestManager.OnTestMethodInitialise();
            }
            catch (Exception exp)
            {
                TestManager.TestResult.SetFailedTest(exp);
                HandleException(exp);
            }
        }

        public override void Dispose()
        {
            try
            {
                //TODO: Implement the below functionality 

                //TODO: Implement the below functionality 
                if (XunitContext.Context.TestException != null)
                {
                    TestManager.TestResult.SetFailedTest(XunitContext.Context.TestException);
                }

                if (TestManager.TestResult.TestResultValue != TestResultType.Success)
                {
                    Output.WriteLine(Generic.TestErrorMessage);
                }
                else
                {
                    Output.WriteLine(Generic.TestSuccessMessage);
                }
            }
            catch (Exception exp)
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
    }
}