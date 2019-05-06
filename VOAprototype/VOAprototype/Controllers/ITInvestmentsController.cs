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
using VOAprototype.Classification;
using VOAprototype.Models;

namespace VOAprototype.Controllers
{
    public class ITInvestmentsController : Controller
    {
        private readonly VOAprototypeContext _context;

        public ITInvestmentsController(VOAprototypeContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult TBMDashboard()
        {
            return View();
        }

        private async Task<List<ITInvestment>> SearchPOs(string searchString)
        {
            var purchaseOrders = from po in _context.PurchaseOrder
                                 select po;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                purchaseOrders = purchaseOrders.Where(po =>
                    (po.Entity.ToString() ?? "").ToLower().Contains(searchString) ||
                    (po.BusinessFunction ?? "").ToLower().Contains(searchString) ||
                    (po.Finance ?? "").ToLower().Contains(searchString) ||
                    (po.ITTower ?? "").ToLower().Contains(searchString) ||
                    (po.ITFunction ?? "").ToLower().Contains(searchString) ||
                    (po.TBMITService ?? "").ToLower().Contains(searchString) ||
                    (po.TBMCategory ?? "").ToLower().Contains(searchString) ||
                    (po.TBMName ?? "").ToLower().Contains(searchString) ||
                    (po.EgovBRM ?? "").ToLower().Contains(searchString) ||
                    (po.Application ?? "").ToLower().Contains(searchString));
            }
            return purchaseOrders.ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Approvals(string searchString)
        {
            return View((await SearchPOs(searchString)).OrderByDescending(po => po.EntryDate));
        }

        // GET: PurchaseOrders
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            return View((await SearchPOs(searchString)).OrderByDescending(po => po.EntryDate));
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
            var itfunctions = _context.ITFunction.OrderBy(itf => itf.Name);
            SelectList selectList = new SelectList(itfunctions, "Name", "Name");
            ViewBag.itfunctionlist = selectList;
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Application,Description,Entity,BusinessFunction,Finance,ITTower,ITFunction,TBMITService,TBMCategory,TBMName,EgovBRM,Units,UnitPrice,SeatsPerLicense,SeatsUsed,EntryDate,PurchaseDate,ApprovalDate,ExpirationDate")] ITInvestment purchaseOrder)
        {
            purchaseOrder.EntryDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            foreach (var error in errors)
            {
                String errorMessage = "";
                foreach (var err in error.Errors)
                {
                    errorMessage += err.ErrorMessage + "; ";
                }
                Debug.WriteLine("HERRE: " + errorMessage);
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
            var itfunctions = _context.ITFunction.OrderBy(itf => itf.Name);
            SelectList selectList = new SelectList(itfunctions, "Name", "Name");
            ViewBag.itfunctionlist = selectList;

            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Application,Description,Entity,ITFunction,ITTower,Units,UnitPrice,SeatsPerLicense,SeatsUsed,BusinessFunction,Finance,TBMITService,TBMCategory,TBMName,EgovBRM,EntryDate,PurchaseDate,ApprovalDate,ExpirationDate,FirstClassification,SecondClassification,ThirdClassification")] ITInvestment purchaseOrder)
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
                List<ITInvestmentCSVModel> poModels = new List<ITInvestmentCSVModel>();
                using (var streamreader = new StreamReader(file.OpenReadStream()))
                using (var csvreader = new CsvReader(streamreader))
                {
                    csvreader.Configuration.RegisterClassMap<ITInvestmentCSVModel.PurchaseOrderCSVMap>();
                    poModels.AddRange(csvreader.GetRecords<ITInvestmentCSVModel>());
                }
                if (ModelState.IsValid)
                {
                    foreach (ITInvestmentCSVModel pomodel in poModels)
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
            if (ViewBag.itfunctionlist == null)
            {
                SelectList selectList = new SelectList(_context.ITFunction.OrderBy(itf => itf.Name), "Name", "Name");
                ViewBag.itfunctionlist = selectList;
            }
            List<string> classifiedITFs = await Classifier.Classify(purchaseOrder.Application, purchaseOrder.Description);
            purchaseOrder.FirstClassification = classifiedITFs[0];
            purchaseOrder.SecondClassification = classifiedITFs[1];
            purchaseOrder.ThirdClassification = classifiedITFs[2];
            var allPOs = from po in _context.PurchaseOrder select po;
            HashSet<string> firstClass = new HashSet<string>();
            List<string> secondClass = new List<string>();
            List<string> thirdClass = new List<string>();
            
            foreach (ITInvestment order in allPOs)
            {
                if (order.PurchaseDate.HasValue)
                {
                    if (order.ITFunction.Equals(classifiedITFs[0]))
                    {
                        firstClass.Add(order.Application);
                    }
                    if (order.ITFunction.Equals(classifiedITFs[1]))
                    {
                        secondClass.Add(order.Application);
                    }
                    if (order.ITFunction.Equals(classifiedITFs[2]))
                    {
                        thirdClass.Add(order.Application);
                    }
                }
            }
            firstClass.UnionWith(secondClass);
            firstClass.UnionWith(thirdClass);
            ViewBag.Alternatives = firstClass;
            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, [Bind("Id,Application,Description,Entity,ITFunction,ITTower,Units,UnitPrice,SeatsPerLicense,SeatsUsed,BusinessFunction,Finance,TBMITService,TBMCategory,TBMName,EgovBRM,EntryDate,PurchaseDate,ApprovalDate,ExpirationDate,FirstClassification,SecondClassification,ThirdClassification")] ITInvestment purchaseOrder)
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
                    ITFunction itfunc = _context.ITFunction.Where(itf => itf.Name.Equals(purchaseOrder.ITFunction)).First();
                    string filteredDesc = await Classifier.Filter(purchaseOrder.Description);
                    itfunc.Unigram = itfunc.Unigram + " " + filteredDesc;
                    _context.Update(itfunc);
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
                Classifier.Train();
                return RedirectToAction(nameof(Approvals));
            }
            return View(purchaseOrder);
        }

        public IActionResult Export()
        {
            var purchaseOrders = from po in _context.PurchaseOrder
                                 select po;
            var poModels = purchaseOrders.Select(po => new ITInvestmentCSVModel(po)).ToList();

            var result = WriteCSVToMemory(poModels);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv");
        }

        private byte[] WriteCSVToMemory(IEnumerable<ITInvestmentCSVModel> orders)
        {
            using (var memorystream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memorystream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.Configuration.RegisterClassMap<ITInvestmentCSVModel.PurchaseOrderCSVMap>();
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
