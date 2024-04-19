using Allure.NUnit.Attributes;
using RestSharp;
using Testiny.Clients;
using Testiny.Models;

namespace Testiny.Services
{
    public class CaseService : ICaseService, IDisposable
    {
        private readonly RestClientExtended _client;

        public CaseService(RestClientExtended client)
        {
            _client = client;
        }

        [AllureStep("Get Case By Id")]
        public Task<RestResponse> GetCaseById(int caseId)
        {
            var request = new RestRequest("api/v1/testcase/{case_id}")
                .AddUrlSegment("case_id", caseId);

            return _client.ExecuteAsync(request);
        }

        [AllureStep("Get Cases By Query")]
        public Task<RestResponse> GetCasesByQuery(string query)
        {
            var request = new RestRequest("api/v1/testcase")
                .AddParameter("q", query);

            return _client.ExecuteAsync(request);
        }

        public Task<Case> AddCase(Case tCase)
        {
            var request = new RestRequest("api/v1/testcase", Method.Post)
                .AddJsonBody(tCase);

            return _client.ExecuteAsync<Case>(request);
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}