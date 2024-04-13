using Testiny.Pages;
using Testiny.Helpers.Configuration;
using OpenQA.Selenium;

namespace Testiny.Tests.GUI
{
    public class LoginTests : BaseTest
    {
        [Test]
        public void SuccessLoginTest()
        {

            TopMenuPage topMenuPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            Assert.That(topMenuPage.IsPageOpened);
            //topMenuPage.ProjectsMenu.SelectByIndex(2);
            //Thread.Sleep(1000);

            //topMenuPage.AccountMenu.SelectByIndex(0);
            //Thread.Sleep(3000);
        }
    }
}