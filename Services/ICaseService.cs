using RestSharp;
using Testiny.Models;

namespace Testiny.Services
{
    public interface ICaseService
    {
        Task<Case> AddCase(Case @case);
        Task<RestResponse> GetCaseById(int caseId);
        Task<RestResponse> GetCasesByQuery(string query);
    }
}