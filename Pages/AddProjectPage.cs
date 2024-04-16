using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class AddProjectPage : BasePage
    {
        private static readonly By _titleIconBy = By.Id("icon-project-add-20");
        private static readonly By _titleLabelBy = By.XPath("//h4[contains(text(),'Create a new project')]");
        private static readonly By _nameInputBy = By.CssSelector("[data-testid='textbox-name']");
        private static readonly By _projectKeyInputBy = By.CssSelector("[data-testid='textbox-key']");
        private static readonly By _descriptionInputBy = By.CssSelector("[data-testid='textbox-description']");
        private static readonly By _addButtonBy = By.CssSelector("[data-testid='button-save-entity']");
        private static readonly By _closeButtonBy = By.CssSelector("[data-testid='button-close-entity'] div");
        private static readonly By _deleteButtonBy = By.CssSelector("[data-testid='section-project_edit'] button[data-testid='button-more_single:delete']");
        private static readonly By _errorLabelBy = By.XPath("//span[contains(text(),'Valid input is required')]");

        public AddProjectPage(IWebDriver driver) : base(driver) { }

        public UIElement TitleIcon => new(Driver, _titleIconBy);
        public UIElement TitleLabel => new(Driver, _titleLabelBy);
        public UIElement NameInput => new(Driver, _nameInputBy);
        public UIElement ProjectKeyInput => new(Driver, _projectKeyInputBy);
        public UIElement DescriptionInput => new(Driver, _descriptionInputBy);
        public Button AddButton => new(Driver, _addButtonBy);
        public Button CloseButton => new(Driver, _closeButtonBy);
        public Button DeleteButton => new(Driver, _deleteButtonBy);
        public UIElement ErrorLabel => new(Driver, _errorLabelBy);

        [AllureStep("Input Project Name Value")]
        public AddProjectPage NameValueInput(string value)
        {
            NameInput.SendKeys(value);
            return this;
        }

        [AllureStep("Input Project Key Value")]
        public AddProjectPage ProjectKeyValueInput(string value)
        {
            ProjectKeyInput.SendKeys(value);
            return this;
        }

        [AllureStep("Input Project Description Value")]
        public AddProjectPage DescriptionValueInput(string value)
        {
            DescriptionInput.SendKeys(value);
            return this;
        }

        [AllureStep("Click Add Button")]
        public ProjectPage AddButtonClick()
        {
            AddButton.Click();
            return new ProjectPage(Driver);
        }

        [AllureStep("Click Close Button")]
        public AddProjectPage CloseButtonClick()
        {
            CloseButton.Click();
            return this;
        }

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

        [AllureStep("Checking is the Add Project page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return TitleLabel.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}