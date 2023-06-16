using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_App.Data;
using Budget_App.Models;

namespace Budget_App.Controllers
{
    public class MesthiriAmtsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MesthiriAmtsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MesthiriAmts
        public async Task<IActionResult> Index()
        {
              return View(await _context.MesthiriAmts.ToListAsync());
        }

        // GET: MesthiriAmts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MesthiriAmts == null)
            {
                return NotFound();
            }

            var mesthiriAmt = await _context.MesthiriAmts
                .FirstOrDefaultAsync(m => m.Id == id);
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
                _context.Add(mesthiriAmt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mesthiriAmt);
        }

        // GET: MesthiriAmts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MesthiriAmts == null)
            {
                return NotFound();
            }

            var mesthiriAmt = await _context.MesthiriAmts.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ExpenseAmt,SpentDate")] MesthiriAmt mesthiriAmt)
        {
            if (id != mesthiriAmt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesthiriAmt);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MesthiriAmts == null)
            {
                return NotFound();
            }

            var mesthiriAmt = await _context.MesthiriAmts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesthiriAmt == null)
            {
                return NotFound();
            }

            return View(mesthiriAmt);
        }

        // POST: MesthiriAmts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MesthiriAmts == null)
            {
                return Problem("Entity set 'ApplicationDBContext.MesthiriAmts'  is null.");
            }
            var mesthiriAmt = await _context.MesthiriAmts.FindAsync(id);
            if (mesthiriAmt != null)
            {
                _context.MesthiriAmts.Remove(mesthiriAmt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesthiriAmtExists(int id)
        {
          return _context.MesthiriAmts.Any(e => e.Id == id);
        }
    }
}
