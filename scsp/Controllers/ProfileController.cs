using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SCSPDataContext _context;


    public ProfileController(ILogger<HomeController> logger, SCSPDataContext context)
    {
        _logger = logger;
        _context = context;

    }
    [Authorize]
    public IActionResult Index()
    {
        var identity = HttpContext.User.Identity;
        var username = identity != null ? identity.Name : null;
        var user = _context.User.FirstOrDefault(m => m.UserID == username);
        if(user == null){
            return RedirectToAction("Logout", "Authentication");
        }
        var posts = _context.Post.Where(p => p.Author == user).OrderBy(post => post.Time).Reverse().ToList();
        ProfileIndexViewModel vm = new ProfileIndexViewModel{
            currentuser = user,
            Posts = posts,
            Followers = user.Follows ?? new List<User>(),
            Following = user.FollowedBy ?? new List<User>(),
        };
        return View(vm);
    }

    // edit profile
    
    [Authorize]
    public async Task<IActionResult> Edit()
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = await _context.User.FindAsync(username);
            if (user == null)
            {
                return RedirectToAction("Logout", "Authentication");
            }
            ProfileEditViewModel vm = new ProfileEditViewModel{
                currentuser = user
            };
            return View(vm);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string username, string fname, string lname, string bio)
        {
            var identity = HttpContext.User.Identity;
            var cusername = identity != null ? identity.Name : null;
            var user = await _context.User.FindAsync(username);
            if (user == null || cusername != username){
                return RedirectToAction("Logout", "Authentication");
            }
            user.FName = fname;
            user.LName = lname;
            user.Bio = bio;
            try{
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(string id){
          return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
}
