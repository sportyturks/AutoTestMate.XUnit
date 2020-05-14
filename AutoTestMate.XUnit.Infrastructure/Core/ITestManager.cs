using Castle.Windsor;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
	public interface ITestManager
	{
        WindsorContainer Container { get; }
		IConfigurationReader ConfigurationReader { get; }
        IConfiguration AppConfiguration { get; }
		ILoggingUtility LoggingUtility { get; }
		ITestResult TestResult { get; }
		void OnTestMethodInitialise();
		void OnTestCleanup();
		void InitialiseIoc();
		void InitialiseConfigurationReader();
		void InitialiseConfigurationReaderDependencies();
        void Dispose();
        void DisposeInternal();
		void UpdateConfigurationReader(IConfigurationReader configurationReader);
    }
}