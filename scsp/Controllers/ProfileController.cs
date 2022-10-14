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
        foreach (var post in posts)
        {
            post.Likes = _context.LikePost.Where(l => l.Post == post).ToList();
            post.Dislikes = _context.DislikePost.Where(d => d.Post == post).ToList();
            post.Comments = _context.Comment.Where(c => c.Post == post).ToList();
        }
        ProfileIndexViewModel vm = new ProfileIndexViewModel{
            currentuser = user,
            Posts = posts,
            Followers = user.Follows ?? new List<User>(),
            Following = user.FollowedBy ?? new List<User>(),
        };
        return View(vm);
    }

    [Authorize]
    public IActionResult Explore(string id)
    {
        var identity = HttpContext.User.Identity;
        var username = identity != null ? identity.Name : null;
        if(username == id)
            return RedirectToAction(nameof(Index));
        username = id;
        var user = _context.User.FirstOrDefault(m => m.UserID == username);
        if(user == null){
            return Content("User (" + username + ") not found");
        }
        var posts = _context.Post.Where(p => p.Author == user).OrderBy(post => post.Time).Reverse().ToList();
        foreach (var post in posts)
        {
            post.Likes = _context.LikePost.Where(l => l.Post == post).ToList();
            post.Dislikes = _context.DislikePost.Where(d => d.Post == post).ToList();
            post.Comments = _context.Comment.Where(c => c.Post == post).ToList();
        }
        ProfileExploreViewModel vm = new ProfileExploreViewModel{
            user = user,
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

    [Authorize]
    public async Task<IActionResult> UpdateDP()
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = await _context.User.FindAsync(username);
            if (user == null)
            {
                return RedirectToAction("Logout", "Authentication");
            }
            ProfileUpdateDPViewModel vm = new ProfileUpdateDPViewModel{
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
        public async Task<IActionResult> UpdateDP(string username, IFormFile file)
        {
            var identity = HttpContext.User.Identity;
            var cusername = identity != null ? identity.Name : null;
            var user = await _context.User.FindAsync(username);
            if (user == null || cusername != username){
                return RedirectToAction("Logout", "Authentication");
            }
            string photo = "";
            if(file != null && file.Length > 0){
                using(var target = new MemoryStream()){
                    file.CopyTo(target);
                    var barray = target.ToArray();
                    photo = Convert.ToBase64String(barray);
                }
            }
            user.Photo = photo;
            try{
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException){
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Follow(string id)
        {
            var username = id;
            var identity = HttpContext.User.Identity;
            var cusername = identity != null ? identity.Name : null;
            var user = await _context.User.FirstOrDefaultAsync(e => e.UserID == username);
            var cuser = await _context.User.FirstOrDefaultAsync(e => e.UserID == cusername);
            if (cuser == null){
                return RedirectToAction("Logout", "Authentication");
            }
            if(user == null){
                return Content("User " + username + "does not exist");
            }
            if(cuser == user){
                return Content("Cannot follow ownself");
            }
            cuser.Follows.Add(user);
            user.FollowedBy.Add(cuser);
            try{
                _context.Update(user);
                _context.Update(cuser);
                await _context.SaveChangesAsync();
            }
            catch (Exception e){
                return Content("Concurrency Exception: " + e.Message);
            }
            return RedirectToAction(nameof(Index));
        }


        private bool UserExists(string id){
          return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
}
