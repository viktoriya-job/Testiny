using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Elements;

namespace Testiny.Pages
{
    public class LoginPage : BasePage
    {
        private static string _endPoint = "";

        private static readonly By _usernameInputBy = By.Id(":r0:");
        private static readonly By _passwordInputBy = By.Id(":r2:");
        private static readonly By _loginButtonBy = By.CssSelector("[data-testid='button-login']");
        private static readonly By _errorLabelBy = By.CssSelector("[data-testid='text-login-error:login-error-invalid']");

        public LoginPage(IWebDriver driver) : base(driver, false) { }
        public LoginPage(IWebDriver driver, bool openPageByUrl = false) : base(driver, openPageByUrl) { }

        public UIElement UsernameInput => new(Driver, _usernameInputBy);
        public UIElement PasswordInput => new(Driver, _passwordInputBy);
        public Button LoginButton => new(Driver, _loginButtonBy);
        public UIElement ErrorLabel => new(Driver, _errorLabelBy);

        [AllureStep("Input Username Value")]
        public LoginPage UsernameValueInput(string value)
        {
            UsernameInput.SendKeys(value);
            return this;
        }

        [AllureStep("Input Password Value")]
        public LoginPage PasswordValueInput(string value)
        {
            PasswordInput.SendKeys(value);
            return this;
        }

        [AllureStep("Click Login Button")]
        public LoginPage LoginButtonClick()
        {
            LoginButton.Click();
            return this;
        }

        protected override string GetEndpoint() => _endPoint;

        [AllureStep("Checking is the Login page opened")]
        public override bool IsPageOpened()
        {
            try
            {
                return LoginButton.Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}