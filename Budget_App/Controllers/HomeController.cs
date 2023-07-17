using Budget_App.Data;
using Budget_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Diagnostics;

namespace Budget_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoCollection<Expense> _expenseCollection;

        public HomeController(ILogger<HomeController> logger, IOptions<ExpenseDatabaseSettings> expenseDatabaseSettings)
        {
            _logger = logger;
            var mongoClient = new MongoClient(
             expenseDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                expenseDatabaseSettings.Value.DatabaseName);

            _expenseCollection = mongoDatabase.GetCollection<Expense>(
                expenseDatabaseSettings.Value.ExpenseCollectionName);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            var splitList = _expenseCollection.Aggregate()
                                                .Group(m=>m.SpentDate,
                                                n => new
                                                {
                                                    Date=n.Key,
                                                    Spends=n.Select(
                                                        o => new
                                                        {
                                                            ExpenseName = o.ExpenseName,
                                                            ExpenseDescription = o.ExpenseDescription,
                                                            ExpenseAmmount = o.ExpenseAmt
                                                        }
                                                        )
                                                }).ToList().OrderBy(a=>a.Date);




            return View(splitList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}