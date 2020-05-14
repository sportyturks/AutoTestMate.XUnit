using System.Collections.Generic;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    /// <summary>
    ///     Interface for Framework Configuration Readers
    /// </summary>
    public interface IConfigurationReader
    {
        void AddSetting(string key, string value);
        bool UpdateSetting(string key, string value);
        string GetConfigurationValue(string key, bool required = false);
        string LogLevel { get; }
        string LogName { get; }
	    IDictionary<string, string> Settings { get; }
	    IConfiguration AppConfiguration { get; }
    }
}