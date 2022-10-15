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
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            post.Author = _context.User.FirstOrDefault(u => u.UserID == post.AuthorId) ?? new User();
            var Comments = _context.Comment.Where(c => c.Post == post).ToList() ?? new List<Comment>();
            foreach (var Comment in Comments)
            {
                Comment.Author = _context.User.FirstOrDefault(u => u.UserID == Comment.AuthorId) ?? new User();
                var LikesComment = _context.LikeComment.Where( lc => lc.Comment == Comment).ToList() ?? new List<LikeComment>();
                var DislikesComment = _context.DislikeComment.Where( dlc => dlc.Comment == Comment).ToList() ?? new List<DislikeComment>();
                Comment.Likes = LikesComment;
                Comment.Dislikes = DislikesComment;
            }
            var Likes = _context.LikePost.Where( lp => lp.Post == post).ToList() ?? new List<LikePost>();
            var Dislikes = _context.DislikePost.Where( dlp => dlp.Post == post).ToList() ?? new List<DislikePost>();
            bool likedbyme = false, dislikedbyme = false;
            foreach (var Like in Likes)
            {
                Like.Author = _context.User.FirstOrDefault(u => u.UserID == Like.AuthorId) ?? new User();
                likedbyme = likedbyme || Like.Author == user;
            }
            foreach (var Dislike in Dislikes)
            {
                Dislike.Author = _context.User.FirstOrDefault(u => u.UserID == Dislike.AuthorId) ?? new User();
                dislikedbyme = dislikedbyme || Dislike.Author == user;
            }
            Comments.Sort((a,b) => 
            HelperFunctions.confidence(a.Likes.Count,a.Dislikes.Count)
            .CompareTo(HelperFunctions.confidence(b.Likes.Count, b.Dislikes.Count)));
            Comments.Reverse();
            post.Comments = Comments;
            post.Likes = Likes;
            post.Dislikes = Dislikes;

            var vm = new PostDetailsViewModel{
                Post = post,
                AlertMsg = "",
                AlertType = "",
                likedbyme = likedbyme,
                dislikedbyme = dislikedbyme,
            };

            return View(vm);
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
        [Authorize]
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

        // GET: Post/Like/5
        [Authorize]
        public async Task<IActionResult> Like(int id)
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
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            DislikePost? dlp = _context.DislikePost.FirstOrDefault(dlp => dlp.Post == post && dlp.Author == user);
            if(dlp != null){
                post.Dislikes.Remove(dlp);
                _context.Post.Update(post);
                _context.DislikePost.Remove(dlp);
                await _context.SaveChangesAsync();
            }
            LikePost? lp = _context.LikePost.FirstOrDefault(lp => lp.Post == post && lp.Author == user);
            if(lp != null){
                return RedirectToAction(nameof(UnLike), new {id = id});
            }
            lp = new LikePost{
                Author = user,
                AuthorId = username,
                Post = post
            };
            _context.LikePost.Add(lp);
            await _context.SaveChangesAsync();
            post.Likes.Add(lp);
            _context.Post.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Post", new {id = id});
        }

        // GET: Post/Dislike/5
        [Authorize]
        public async Task<IActionResult> Dislike(int id)
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
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            LikePost? lp = _context.LikePost.FirstOrDefault(lp => lp.Post == post && lp.Author == user);
            if(lp != null){
                post.Likes.Remove(lp);
                _context.Post.Update(post);
                _context.LikePost.Remove(lp);
                await _context.SaveChangesAsync();
            }
            DislikePost? dlp = _context.DislikePost.FirstOrDefault(dlp => dlp.Post == post && dlp.Author == user);
            if(dlp != null){
                return RedirectToAction(nameof(UnDislike), new {id = id});
            }
            dlp = new DislikePost{
                Author = user,
                AuthorId = username,
                Post = post
            };
            _context.DislikePost.Add(dlp);
            await _context.SaveChangesAsync();
            post.Dislikes.Add(dlp);
            _context.Post.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Post", new {id = id});
        }

        // GET: Post/UnLike/5
        [Authorize]
        public async Task<IActionResult> UnLike(int id)
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
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            LikePost? lp = _context.LikePost.FirstOrDefault(lp => lp.Post == post && lp.Author == user);
            if(lp != null){
                post.Likes.Remove(lp);
                _context.Post.Update(post);
                _context.LikePost.Remove(lp);
                await _context.SaveChangesAsync();
            }
            else return RedirectToAction(nameof(Like), new {id = id});
            return RedirectToAction(nameof(Details), "Post", new {id = id});
        }

        // GET: Post/UnDislike/5
        [Authorize]
        public async Task<IActionResult> UnDislike(int id)
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
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            DislikePost? dlp = _context.DislikePost.FirstOrDefault(dlp => dlp.Post == post && dlp.Author == user);
            if(dlp != null){
                post.Dislikes.Remove(dlp);
                _context.Post.Update(post);
                _context.DislikePost.Remove(dlp);
                await _context.SaveChangesAsync();
            } else return RedirectToAction(nameof(Dislike), new {id = id});
            return RedirectToAction(nameof(Details), "Post", new {id = id});
        }



        private bool PostExists(int id)
        {
          return (_context.Post?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
