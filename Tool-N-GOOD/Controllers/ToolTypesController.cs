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
    public class ToolTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToolTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ToolTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToolTypes.ToListAsync());
        }

        // GET: ToolTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolType = await _context.ToolTypes
                .FirstOrDefaultAsync(m => m.ToolTypeId == id);
            if (toolType == null)
            {
                return NotFound();
            }

            return View(toolType);
        }

        // GET: ToolTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToolTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToolTypeId,Name")] ToolType toolType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toolType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toolType);
        }

        // GET: ToolTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolType = await _context.ToolTypes.FindAsync(id);
            if (toolType == null)
            {
                return NotFound();
            }
            return View(toolType);
        }

        // POST: ToolTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToolTypeId,Name")] ToolType toolType)
        {
            if (id != toolType.ToolTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toolType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolTypeExists(toolType.ToolTypeId))
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
            return View(toolType);
        }

        // GET: ToolTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toolType = await _context.ToolTypes
                .FirstOrDefaultAsync(m => m.ToolTypeId == id);
            if (toolType == null)
            {
                return NotFound();
            }

            return View(toolType);
        }

        // POST: ToolTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toolType = await _context.ToolTypes.FindAsync(id);
            _context.ToolTypes.Remove(toolType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolTypeExists(int id)
        {
            return _context.ToolTypes.Any(e => e.ToolTypeId == id);
        }
    }
}
