using RestSharp;
using System.Net;
using Testiny.Clients;
using Testiny.Models;

namespace Testiny.Services
{
    public class ProjectService : IProjectService, IDisposable
    {
        private readonly RestClientExtended _client;

        public ProjectService(RestClientExtended client)
        {
            _client = client;
        }

        public Task<Project> AddProject(Project project)
        {
            var request = new RestRequest("api/v1/project", Method.Post)
                .AddJsonBody(project);

            return _client.ExecuteAsync<Project>(request);
        }

        public Task<RestResponse> GetProject(int projectId)
        {
            var request = new RestRequest("api/v1/project/{id}")
                .AddUrlSegment("id", projectId);

            return _client.ExecuteAsync(request);
        }

        public Task<Project> RemoveProject(int projectId)
        {
            var request = new RestRequest("api/v1/project/{id}", Method.Delete)
                .AddUrlSegment("id", projectId);

            return _client.ExecuteAsync<Project>(request);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}