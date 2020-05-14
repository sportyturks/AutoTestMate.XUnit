using AutoTestMate.XUnit.Infrastructure.Enums;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public interface ITestResult
    {
        TestResultType TestResultValue { get; set; }
        System.Exception Exception { get; set; }
        string TestResultName { get; set; }
        string TestResultDescription { get; set; }
        void SetFailedTest(System.Exception exception);
        void SetPassedTest();
        void SetInconclusiveTest();
        void InitilialiseTest();
    }
}