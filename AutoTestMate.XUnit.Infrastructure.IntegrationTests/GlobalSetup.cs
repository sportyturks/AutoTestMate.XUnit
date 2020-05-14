using AutoTestMate.XUnit.Infrastructure.Core;
using Xunit;
using Xunit.Sdk;

namespace AutoTestMate.XUnit.Infrastructure.IntegrationTests
{
    [GlobalSetUp]
    public static class GlobalSetup
    {
        public static void Setup()
        {
            XunitContext.EnableExceptionCapture();
        }
    }

    [CollectionDefinition(Constants.Configuration.TestManagerCollection)]
    public class TestManagerCollection : ICollectionFixture<TestManager>, ICollectionFixture<TestOutputHelper>
    {
    }
}