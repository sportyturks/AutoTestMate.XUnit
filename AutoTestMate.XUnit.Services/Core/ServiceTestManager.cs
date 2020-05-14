using System;
using System.Net;
using System.Net.Http;
using AutoTestMate.XUnit.Infrastructure.Constants;
using AutoTestMate.XUnit.Infrastructure.Core;
using Castle.MicroKernel.Registration;

namespace AutoTestMate.XUnit.Services.Core
{
    public class ServiceTestManager : TestManager
    {
        #region Properties

        private static ServiceTestManager _uniqueInstance;
        private static readonly object SyncLock = new Object();

        public static ServiceTestManager Instance()
        {
            // Lock entire body of method
            lock (SyncLock)
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (_uniqueInstance == null)
                {
                    _uniqueInstance = new ServiceTestManager();
                }

                CurrentTestManager.Current = _uniqueInstance;

                return _uniqueInstance;
            }
        }
        public virtual bool UseHttpClient
        { 
            get
            {
                if (ConfigurationReader == null)

                {
                    return false;
                }

                var useHttpClientConfigValue = ConfigurationReader.GetConfigurationValue(Constants.Configuration.UseHttpClientConfig);

                return !string.IsNullOrWhiteSpace(useHttpClientConfigValue) && string.Equals(useHttpClientConfigValue.ToLower(), Generic.TrueValue);
            }
        }
        public virtual HttpClient HttpClient
        {
            get
            {
                if (UseHttpClient)
                {
                    return Container.Resolve<HttpClient>();
                }

                throw new ApplicationException(Constants.Configuration.HttpClientSettingExceptionMsg);
            }
        }

        #endregion

        #region Constructor

        public ServiceTestManager()
        {
            TestManagerInitialise();

            CurrentTestManager.Current = this;
        }

        #endregion

        #region Public Methods
        public new void TestManagerInitialise()
        {
            InitialiseIoc();
            InitialiseConfigurationReader();
            InitialiseConfigurationReaderDependencies();
            InitialiseHttpClient();
        }

        public override void Dispose()
        {
            base.Dispose();

            HttpClient?.Dispose();
        }

        public virtual void InitialiseHttpClient()
        {
            var useHttpClient = ConfigurationReader.GetConfigurationValue(Constants.Configuration.UseHttpClientConfig);

            if (string.IsNullOrWhiteSpace(useHttpClient) || !string.Equals(useHttpClient.ToLower(), Generic.TrueValue)) return;
            
            var cookieContainer = new CookieContainer {PerDomainCapacity = 5};
            var httpClientHandler = new HttpClientHandler
            {
                CookieContainer = cookieContainer,
                UseDefaultCredentials = true,
                AllowAutoRedirect = false
            };

            var httpClient = new HttpClient(httpClientHandler);

            Container.Register(Component.For<HttpClient>().Instance(httpClient).OverridesExistingRegistration().LifestyleSingleton())
                .Register(Component.For<HttpClientHandler>().Instance(httpClientHandler).OverridesExistingRegistration().LifestyleSingleton())
                .Register(Component.For<CookieContainer>().Instance(cookieContainer).OverridesExistingRegistration().LifestyleSingleton());
        }
        #endregion
    }
}
