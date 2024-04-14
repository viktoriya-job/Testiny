using OpenQA.Selenium;
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

        public DialogPage AddProjectUnsuccessfull(Project project, AddProjectPage addProjectPage)
        {
            addProjectPage
                    .InputNameValue(project.ProjectName)
                    .InputProjectKeyValue(project.ProjectKey)
                    .InputDescriptionValue(project.Description)
                    .ClickCloseButton();

            return new DialogPage(Driver);
        }

        public AddProjectPage InputProjectFields(Project project, AddProjectPage addProjectPage)
        {
            return addProjectPage
                    .InputNameValue(project.ProjectName)
                    .InputProjectKeyValue(project.ProjectKey)
                    .InputDescriptionValue(project.Description);
        }
    }
}