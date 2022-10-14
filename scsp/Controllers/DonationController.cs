using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers
{
    public class DonationController : Controller
    {
        private readonly SCSPDataContext _context;

        public DonationController(SCSPDataContext context)
        {
            _context = context;
        }

        // GET: Donation
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
              return _context.Donation != null ? 
                          View(await _context.Donation.Where(d => d.User == user).ToListAsync()) :
                          Content("Entity set 'SCSPDataContext.Donation'  is null.");
        }

        
        // GET: Donation/Create
        public IActionResult Create(string message = "", string alert = "", double amount = 0.0)
        {
            DonationCreateViewModel vm = new DonationCreateViewModel(){
                AlertMsg = message,
                AlertType = alert,
                Amount = amount,
            };
            return View(vm);
        }

        // POST: Donation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(double amount)
        {
            if(amount <= 0){
                return Create("Amount cannot be negative or zero", "danger", amount);
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            Donation donation = new Donation{
                User = user,
                Amount = amount,
                Time = DateTime.Now
            };
            _context.Add(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool DonationExists(int id)
        {
          return (_context.Donation?.Any(e => e.DonationID == id)).GetValueOrDefault();
        }
    }
}
