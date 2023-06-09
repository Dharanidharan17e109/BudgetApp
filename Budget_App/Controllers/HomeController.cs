using Budget_App.Data;
using Budget_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger,ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            var splitList = _context.Expenses.GroupBy(c => c.SpentDate)
                .Select(x => new
                {
                    Date=x.Key,
                    Spends = x.Select(y => new
                    {
                        ExpenseName=y.ExpenseName,
                        ExpenseDescription=y.ExpenseDescription,
                        ExpenseAmmount=y.ExpenseAmt
                    })
                })
                .ToList();
            return View(splitList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}