using System.Diagnostics.CodeAnalysis;

namespace AutoTestMate.XUnit.Infrastructure.Constants
{
    [ExcludeFromCodeCoverage]
    public class Configuration
    {
        public const string LogLevelKey = "LogLevel";
		public const string LogNameKey = "LogName";
        public const string NullValue = "NULL";
        public const string TestManagerCollection = "TestManagerCollection";
        public const string ServiceManagerCollection = "ServiceManagerCollection";
        public const string WebManagerCollection = "WebManagerCollection";
    }
}
