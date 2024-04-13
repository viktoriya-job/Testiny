﻿using Testiny.Helpers.Configuration;
using Testiny.Models;
using Testiny.Pages;

namespace Testiny.Tests.GUI
{
    public class ProjectsTests : BaseTest
    {
        protected Project project = new()

        {
            ProjectName = "Test Project8",
            ProjectKey = "Proj8",
            Description = "Test Description"
        };

        [TestCase]
        public void EnterProjectKeyValueSuccessTest()
        {

        }

        [Test]
        public void AddProjectTest()
        {
            AddProjectPage addProjectPage = NavigationSteps
                .SuccessfulLogin(Configurator.Admin)
                .CreateProjectMenuSelect();

            ProjectPage projectPage = ProjectSteps
                .AddProjectSuccessfull(project, addProjectPage);

            Thread.Sleep(1000);

            Assert.Multiple(() =>
            {
                Assert.That(projectPage.ProjectKeyText.Text.Trim(), Is.EqualTo(project.ProjectKey.ToUpper()));

                AllProjectsPage allProjectsPage = NavigationSteps.NavigateToAllProjectsPage();

                Assert.That(allProjectsPage.ProjectKeysText.Contains(project.ProjectKey.ToUpper()));
            });
        }

        [Test]
        public void RemoveProjectTest()
        {
            NavigationSteps
                .SuccessfulLogin(Configurator.Admin);

            AllProjectsPage allProjectsPage = NavigationSteps
                .NavigateToAllProjectsPage();

            var index = allProjectsPage.ProjectKeys.FindIndex(projectKey => projectKey.Text.Trim().ToUpper() == project.ProjectKey.ToUpper());
            allProjectsPage.ProjectKeys[index].Click();

            Thread.Sleep(3000);
        }
    }
}