using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOAprototype.Models;

namespace VOAprototype.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        private readonly VOAprototypeContext _context;

        public PurchaseOrdersController(VOAprototypeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            /*var purchaseOrders = from po in _context.PurchaseOrder
                                 select po;

            Random rng = new Random();
            foreach (PurchaseOrder po in purchaseOrders)
            {
                DateTime date = new DateTime(2015, 1, 1);
                po.EntryDate = date.AddMonths(rng.Next(48)).AddDays(120);
                if (DateTime.Now.AddDays(-120).CompareTo(po.EntryDate) > 0)
                {
                    po.ApprovalDate = po.EntryDate.AddDays(rng.Next(121));
                }
                po.ExpirationDate = po.EntryDate.AddDays(400 + rng.Next(730));
                if (po.ApprovalDate.HasValue)
                {
                    po.PurchaseDate = po.ApprovalDate.Value.AddDays(rng.Next(15));
                }
                if (rng.Next(10) > 2)
                {
                    po.SeatsPerLicense = rng.Next(100);
                    po.SeatsUsed = rng.Next(po.Units * po.SeatsPerLicense.Value);
                }
                _context.Update(po);
            }
            await _context.SaveChangesAsync();*/
            return View();
        }

        // GET: PurchaseOrders
        [HttpGet]
        public async Task<IActionResult> Approvals(string searchString)
        {
            var purchaseOrders = from po in _context.PurchaseOrder
                                 select po;

            if (!String.IsNullOrEmpty(searchString))
            {
                purchaseOrders = purchaseOrders.Where(po =>
                    po.Entity.ToString().Contains(searchString) ||
                    po.BusinessFunction.Contains(searchString) ||
                    po.Finance.Contains(searchString) ||
                    po.ITTower.Contains(searchString) ||
                    po.ITFunction.Contains(searchString) ||
                    po.TBMITService.Contains(searchString) ||
                    po.TBMCategory.Contains(searchString) ||
                    po.TBMName.Contains(searchString) ||
                    po.EgovBRM.Contains(searchString) ||
                    po.Application.Contains(searchString));
            }

            return View(await purchaseOrders.ToListAsync());
        }

        // GET: PurchaseOrders
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var purchaseOrders = from po in _context.PurchaseOrder
                         select po;

            if (!String.IsNullOrEmpty(searchString))
            {
                purchaseOrders = purchaseOrders.Where(po =>
                    po.Entity.ToString().Contains(searchString) ||
                    po.BusinessFunction.Contains(searchString) ||
                    po.Finance.Contains(searchString) ||
                    po.ITTower.Contains(searchString) ||
                    po.ITFunction.Contains(searchString) ||
                    po.TBMITService.Contains(searchString) ||
                    po.TBMCategory.Contains(searchString) ||
                    po.TBMName.Contains(searchString) ||
                    po.EgovBRM.Contains(searchString) ||
                    po.Application.Contains(searchString));
            }

            return View(await purchaseOrders.ToListAsync());
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Application,Description,Entity,BusinessFunction,Finance,ITTower,ITFunction,TBMITService,TBMCategory,TBMName,EgovBRM,Units,UnitPrice,SeatsPerLicense,SeatsUsed,EntryDate,PurchaseDate,ApprovalDate,ExpirationDate")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                purchaseOrder.EntryDate = DateTime.Now;
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            var itfunctions = from itf in _context.ITFunction select itf;
            SelectList selectList = new SelectList(itfunctions, "Id", "Id");
            ViewBag.itfunctionlist = selectList;

            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Application,Description,Entity,BusinessFunction,Finance,ITTower,ITFunction,TBMITService,TBMCategory,TBMName,EgovBRM,Units,UnitPrice,SeatsPerLicense,SeatsUsed,EntryDate,PurchaseDate,ApprovalDate,ExpirationDate")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.Id))
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
            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        [HttpPost("UploadFiles")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                List<PurchaseOrderCSVModel> poModels = new List<PurchaseOrderCSVModel>();
                using (var streamreader = new StreamReader(file.OpenReadStream()))
                using (var csvreader = new CsvReader(streamreader))
                {
                    csvreader.Configuration.RegisterClassMap<PurchaseOrderCSVModel.PurchaseOrderCSVMap>();
                    poModels.AddRange(csvreader.GetRecords<PurchaseOrderCSVModel>());
                }
                if (ModelState.IsValid)
                {
                    foreach (PurchaseOrderCSVModel pomodel in poModels)
                    {
                        var purchaseOrder = await _context.PurchaseOrder.FindAsync(pomodel.Id);
                        if (purchaseOrder == null)
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {
                                purchaseOrder = pomodel.ToNewPurchaseOrder();

                                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PurchaseOrder] ON");

                                _context.PurchaseOrder.Add(purchaseOrder);
                                _context.SaveChanges();

                                _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PurchaseOrder] OFF");

                                transaction.Commit();
                            }
                        }
                        else
                        {
                            pomodel.UpdatePurchaseOrder(purchaseOrder);
                            _context.Update(purchaseOrder);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PurchaseOrders/Approve/5
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, [Bind("Id,Entity,BusinessFunction,Finance,ITTower,ITFunction,TBMITService,TBMCategory,TBMName,EgovBRM,Application,Units,UnitPrice,SeatsPerLicense,SeatsUsed,Description,EntryDate")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    purchaseOrder.ApprovalDate = DateTime.Now;
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.Id))
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
            return View(purchaseOrder);
        }

        public IActionResult Export()
        {
            var purchaseOrders = from po in _context.PurchaseOrder
                                 select po;
            var poModels = purchaseOrders.Select(po => new PurchaseOrderCSVModel(po)).ToList();

            var result = WriteCSVToMemory(poModels);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv");
        }

        private byte[] WriteCSVToMemory(IEnumerable<PurchaseOrderCSVModel> orders)
        {
            using (var memorystream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memorystream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.Configuration.RegisterClassMap<PurchaseOrderCSVModel.PurchaseOrderCSVMap>();
                csvWriter.WriteRecords(orders);
                streamWriter.Flush();
                return memorystream.ToArray();
            }
        }

        // POST: PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            _context.PurchaseOrder.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrder.Any(e => e.Id == id);
        }
    }
}
