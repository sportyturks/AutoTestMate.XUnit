using System.Collections.Generic;
using System.Collections.Specialized;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    public interface IConfiguration
    {
        List<KeyValuePair<string, string>> Settings { get; }
    }
}


