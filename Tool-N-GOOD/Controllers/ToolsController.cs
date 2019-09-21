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
    public class ToolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GoodTools
        public async Task<IActionResult> Index()
        {
            var tools = _context.Tools
                .Include(t => t.BrandType)
                .Include(t => t.MeasurementType)
                .Include(t => t.ToolType)
                .Include(t => t.User)
                .Include(t => t.UsageHistories)
                .Where(t => t.Serviceable == true);
            return View(await tools.ToListAsync());

            //foreach(var tool in tools)
            //{

            //}



        }

        // GET: BAdTools
        public async Task<IActionResult> BadTool()
        {

            var applicationDbContext = _context.Tools.Include(t => t.BrandType).Include(t => t.MeasurementType).Include(t => t.ToolType).Include(t => t.User).Where(t => t.Serviceable == false);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.BrandType)
                .Include(t => t.MeasurementType)
                .Include(t => t.ToolType)
                .Include(t => t.User)


                .FirstOrDefaultAsync(m => m.ToolId == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // GET: Tools/Create
        public IActionResult Create()
        {
            ViewData["BrandTypeId"] = new SelectList(_context.BrandTypes, "BrandTypeId", "Name");
            ViewData["MeasurementTypeId"] = new SelectList(_context.MeasurementTypes, "MeasurementTypeId", "Type");
            ViewData["ToolTypeId"] = new SelectList(_context.ToolTypes, "ToolTypeId", "Name");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "FullName");
            return View();
        }

        // POST: Tools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToolId,Name,Description,Measurement,Serviceable,BrandTypeId,ToolTypeId,UserId,MeasurementTypeId")] Tool tool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandTypeId"] = new SelectList(_context.BrandTypes, "BrandTypeId", "Name", tool.BrandTypeId);
            ViewData["MeasurementTypeId"] = new SelectList(_context.MeasurementTypes, "MeasurementTypeId", "Type", tool.MeasurementTypeId);
            ViewData["ToolTypeId"] = new SelectList(_context.ToolTypes, "ToolTypeId", "Name", tool.ToolTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", tool.UserId);
            return View(tool);
        }

        // GET: Tools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools.FindAsync(id);
            if (tool == null)
            {
                return NotFound();
            }
            ViewData["BrandTypeId"] = new SelectList(_context.BrandTypes, "BrandTypeId", "Name", tool.BrandTypeId);
            ViewData["MeasurementTypeId"] = new SelectList(_context.MeasurementTypes, "MeasurementTypeId", "Type", tool.MeasurementTypeId);
            ViewData["ToolTypeId"] = new SelectList(_context.ToolTypes, "ToolTypeId", "Name", tool.ToolTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "FullName", tool.UserId);
            return View(tool);
        }

        // POST: Tools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToolId,Name,Description,Measurement,Serviceable,BrandTypeId,ToolTypeId,UserId,MeasurementTypeId")] Tool tool)
        {
            if (id != tool.ToolId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolExists(tool.ToolId))
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
            ViewData["BrandTypeId"] = new SelectList(_context.BrandTypes, "BrandTypeId", "Name", tool.BrandTypeId);
            ViewData["MeasurementTypeId"] = new SelectList(_context.MeasurementTypes, "MeasurementTypeId", "Type", tool.MeasurementTypeId);
            ViewData["ToolTypeId"] = new SelectList(_context.ToolTypes, "ToolTypeId", "Name", tool.ToolTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", tool.UserId);
            return View(tool);
        }

        // GET: Tools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tool = await _context.Tools
                .Include(t => t.BrandType)
                .Include(t => t.MeasurementType)
                .Include(t => t.ToolType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.ToolId == id);
            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        // POST: Tools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tool = await _context.Tools.FindAsync(id);
            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolExists(int id)
        {
            return _context.Tools.Any(e => e.ToolId == id);
        }
    }
}
