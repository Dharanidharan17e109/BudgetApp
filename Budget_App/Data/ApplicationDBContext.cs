using Budget_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_App.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {

        }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<MesthiriAmt> MesthiriAmts { get; set; }
    }
}
