using Xunit.Abstractions;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public interface ITestBase
    {
        void TestInitialise();
        void OnTestInitialise();
        IConfigurationReader ConfigurationReader { get; }
        ILoggingUtility LoggingUtility { get; }
        ITestManager TestManager { get; set; }
        ITestOutputHelper Output { get; set; }
        ITest CurrentTest { get; set; }
    }
}