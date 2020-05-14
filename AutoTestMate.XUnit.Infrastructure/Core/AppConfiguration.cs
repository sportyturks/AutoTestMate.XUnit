using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Extensions.Configuration;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public class AppConfiguration : IConfiguration
    {
        public AppConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, false)
                .Build().AsEnumerable().ToList();

            Settings = config;

        }

        public List<KeyValuePair<string, string>> Settings { get; }
    }
}
