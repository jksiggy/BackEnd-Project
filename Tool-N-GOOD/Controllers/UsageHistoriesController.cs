using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tool_N_GOOD.Data;
using Tool_N_GOOD.Models;

namespace Tool_N_GOOD.Controllers
{
    public class UsageHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsageHistoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: UsageHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UsageHistoryies.Include(u => u.Tool).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UsageHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usageHistory = await _context.UsageHistoryies
                .Include(u => u.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UsageHistoryId == id);
            if (usageHistory == null)
            {
                return NotFound();
            }

            return View(usageHistory);
        }

        // GET: UsageHistories/Create
        public async Task<IActionResult> Create(int? id)
        {
            var tool = await _context.Tools
                .FirstOrDefaultAsync(t => t.ToolId == id);

            var usageHistory = new UsageHistory();
            usageHistory.Tool = tool;


            return View(usageHistory);
        }

        // POST: UsageHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("UsageHistoryId,UserId,ToolId,TaskFor,Inspection,Serviceable,CheckoutTime,ExpectedReturn,PromiseReturn")] UsageHistory usageHistory)
        {
            //UsageHistory.UserId = user.Id;
            ModelState.Remove("CheckoutTime");
            ModelState.Remove("PromiseReturn");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                var tool = await _context.Tools
                .FirstOrDefaultAsync(t => t.ToolId == id);

                usageHistory.UserId = user.Id;
                usageHistory.ToolId = tool.ToolId;
                _context.Add(usageHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ToolId"] = new SelectList(_context.Tools, "ToolId", "Description", usageHistory.ToolId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", usageHistory.UserId);
            return View(usageHistory);
        }




        // GET: UsageHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var usageHistory = await _context.UsageHistoryies.FindAsync(id);
            if (usageHistory == null)
            {
                return NotFound();
            }
            ViewData["ToolId"] = new SelectList(_context.Tools, "ToolId", "Description", usageHistory.ToolId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", usageHistory.UserId);
            return View(usageHistory);
        }

        // POST: UsageHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsageHistoryId,UserId,ToolId,TaskFor,Inspection,Serviceable,CheckoutTime,ExpectedReturn,PromiseReturn")] UsageHistory usageHistory)
        {
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await GetCurrentUserAsync();
                    usageHistory.UserId = user.Id;
                    _context.Update(usageHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsageHistoryExists(usageHistory.UsageHistoryId))
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
            ViewData["ToolId"] = new SelectList(_context.Tools, "ToolId", "Description", usageHistory.ToolId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", usageHistory.UserId);
            return View(usageHistory);
        }



        // GET: UsageHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usageHistory = await _context.UsageHistoryies
                .Include(u => u.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UsageHistoryId == id);
            if (usageHistory == null)
            {
                return NotFound();
            }

            return View(usageHistory);
        }

        // POST: UsageHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usageHistory = await _context.UsageHistoryies.FindAsync(id);
            _context.UsageHistoryies.Remove(usageHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsageHistoryExists(int id)
        {
            return _context.UsageHistoryies.Any(e => e.UsageHistoryId == id);
        }

        private Task<ApplicationUser> GetUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        private async Task<bool> WasCreatedByUser(UsageHistory usageHistory)
        {
            var user = await GetUserAsync();
            return usageHistory.UserId == user.Id;
        }
    }
}
