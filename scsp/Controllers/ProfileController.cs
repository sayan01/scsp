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
        var followers = _context.UserUser.Where(uu => uu.Followee == user).ToList();
        var following = _context.UserUser.Where(uu => uu.Follower == user).ToList();
        ProfileIndexViewModel vm = new ProfileIndexViewModel{
            currentuser = user,
            Posts = posts,
            Followers = followers,
            Following = following,
        };
        return View(vm);
    }

    [Authorize]
    public IActionResult Explore(string id)
    {
        var identity = HttpContext.User.Identity;
        var cusername = identity != null ? identity.Name : null;
        var cuser = _context.User.FirstOrDefault(m => m.UserID == cusername);
        if(cuser == null){
            return RedirectToAction("Logout", "Authentication");
        }
        if(cusername == id)
            return RedirectToAction(nameof(Index));
        var username = id;
        var user = _context.User.FirstOrDefault(m => m.UserID == username);
        if(user == null){
            return Content("User (" + username + ") not found");
        }
        var posts = _context.Post.Where(p => p.Author == user).OrderBy(post => post.Time).Reverse().ToList();
        var followers = _context.UserUser.Where(uu => uu.Followee == user).ToList();
        var following = _context.UserUser.Where(uu => uu.Follower == user).ToList();
        bool ifollowhim = false, hefollowsme = false;
        foreach (var follower in followers)
        {
            ifollowhim = ifollowhim || (follower.Follower == cuser);
        }
        foreach (var followee in following)
        {
            hefollowsme = hefollowsme || followee.Followee == cuser;
        }
        foreach (var post in posts)
        {
            post.Likes = _context.LikePost.Where(l => l.Post == post).ToList();
            post.Dislikes = _context.DislikePost.Where(d => d.Post == post).ToList();
            post.Comments = _context.Comment.Where(c => c.Post == post).ToList();
        }
        ProfileExploreViewModel vm = new ProfileExploreViewModel{
            user = user,
            Posts = posts,
            Followers = followers,
            Following = following,
            ifollowhim = ifollowhim,
            hefollowsme = hefollowsme,
            currentuser = cuser
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
    public async Task<IActionResult> UpdateDP(string msg = "")
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = await _context.User.FindAsync(username);
            if (user == null)
            {
                return RedirectToAction("Logout", "Authentication");
            }
            ProfileUpdateDPViewModel vm = new ProfileUpdateDPViewModel{
                currentuser = user,
                AlertMsg = msg,
                AlertType = "danger"
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
            string initials = (String.IsNullOrEmpty(user.FName) ? "" : user.FName[0]) + "" + (String.IsNullOrEmpty(user.LName) ? "" : user.LName[0]);
            string photo = "https://avatars.dicebear.com/api/initials/"+initials+".svg?r=50&b=black";
            if(file != null && file.Length > 0){
                if(file.Length > 1 * 1000 * 1000) return RedirectToAction(nameof(UpdateDP), new {msg="File is too big. Max size is 1MB"});
                if(!file.FileName.ToLower().EndsWith(".png") && !file.FileName.ToLower().EndsWith(".jpg") )
                    return  RedirectToAction(nameof(UpdateDP), new {msg="Only .jpg or .png files are allowed"});
                // return RedirectToAction(nameof(UpdateDP), new {msg="Roadblock temporary"});
                using(var target = new MemoryStream()){
                    file.CopyTo(target);
                    var barray = target.ToArray();
                    photo = "data:image/;base64," + Convert.ToBase64String(barray);
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
        public IActionResult Followers(string id = ""){
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : "";
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(username == null || user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            if(String.IsNullOrEmpty(id)) id =  username;
            var targetuser = _context.User.FirstOrDefault(m => m.UserID == id);
            if(targetuser == null){
                return Content("Target User is not valid");
            }
            var useruser = _context.UserUser.Where(uu => uu.FolloweeId == id).ToList();
            foreach (var uu in useruser)
            {
                uu.Follower = _context.User.Find(uu.FollowerId) ?? new Models.User();
                uu.Followee = _context.User.Find(uu.FolloweeId) ?? new Models.User();
            }
            var vm = new ProfileFollowersViewModel{
                currentuser = user,
                targetuser = targetuser,
                Followers = useruser
            };
            return View(nameof(Followers),vm);
        }

        [Authorize]
        public IActionResult Following(string id = ""){
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : "";
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(username == null || user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            if(String.IsNullOrEmpty(id)) id =  username;
            var targetuser = _context.User.FirstOrDefault(m => m.UserID == id);
            if(targetuser == null){
                return Content("Target User is not valid");
            }
            var useruser = _context.UserUser.Where(uu => uu.FollowerId == id).ToList();
            foreach (var uu in useruser)
            {
                uu.Follower = _context.User.Find(uu.FollowerId) ?? new Models.User();
                uu.Followee = _context.User.Find(uu.FolloweeId) ?? new Models.User();
            }
            var vm = new ProfileFollowingViewModel{
                currentuser = user,
                targetuser = targetuser,
                Following = useruser
            };
            return View(nameof(Following),vm);
        }
        
        [Authorize]
        public async Task<IActionResult> Follow(string id)
        {
            var username = id;
            var identity = HttpContext.User.Identity;
            var cusername = identity != null ? identity.Name : "";
            cusername ??= "";
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
            Foll? relation = _context.UserUser.FirstOrDefault(uu => uu.Follower == cuser && uu.Followee == user);
            if(relation != null) return RedirectToAction(nameof(Unfollow), new {id = id});
            relation = new Foll{Follower = cuser, Followee = user};
            cuser.Follows.Add(relation);
            user.FollowedBy.Add(relation);
            try{
                _context.Update(user);
                _context.Update(cuser);
                await _context.SaveChangesAsync();
            }
            catch (Exception e){
                return Content("Concurrency Exception: " + e.Message);
            }
            return RedirectToAction(nameof(Explore), new {id = id});
        }

        [Authorize]
        public async Task<IActionResult> Unfollow(string id)
        {
            var username = id;
            var identity = HttpContext.User.Identity;
            var cusername = identity != null ? identity.Name : "";
            cusername ??= "";
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
            Foll? relation = _context.UserUser.FirstOrDefault(uu => uu.Follower == cuser && uu.Followee == user);
            if(relation == null) return Content("Already not following");
            cuser.Follows.Remove(relation);
            user.FollowedBy.Remove(relation);
            try{
                _context.Update(user);
                _context.Update(cuser);
                _context.Remove(relation);
                await _context.SaveChangesAsync();
            }
            catch (Exception e){
                return Content("Concurrency Exception: " + e.Message);
            }
            return RedirectToAction(nameof(Explore), new {id = id});
        }


        private bool UserExists(string id){
          return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
}
