using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOAprototype.Classification;
using VOAprototype.Models;

namespace VOAprototype.Controllers
{
    public class ITFunctionsController : Controller
    {
        private readonly VOAprototypeContext _context;

        public ITFunctionsController(VOAprototypeContext context)
        {
            _context = context;
        }

        // GET: ITFunctions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ITFunction.ToListAsync());
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: ITFunctions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTFunction = await _context.ITFunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iTFunction == null)
            {
                return NotFound();
            }

            return View(iTFunction);
        }

        // GET: ITFunctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ITFunctions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Unigram")] ITFunction iTFunction)
        {
            if (ModelState.IsValid)
            {
                iTFunction.Unigram = await Classifier.Filter(iTFunction.Description);
                await Classifier.Add(iTFunction.Id, iTFunction.Unigram);
                _context.Add(iTFunction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(iTFunction);
        }

        // GET: ITFunctions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTFunction = await _context.ITFunction.FindAsync(id);
            if (iTFunction == null)
            {
                return NotFound();
            }
            return View(iTFunction);
        }

        // POST: ITFunctions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Unigram")] ITFunction iTFunction)
        {
            if (id != iTFunction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(iTFunction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ITFunctionExists(iTFunction.Id))
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
            return View(iTFunction);
        }

        // GET: ITFunctions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iTFunction = await _context.ITFunction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iTFunction == null)
            {
                return NotFound();
            }

            return View(iTFunction);
        }

        // POST: ITFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var iTFunction = await _context.ITFunction.FindAsync(id);
            _context.ITFunction.Remove(iTFunction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ITFunctionExists(string id)
        {
            return _context.ITFunction.Any(e => e.Id == id);
        }
    }
}
