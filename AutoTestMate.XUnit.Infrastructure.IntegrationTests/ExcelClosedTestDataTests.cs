using AutoTestMate.XUnit.Infrastructure.Attributes;
using AutoTestMate.XUnit.Infrastructure.Core;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AutoTestMate.XUnit.Infrastructure.IntegrationTests
{
    [Collection(Constants.Configuration.TestManagerCollection)]
    public class ExcelClosedTestDataTests : TestBase
    {
        [Fact]
        [ExcelRowKeyData(@"./Data", "NurseryRhymesBook.xlsx", "8", "TableThree")]
        public void EnsureCorrectFieldsAccessed()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("RowKey") == "8");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldSeven") == "Climbed");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldEight") == "Up");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldNine") == "The");
        }

        [Fact]
        [ExcelRowKeyData(@"./Data", "NurseryRhymesBook.xlsx", "8", "TableThree")]
        public void EnsureCorrectFieldsAccessed1()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("RowKey") == "8");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldSeven") == "Climbed");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldEight") == "Up");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldNine") == "The");
        }

        public ExcelClosedTestDataTests(TestManager testManager, ITestOutputHelper output) : base(testManager, output)
        {
        }
    }
}