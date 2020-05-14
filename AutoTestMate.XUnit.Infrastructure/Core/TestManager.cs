using System;
using AutoTestMate.XUnit.Infrastructure.Enums;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Xunit;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public class TestManager : ITestManager, IDisposable
	{
		#region Constructor

		public TestManager()
		{
            TestManagerInitialise();

            CurrentTestManager.Current = this;
        }

        #endregion

        #region Properties

        private static TestManager _uniqueInstance;
        private static readonly object SyncLock = new Object();

        public static TestManager Instance()
        {
            // Lock entire body of method
            lock (SyncLock)
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (_uniqueInstance == null)
                {
                    _uniqueInstance = new TestManager();
                }

                CurrentTestManager.Current = _uniqueInstance;

                return _uniqueInstance;
            }
        }
        public bool IsInitialised { get; set; }
        public WindsorContainer Container { get; set; }
		public ILoggingUtility LoggingUtility => Container.Resolve<ILoggingUtility>();
		public IConfigurationReader ConfigurationReader => Container.Resolve<IConfigurationReader>();
        public IConfiguration AppConfiguration => Container.Resolve<IConfiguration>();
        public ITestResult TestResult => Container.Resolve<ITestResult>();
        #endregion

        #region Public Methods

        public virtual void OnTestMethodInitialise()
        {
            if (IsInitialised) throw new ApplicationException(Exceptions.Exception.ExceptionMsgSingletonAlreadyInitialised);

            try
            {
                InitialiseConfigurationReader();
                IsInitialised = true;
            }
            catch (Exception exp)
            {
                LoggingUtility.Error(exp.Message);
                throw;
            }
        }

        public virtual void OnTestCleanup()
        {
            DisposeInternal();
        }

        public virtual void InitialiseConfigurationReader()
        {
            Container.Register(Component.For<IConfigurationReader>().ImplementedBy<ConfigurationReader>().OverridesExistingRegistration().LifestyleSingleton());
            TestResult.InitilialiseTest();

        }

        public void TestManagerInitialise()
        {
            InitialiseIoc();
            InitialiseConfigurationReader();
            InitialiseConfigurationReaderDependencies();
        }

		public virtual void InitialiseIoc()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<ILoggingUtility>().ImplementedBy<TestLogger>().LifestyleSingleton())
                .Register(Component.For<IConfigurationReader>().ImplementedBy<ConfigurationReader>().LifestyleSingleton())
                .Register(Component.For<IConfiguration>().ImplementedBy<AppConfiguration>().LifestyleSingleton())
                .Register(Component.For<IMemoryCache>().ImplementedBy<MemoryCache>().LifestyleSingleton())
                .Register(Component.For<ITestManager>().Instance(this).OverridesExistingRegistration().LifeStyle.Singleton)
                .Register(Component.For<ITestResult>().ImplementedBy<TestResult>().LifestyleSingleton());

            Container = container;
        }
		public virtual void InitialiseConfigurationReaderDependencies()
        {

        }
		
	    public virtual void UpdateConfigurationReader(IConfigurationReader configurationReader)
		{
			if (configurationReader != null)
			{
				Container.Register(Component.For<IConfigurationReader>().Instance(configurationReader).OverridesExistingRegistration().LifestyleSingleton());
			}
			else //Ensure ConfigurationReader is resolved from existing container dependencies 
			{
				Container.Register(Component.For<IConfigurationReader>().ImplementedBy<ConfigurationReader>().OverridesExistingRegistration().LifestyleSingleton());
			}
		}
		public virtual void Dispose()
        {
            DisposeInternal();
            Container.Dispose();
        }
        public virtual void DisposeInternal()
		{
			IsInitialised = false;
		}

        #endregion
	}
}
