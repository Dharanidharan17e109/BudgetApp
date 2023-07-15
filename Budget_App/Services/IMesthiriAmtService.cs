using Budget_App.Models;

namespace Budget_App.Services
{
    public interface IMesthiriAmtService
    {
        Task<IEnumerable<MesthiriAmt>> GetMultipleAsync(string query);
        Task<MesthiriAmt> GetAsync(string id);
        Task AddAsync(MesthiriAmt mesthiriAmt);
        Task UpdateAsync(string id, MesthiriAmt mesthiriAmt);
        Task DeleteAsync(string id);
    }
}
