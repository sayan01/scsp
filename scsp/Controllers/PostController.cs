using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers
{
    public class PostController : Controller
    {
        private readonly SCSPDataContext _context;

        public PostController(SCSPDataContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
              return _context.Post != null ? 
                          View(await _context.Post.ToListAsync()) :
                          Problem("Entity set 'SCSPDataContext.Post'  is null.");
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.PostID == id);
            if(post == null){
                return Content("Post does not exist");
            }
            post.Author = _context.User.FirstOrDefault(u => u.UserID == post.AuthorId) ?? new User();
            var Comments = _context.Comment.Where(c => c.Post == post).ToList() ?? new List<Comment>();
            foreach (var Comment in Comments)
            {
                Comment.Author = _context.User.FirstOrDefault(u => u.UserID == Comment.AuthorId) ?? new User();
            }
            post.Comments = Comments;
            return View(post);
        }

        // GET: Post/Create
        [Authorize]
        public IActionResult Create(string message, string alert = "danger")
        {
            PostCreateViewModel vm = new PostCreateViewModel(){
                AlertMsg = message,
                AlertType = alert,
            };
            return View(vm);
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string content, IFormFile file)
        {
            if(String.IsNullOrEmpty(content)){
                return Create("Content cannot be empty");
            }
            string photo = "";
            if(file != null && file.Length > 0){
                using(var target = new MemoryStream()){
                    file.CopyTo(target);
                    var barray = target.ToArray();
                    photo = Convert.ToBase64String(barray);
                }
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            Post post = new Post{
                Content = content,
                Author = user,
                Time = DateTime.Now,
                Photo = photo
            };
            try{
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Home");
            }catch (Exception e){
                Console.WriteLine(e);
                PostCreateViewModel vm = new PostCreateViewModel{
                    AlertMsg = "Something Went Wrong\n" + e.Message,
                    Content = content
                };
                return View(vm);
            }
        }

        
        // GET: Post/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            if(post.Author != user){
                return Unauthorized();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'SCSPDataContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            if(post.Author != user){
                return Unauthorized();
            }
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        private bool PostExists(int id)
        {
          return (_context.Post?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
