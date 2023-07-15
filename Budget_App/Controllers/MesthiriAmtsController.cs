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
    public class MesthiriAmtsController : Controller
    {
        private readonly IMongoCollection<MesthiriAmt> _mesthiriAmtCollection;

        public MesthiriAmtsController(IOptions<MesthiriAmtDatabaseSettings> mesthiriAmtDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            mesthiriAmtDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mesthiriAmtDatabaseSettings.Value.DatabaseName);

            _mesthiriAmtCollection = mongoDatabase.GetCollection<MesthiriAmt>(
                mesthiriAmtDatabaseSettings.Value.MesthiriAmtCollectionName);
        }

        // GET: MesthiriAmts
        public async Task<IActionResult> Index()
        {
            return View(await _mesthiriAmtCollection.Find(_ => true).ToListAsync());
        }

        // GET: MesthiriAmts/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _mesthiriAmtCollection == null)
            {
                return NotFound();
            }

            var mesthiriAmt = await _mesthiriAmtCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
            mesthiriAmt.SpentDate=mesthiriAmt.SpentDate.ToLocalTime();  
            if (mesthiriAmt == null)
            {
                return NotFound();
            }

            return View(mesthiriAmt);
           
        }

        // GET: MesthiriAmts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MesthiriAmts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ExpenseAmt,SpentDate")] MesthiriAmt mesthiriAmt)
        {
            if (ModelState.IsValid)
            {
                await _mesthiriAmtCollection.InsertOneAsync(mesthiriAmt);

                return RedirectToAction(nameof(Index));
            }
            return View(mesthiriAmt);
        }

        // GET: MesthiriAmts/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _mesthiriAmtCollection == null)
            {
                return NotFound();
            }

            var mesthiriAmt = await _mesthiriAmtCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
            mesthiriAmt.SpentDate = mesthiriAmt.SpentDate.ToLocalTime();
            if (mesthiriAmt == null)
            {
                return NotFound();
            }
            return View(mesthiriAmt);
           
        }

        // POST: MesthiriAmts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Description,ExpenseAmt,SpentDate")] MesthiriAmt mesthiriAmt)
        {
            if (id != mesthiriAmt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mesthiriAmtCollection.ReplaceOneAsync(m => m.Id == id, mesthiriAmt);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesthiriAmtExists(mesthiriAmt.Id))
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
            return View(mesthiriAmt);
        }

        // GET: MesthiriAmts/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _mesthiriAmtCollection == null)
            {
                return NotFound();
            }

            var expense = await _mesthiriAmtCollection.DeleteOneAsync(m => m.Id == id);

            if (expense == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        // POST: MesthiriAmts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            if (_mesthiriAmtCollection == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Expenses'  is null.");
            }
            var expense = await _mesthiriAmtCollection.Find(m => m.Id == id).FirstOrDefaultAsync();
            if (expense != null)
            {
                await _mesthiriAmtCollection.DeleteOneAsync(m => m.Id == id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MesthiriAmtExists(string id)
        {
            var mesthiriAmt = _mesthiriAmtCollection.Find(e => e.Id == id);
            return mesthiriAmt.CountDocuments() >= 1 ? true : false;
            }
    }
}
