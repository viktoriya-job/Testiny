using Testiny.Pages;
using Testiny.Helpers.Configuration;

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
        }
    }
}