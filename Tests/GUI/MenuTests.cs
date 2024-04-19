using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Testiny.Helpers.Configuration;
using Testiny.Pages;
using Testiny.Steps;

namespace Testiny.Tests.GUI
{
    [AllureSuite("Menu UI Tests")]
    public class MenuTests : BaseTest
    {
        [Test]
        [AllureFeature("Positive UI Tests")]
        public void TooltipTest()
        {
            TopMenuPage topMenuPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            AllureApi.Step("Checking the value of attribute 'Title'");
            Assert.That(topMenuPage.FeedbackButton.GetAttribute("Title"), Is.EqualTo("Give feedback"));
        }
    }
}