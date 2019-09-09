using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tool_N_GOOD.Data;
using Tool_N_GOOD.Models;

namespace Tool_N_GOOD.Controllers
{
    public class BrandTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BrandTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BrandTypes.ToListAsync());
        }

        // GET: BrandTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandType = await _context.BrandTypes
                .FirstOrDefaultAsync(m => m.BrandTypeId == id);
            if (brandType == null)
            {
                return NotFound();
            }

            return View(brandType);
        }

        // GET: BrandTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandTypeId,Name")] BrandType brandType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brandType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandType);
        }

        // GET: BrandTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandType = await _context.BrandTypes.FindAsync(id);
            if (brandType == null)
            {
                return NotFound();
            }
            return View(brandType);
        }

        // POST: BrandTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandTypeId,Name")] BrandType brandType)
        {
            if (id != brandType.BrandTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandTypeExists(brandType.BrandTypeId))
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
            return View(brandType);
        }

        // GET: BrandTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandType = await _context.BrandTypes
                .FirstOrDefaultAsync(m => m.BrandTypeId == id);
            if (brandType == null)
            {
                return NotFound();
            }

            return View(brandType);
        }

        // POST: BrandTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandType = await _context.BrandTypes.FindAsync(id);
            _context.BrandTypes.Remove(brandType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandTypeExists(int id)
        {
            return _context.BrandTypes.Any(e => e.BrandTypeId == id);
        }
    }
}
