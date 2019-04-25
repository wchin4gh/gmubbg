using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Create([Bind("Id")] ITFunction iTFunction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iTFunction);
                await _context.SaveChangesAsync();
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
