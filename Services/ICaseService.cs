using Testiny.Models;

namespace Testiny.Services
{
    public interface ICaseService
    {
        Task<Case> GetCase(int caseId);
    }
}