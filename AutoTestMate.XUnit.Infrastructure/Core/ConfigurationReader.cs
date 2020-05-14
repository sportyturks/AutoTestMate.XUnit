using System.Collections.Generic;
using System.Linq;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
	/// <summary>
	///     Framework configuration reader.
	/// </summary>
	public class ConfigurationReader : IConfigurationReader
    {
        #region Private Variables

        private readonly IDictionary<string, string> _settings;
        private readonly IConfiguration _appConfiguration;

        #endregion

		#region Constructor

		public ConfigurationReader(IConfiguration appConfiguration)
        {
	        _settings = new Dictionary<string, string>();
            _appConfiguration = appConfiguration;
        }

	    public ConfigurationReader(IConfiguration appConfiguration, IDictionary<string, string> settings)
	    {
		    _appConfiguration = appConfiguration;
			_settings = settings;
	    }

		#endregion

		#region Public Methods

		/// <summary>
		///     Gets a value from the configuration file.
		/// </summary>
		public string GetConfigurationValue(string key, bool required = false)
        {
	        if (_settings.Count == 0 && _appConfiguration.Settings.Count == 0)
	        {
		        throw new KeyNotFoundException($"{key} was not found in the test parameters. Please make sure that the solution has an active .runsettings file and that the parameter is valid.");
	        }

			var testSettingsValue = _settings.ContainsKey(key) ? _settings[key] : string.Empty;
			if (!string.IsNullOrWhiteSpace(testSettingsValue))
            {
                return testSettingsValue;
            }

            var list = _appConfiguration.Settings;
            var dictionary = list.ToDictionary(x => x.Key, x => x.Value);
            var appSettingsValue = dictionary.ContainsKey(key) ? dictionary[key] : string.Empty;
			if (!string.IsNullOrWhiteSpace(appSettingsValue))
            {
                return string.Equals(appSettingsValue, Constants.Configuration.NullValue) ? null : appSettingsValue;
            }

            if (!required)
            {
                return string.Empty;
            }
			
			throw new KeyNotFoundException( $"{key} was not found in the test parameters. Please make sure that the solution has an active .runsettings file and that the parameter is valid.");
        }
        public void AddSetting(string key, string value)
        {
            if (!_settings.ContainsKey(key))
            {
                _settings.Add(key, value);
            }
        }

        public bool UpdateSetting(string key, string value)
        {
            if (!_settings.ContainsKey(key)) return false;

            _settings[key] = value;

            return true;
        }
        #endregion

        #region Public Properties
        public string LogLevel => GetConfigurationValue(Constants.Configuration.LogLevelKey);
        public string LogName => GetConfigurationValue(Constants.Configuration.LogNameKey);
        public IDictionary<string, string> Settings => _settings;
		public IConfiguration AppConfiguration => _appConfiguration;

        #endregion
	}
}
