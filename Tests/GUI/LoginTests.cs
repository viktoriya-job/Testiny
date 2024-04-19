using Testiny.Pages;
using Testiny.Helpers.Configuration;
using Testiny.Models;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;

namespace Testiny.Tests.GUI
{
    [AllureSuite("Login UI Tests")]
    public class LoginTests : BaseTest
    {
        [Test]
        [AllureFeature("Positive UI Tests")]
        public void SuccessLoginTest()
        {
            TopMenuPage topMenuPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            Assert.That(topMenuPage.IsPageOpened);
        }

        [Test]
        [AllureFeature("Negative UI Tests")]
        public void UnsuccessLoginTest()
        {
            User user = new()
            {
                Username = "Test@test.com",
                Password = "123"
            };

            LoginPage loginPage = NavigationSteps
                .IncorrectLogin(user);

            AllureApi.Step("Checking is the Error text displayed");
            Assert.That(loginPage.ErrorLabel.Text.Trim(),
                Is.EqualTo("Either your email address or your password is wrong. Please try again or recover your password.\r\n\r\nCreate a new user, if you are not registered yet."));
        }
    }
}