using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tool_N_GOOD.Data;
using Tool_N_GOOD.Models;

namespace Tool_N_GOOD.Areas.Identity.Pages.Account.Manage
{
    public class UsageHistoryModel : PageModel
    {
        private readonly Tool_N_GOOD.Data.ApplicationDbContext _context;

        public UsageHistoryModel(Tool_N_GOOD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<UsageHistory> UsageHistory { get;set; }

        public async Task OnGetAsync()
        {
            UsageHistory = await _context.UsageHistoryies
                .Include(u => u.Tool)
                .Include(u => u.User).ToListAsync();
        }
    }
}
