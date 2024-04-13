using OpenQA.Selenium;
using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Steps
{
    public class ProjectsSteps(IWebDriver driver) : BaseSteps(driver)
    {
        public ProjectPage AddProjectSuccessfull(Project project, AddProjectPage addProjectPage)
        {
            return addProjectPage
                    .InputNameValue(project.ProjectName)
                    .InputProjectKeyValue(project.ProjectKey)
                    .InputDescriptionValue(project.Description)
                    .ClickAddButton();
        }

        public AddProjectPage AddProjectUnsuccessfull(Project project)
        {
            AddProjectPage addProjectPage = new AddProjectPage(Driver);

            return addProjectPage
                    .InputNameValue(project.ProjectName)
                    .InputProjectKeyValue(project.ProjectKey)
                    .InputDescriptionValue(project.Description)
                    .ClickCloseButton();
        }
    }
}