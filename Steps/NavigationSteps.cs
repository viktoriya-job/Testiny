using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Steps
{
    public class NavigationSteps(IWebDriver driver) : BaseSteps(driver)
    {
        public TopMenuPage SuccessfulLogin(User user) => Login<TopMenuPage>(user);
        public LoginPage IncorrectLogin(User user) => Login<LoginPage>(user);

        private T Login<T>(User user) where T : BasePage
        {
            LoginPage = new LoginPage(Driver);
            LoginPage
                .InputUsernameValue(user.Username)
                .InputPasswordValue(user.Password)
                .LoginButton.Click();

            return (T)Activator.CreateInstance(typeof(T), Driver, false);
        }

        [AllureStep("Navigate To All Projects Page")]
        public AllProjectsPage NavigateToAllProjectsPage() => new AllProjectsPage(Driver, true);

        [AllureStep("Navigate To Dashboard Page")]
        public DashboardPage NavigateToDashboardPage() => new DashboardPage(Driver, true);
    }
}