using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tool_N_GOOD.Data;
using Tool_N_GOOD.Models;

namespace Tool_N_GOOD.Areas.Identity.Pages.Account.Manage
{
    public class UsageHistoryModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UsageHistoryModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<UsageHistory> UsageHistoryies
        {
            get
            {
                return _context.UsageHistoryies
                    .Where(u => u.UserId == _userManager.GetUserId(User))
                    .Include(u => u.Tool)
                    .ToList();
            }
        }
        public void OnGet()
        {

        }
    }
}
