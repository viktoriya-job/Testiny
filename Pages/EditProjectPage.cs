using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class EditProjectPage : BasePage
    {
        private static readonly By _titleLabelBy = By.XPath("//h4[contains(text(),'Project')]");
        private static readonly By _nameInputBy = By.CssSelector("[data-testid='textbox-name']");
        private static readonly By _projectKeyInputBy = By.CssSelector("[data-testid='textbox-key']");
        private static readonly By _descriptionInputBy = By.CssSelector("[data-testid='textbox-description']");
        private static readonly By _deleteButtonBy = By.CssSelector("[data-testid='section-project_edit'] button[data-testid='button-more_single:delete']");

        public EditProjectPage(IWebDriver driver) : base(driver) { }

        public UIElement TitleLabel => new(Driver, _titleLabelBy);
        public UIElement NameInput => new(Driver, _nameInputBy);
        public UIElement ProjectKeyInput => new(Driver, _projectKeyInputBy);
        public UIElement DescriptionInput => new(Driver, _descriptionInputBy);
        public Button DeleteButton => new(Driver, _deleteButtonBy);


        [AllureStep("Click Delete Button")]
        public DialogPage DeleteButtonClick()
        {
            DeleteButton.Click();
            return new DialogPage(Driver);
        }

        protected override string GetEndpoint()
        {
            throw new NotImplementedException();
        }

        [AllureStep("Checking is the Edit Project page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return DeleteButton.Displayed && TitleLabel.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}