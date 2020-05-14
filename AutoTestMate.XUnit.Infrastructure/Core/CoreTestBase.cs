using System;
using System.Reflection;
using AutoTestMate.XUnit.Infrastructure.Constants;
using AutoTestMate.XUnit.Infrastructure.Enums;
using Xunit;
using Xunit.Abstractions;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public abstract class CoreTestBase : XunitContextBase
    {
        // ReSharper disable once PublicConstructorInAbstractClass
        public CoreTestBase(ITestManager testManager, ITestOutputHelper output) : base(output)
        {
            Output = output;
            TestManager = testManager;
            TestInitialise();

            var type = output.GetType();
            var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
            if (testMember != null) CurrentTest = (ITest) testMember.GetValue(output);
        }
        public void TestInitialise()
        {
            OnTestInitialise();
        }

        public virtual void OnTestInitialise()
        {
            try
            {
                TestManager.OnTestMethodInitialise();
            }
            catch (Exception exp)
            {
                TestManager.TestResult.SetFailedTest(exp);

                if (LoggingUtility == null || ConfigurationReader == null) throw;

                LoggingUtility.Error(Exceptions.Exception.ExceptionMsgSetupError + exp.Message);

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
        public virtual void HandleException(Exception exp)
        {
            if (TestManager.LoggingUtility == null || TestManager.ConfigurationReader == null) throw exp;

            LoggingUtility.Error(Exceptions.Exception.ExceptionMsgSetupError + exp.Message);
            Output.WriteLine(Exceptions.Exception.ExceptionMsgSetupError + exp.Message);

            throw exp;
        }
        public virtual IConfigurationReader ConfigurationReader => TestManager.ConfigurationReader;
        public virtual ILoggingUtility LoggingUtility => TestManager.LoggingUtility;
        public ITestManager TestManager { get; set; }
        public new ITestOutputHelper Output { get; set; }
        public ITest CurrentTest { get; set; }
    }
}