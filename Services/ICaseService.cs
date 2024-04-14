using Testiny.Models;

namespace Testiny.Services
{
    public interface ICaseService
    {
        Task<Case> AddCase(Case @case);
        Task<Case> GetCase(int caseId);
    }
}