using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class ProjectPage : BasePage
    {
        private static readonly By _titleLabelBy = By.CssSelector("[data-testid='header-dashboard']>p");
        private static readonly By _projectKeyTextBy = By.CssSelector("[data-testid='text-project-key']");

        public ProjectPage(IWebDriver driver) : base(driver) { }

        public UIElement TitleLable => new(Driver, _titleLabelBy);
        public Button ProjectKeyText => new(Driver, _projectKeyTextBy);
        public TopMenuPage topMenu => new TopMenuPage(Driver);

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        public override bool IsPageOpened()
        {
            try
            {
                return TitleLable.Text.Trim() == $"Have a great day – here is an overview of your {ProjectKeyText.Text.Trim()} project:" 
                    && ProjectKeyText.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}