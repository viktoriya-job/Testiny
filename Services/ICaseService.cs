using RestSharp;
using Testiny.Models;

namespace Testiny.Services
{
    public interface ICaseService
    {
        Task<Case> AddCase(Case @case);
        Task<RestResponse> GetCase(int caseId);
    }
}