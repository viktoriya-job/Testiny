using RestSharp;
using System.Net;
using Testiny.Models;

namespace Testiny.Services
{
    public interface IProjectService
    {
        Task<Project> AddProject(Project project);
        Task<RestResponse> GetProject(int projectId);
        Task<Project> RemoveProject(int projectId);
    }
}