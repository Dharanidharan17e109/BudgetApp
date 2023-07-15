using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_App.Data;
using Budget_App.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Budget_App.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IMongoCollection<Expense> _expenseCollection;
        //private readonly ApplicationDBContext _context;

        public ExpensesController(IOptions<ExpenseDatabaseSettings> expenseDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            expenseDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                expenseDatabaseSettings.Value.DatabaseName);

            _expenseCollection = mongoDatabase.GetCollection<Expense>(
                expenseDatabaseSettings.Value.ExpenseCollectionName);
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
              return View(await _expenseCollection.Find(_=>true).ToListAsync());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _expenseCollection == null)
            {
                return NotFound();
            }

            var expense = await _expenseCollection.Find(m => m.Id==id).FirstOrDefaultAsync();
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpenseName,ExpenseDescription,ExpenseAmt,SpentDate")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expenseCollection.InsertOneAsync(expense);
                
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _expenseCollection== null)
            {
                return NotFound();
            }

            var expense = await _expenseCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
            expense.SpentDate = expense.SpentDate.ToLocalTime();
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ExpenseName,ExpenseDescription,ExpenseAmt,SpentDate")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseCollection.ReplaceOneAsync(m=>m.Id==id,expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _expenseCollection == null)
            {
                return NotFound();
            }

            var expense = await _expenseCollection.DeleteOneAsync(m => m.Id == id);
  
            if (expense == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_expenseCollection == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Expenses'  is null.");
            }
            var expense =  await _expenseCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (expense != null)
            {
                await _expenseCollection.DeleteOneAsync(m => m.Id == id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(string id)
        {
          var expense=_expenseCollection.Find(e => e.Id == id);
            return expense.CountDocuments() >= 1 ? true : false;
        }
    }
}
