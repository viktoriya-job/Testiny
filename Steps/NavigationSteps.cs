using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Steps
{
    public class NavigationSteps(IWebDriver driver) : BaseSteps(driver)
    {
        [AllureStep("Login with correct user data")]
        public TopMenuPage SuccessfulLogin(User user) => Login<TopMenuPage>(user);

        [AllureStep("Login with uncorrect user data")]
        public LoginPage IncorrectLogin(User user) => Login<LoginPage>(user);

        private T Login<T>(User user) where T : BasePage
        {
            LoginPage = new LoginPage(Driver);
            LoginPage
                .UsernameValueInput(user.Username)
                .PasswordValueInput(user.Password)
                .LoginButtonClick();

            return (T)Activator.CreateInstance(typeof(T), Driver, false);
        }

        [AllureStep("Navigate To All Projects Page")]
        public AllProjectsPage NavigateToAllProjectsPage() => new AllProjectsPage(Driver, true);

        [AllureStep("Navigate To Dashboard Page")]
        public DashboardPage NavigateToDashboardPage() => new DashboardPage(Driver, true);

        [AllureStep("Navigate To Import Test Cases Page")]
        public ImportTestCasesPage NavigateToImportTestCasesPage() => new ImportTestCasesPage(Driver, true);
    }
}