using Budget_App.Models;

namespace Budget_App.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetMultipleAsync(string query);
        Task<Expense> GetAsync(string id);
        Task AddAsync(Expense item);
        Task UpdateAsync(string id, Expense item);
        Task DeleteAsync(string id);
    }
}
