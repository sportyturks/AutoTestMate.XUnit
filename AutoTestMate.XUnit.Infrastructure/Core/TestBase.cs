using AutoTestMate.XUnit.Infrastructure.Attributes;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutoTestMate.XUnit.Infrastructure.Core
{
    [Collection("TestManagerCollection")]
    public class TestBase : CoreTestBase, ITestBase
    {
        // ReSharper disable once PublicConstructorInAbstractClass
        public TestBase(TestManager testManager, ITestOutputHelper output): base(testManager, output)
		{
        }
    }
}