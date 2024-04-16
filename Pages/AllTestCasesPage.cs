using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AllTestCasesPage : BasePage
    {
        private static readonly By _titleLabelBy = By.CssSelector("[data-testid='text-title']");

        public AllTestCasesPage(IWebDriver driver) : base(driver, false) { }

        public UIElement TitleLabel => new(Driver, _titleLabelBy);

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        [AllureStep("Checking is the All TestCases page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return TitleLabel.Text.Trim() == "All test cases";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}