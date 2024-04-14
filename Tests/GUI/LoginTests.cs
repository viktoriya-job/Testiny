using Testiny.Pages;
using Testiny.Helpers.Configuration;
using OpenQA.Selenium;
using Testiny.Models;

namespace Testiny.Tests.GUI
{
    public class LoginTests : BaseTest
    {
        [Test]
        [Category("Positive")]
        public void SuccessLoginTest()
        {

            TopMenuPage topMenuPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            Assert.That(topMenuPage.IsPageOpened);
        }

        [Test]
        [Category("Negative")]
        public void UnsuccessLoginTest()
        {
            User user = new()
            {
                Username = "Test@test.com",
                Password = "123"
            };

            LoginPage loginPage = NavigationSteps
                .IncorrectLogin(user);

            Assert.That(loginPage.ErrorLabel.Text.Trim(),
                Is.EqualTo("Either your email address or your password is wrong. Please try again or recover your password.\r\n\r\nCreate a new user, if you are not registered yet."));
        }
    }
}