using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class DashboardPage : BasePage
    {
        private static string _endPoint = "dashboard";
        private static readonly By _titleLabelBy = By.XPath("//p[contains(text(),'Thank you for registering!')]");
        private static readonly By _addButtonBy = By.ClassName("button-label");

        public DashboardPage(IWebDriver driver) : base(driver) { }
        public DashboardPage(IWebDriver driver, bool openPageByUrl) : base(driver, openPageByUrl) { }

        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public Button AddButton => new(Driver, _addButtonBy);

        protected override string GetEndpoint() => _endPoint;

        public override bool IsPageOpened()
        {
            try
            {
                return TitleLable.Displayed && AddButton.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}