using System.ComponentModel;

namespace AutoTestMate.XUnit.Web.Enums
{
	public enum BrowserTypes
	{
		[Description("firefox")]
		Firefox,
		[Description("iexplore")]
		InternetExplorer,
		[Description("chrome")]
		Chrome,
		[Description("microsoftwebdriver")]
		Edge,
		[Description("")]
		NotSet
	}
}
