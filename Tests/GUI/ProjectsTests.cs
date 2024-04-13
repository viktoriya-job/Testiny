using Testiny.Helpers.Configuration;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    public class ProjectsTests : BaseTest
    {
        [Test]
        public void AddProjectTest()
        {
            TopMenuPage topMenuPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            //List<string> list = topMenuPage.ProjectsMenu.GetOptions();
            //Console.WriteLine(list[2]);

            topMenuPage.ProjectsMenu.SelectByText("Create a new project");
            Thread.Sleep(3000);
        }
    }
}