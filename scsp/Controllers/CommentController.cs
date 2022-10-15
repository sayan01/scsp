using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scsp.Models;

namespace scsp.Controllers
{
    public class CommentController : Controller
    {
        private readonly SCSPDataContext _context;

        public CommentController(SCSPDataContext context)
        {
            _context = context;
        }

        // GET: Comment
        [Authorize]
        public async Task<IActionResult> Index()
        {
              return _context.Comment != null ? 
                          View(await _context.Comment.ToListAsync()) :
                          Problem("Entity set 'SCSPDataContext.Comment'  is null.");
        }

        // GET: Comment/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Create
        // public IActionResult Create()
        // {
        //     return View();
        // }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string comment)
        {
            if(String.IsNullOrEmpty(comment)){
                return Content("Comment cannot be empty");
            }
            if(comment.Length > 250){
                return Content("Comment cannot be longer than 250 characters");
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            var post = _context.Post.FirstOrDefault(p => p.PostID == id);
            if(post == null){
                return Content("Post does not exist");
            }
            var Comment = new Comment{
                Post = post,
                Content = comment,
                Author = user,
                Time = DateTime.Now
            };
            _context.Add(Comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Post", new {id = id});
        }

        // GET: Comment/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentID,Content,Time")] Comment comment)
        {
            if (id != comment.CommentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.CommentID))
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
            return View(comment);
        }

        // GET: Comment/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comment == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'SCSPDataContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment != null)
            {
                _context.Comment.Remove(comment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    // GET: Comment/Like/5
        [Authorize]
        public async Task<IActionResult> Like(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'SCSPDataContext.Comment'  is null.");
            }
            var comment = _context.Comment.FirstOrDefault(c=>c.CommentID == id);
            if (comment == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            DislikeComment? dlp = _context.DislikeComment.FirstOrDefault(dlp => dlp.Comment == comment && dlp.Author == user);
            if(dlp != null){
                comment.Dislikes.Remove(dlp);
                _context.Comment.Update(comment);
                _context.DislikeComment.Remove(dlp);
                await _context.SaveChangesAsync();
            }
            LikeComment? lp = _context.LikeComment.FirstOrDefault(lp => lp.Comment == comment && lp.Author == user);
            if(lp != null){
                return RedirectToAction(nameof(UnLike), new {id = id});
            }
            lp = new LikeComment{
                Author = user,
                AuthorId = username,
                Comment = comment
            };
            _context.LikeComment.Add(lp);
            await _context.SaveChangesAsync();
            comment.Likes.Add(lp);
            _context.Comment.Update(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Post", new {id = comment.PostId});
        }

        // GET: Comment/Dislike/5
        [Authorize]
        public async Task<IActionResult> Dislike(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'SCSPDataContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            LikeComment? lp = _context.LikeComment.FirstOrDefault(lp => lp.Comment == comment && lp.Author == user);
            if(lp != null){
                comment.Likes.Remove(lp);
                _context.Comment.Update(comment);
                _context.LikeComment.Remove(lp);
                await _context.SaveChangesAsync();
            }
            DislikeComment? dlp = _context.DislikeComment.FirstOrDefault(dlp => dlp.Comment == comment && dlp.Author == user);
            if(dlp != null){
                return RedirectToAction(nameof(UnDislike), new {id = id});
            }
            dlp = new DislikeComment{
                Author = user,
                AuthorId = username,
                Comment = comment
            };
            _context.DislikeComment.Add(dlp);
            await _context.SaveChangesAsync();
            comment.Dislikes.Add(dlp);
            _context.Comment.Update(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), "Post", new {id = comment.PostId});
        }

        // GET: Comment/UnLike/5
        [Authorize]
        public async Task<IActionResult> UnLike(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'SCSPDataContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            LikeComment? lp = _context.LikeComment.FirstOrDefault(lp => lp.Comment == comment && lp.Author == user);
            if(lp != null){
                comment.Likes.Remove(lp);
                _context.Comment.Update(comment);
                _context.LikeComment.Remove(lp);
                await _context.SaveChangesAsync();
            }
            else return RedirectToAction(nameof(Like), new {id = id});
            return RedirectToAction(nameof(Details), "Post", new {id = comment.PostId});
        }

        // GET: Comment/UnDislike/5
        [Authorize]
        public async Task<IActionResult> UnDislike(int id)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'SCSPDataContext.Comment'  is null.");
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null){
                return NotFound();
            }
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null || username == null){
                return RedirectToAction("Logout", "Authentication");
            }
            DislikeComment? dlp = _context.DislikeComment.FirstOrDefault(dlp => dlp.Comment == comment && dlp.Author == user);
            if(dlp != null){
                comment.Dislikes.Remove(dlp);
                _context.Comment.Update(comment);
                _context.DislikeComment.Remove(dlp);
                await _context.SaveChangesAsync();
            } else return RedirectToAction(nameof(Dislike), new {id = id});
            return RedirectToAction(nameof(Details), "Post", new {id = comment.PostId});
        }




        private bool CommentExists(int id)
        {
          return (_context.Comment?.Any(e => e.CommentID == id)).GetValueOrDefault();
        }
    }
}
