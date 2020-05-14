using System;
using AutoTestMate.XUnit.Infrastructure.Enums;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public class TestResult : ITestResult
    {
        public TestResult()
        {
            InitilialiseTest();
        }

        public TestResultType TestResultValue { get; set; }
        public System.Exception Exception { get; set; }
        public string TestResultName { get; set;}
        public string TestResultDescription { get; set; }
        public void SetFailedTest(System.Exception exception)
        {
            Exception = exception;
            TestResultValue = TestResultType.Failed;
            TestResultName = "Failed";
            TestResultDescription = "Test has failed, please refer to exception for more details.";
        }

        public void SetPassedTest()
        {
            TestResultValue = TestResultType.Success;
            TestResultName = "Success";
            TestResultDescription = "Test has successfully passed.";
        }

        public void SetInconclusiveTest()
        {
            TestResultValue = TestResultType.Inconclusive;
            TestResultName = "Inconclusive";
            TestResultDescription = "Test has completed with inconclusive result.";
        }
        public void InitilialiseTest()
        {
            TestResultValue = TestResultType.Success;
            TestResultName = "Success";
            TestResultDescription = string.Empty;
            Exception = null;
        }
    }
}