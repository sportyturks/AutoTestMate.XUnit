using AutoTestMate.XUnit.Infrastructure.Attributes;
using AutoTestMate.XUnit.Infrastructure.Core;
using Xunit;
using Xunit.Abstractions;

namespace AutoTestMate.XUnit.Infrastructure.IntegrationTests
{
    [Collection("TestManagerCollection")]
    public class ConfigurationReaderTests : TestBase
    {
        
        [Fact]
        [ExcelRowKeyData(@"./Data", "NurseryRhymesBook.xlsx", "8", "TableThree")]
        public void EnsureExcelValuesSet1()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("RowKey") == "8");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldSeven") == "Climbed");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldEight") == "Up");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldNine") == "The");
        }

        
        [Fact]
        [ExcelRowKeyData(@"./Data", "NurseryRhymesBook.xlsx", "8", "TableThree")]
        public void EnsureExcelValuesSet2()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("RowKey") == "4");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldFour") == "Blah");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldFive") == "Blah");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldSix") == "Black");
        }

        [Fact]
        [ExcelRowKeyData(@"./Data", "NurseryRhymesBook.xlsx", "8", "TableThree")]
        public void EnsureExcelValuesSet3()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("RowKey") == "3");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldOne") == "Over");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldTwo") == "The");
            Assert.True(ConfigurationReader.GetConfigurationValue("FieldThree") == "Tree");
        }

        [Fact]
        public void AppSettingValue()
        {
            Assert.True(ConfigurationReader.GetConfigurationValue("BrowserType") == "Chrome");

            Assert.True(ConfigurationReader.GetConfigurationValue("Headless") == "false");
        }

        public ConfigurationReaderTests(TestManager testManager, ITestOutputHelper output) : base(testManager, output)
        {
        }
    }
}
