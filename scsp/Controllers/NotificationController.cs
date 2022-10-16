using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers;

public class NotificationController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SCSPDataContext _context;


    public NotificationController(ILogger<HomeController> logger, SCSPDataContext context)
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
        var comments = new List<Comment>();
        var posts = _context.Post.Where(p => p.AuthorId == username).ToList();
        foreach (var post in posts)
        {
            var cms = _context.Comment.Where(c => c.PostId == post.PostID);
            foreach (var c in cms)
            {
                c.Author = _context.User.Find(c.AuthorId) ?? new Models.User();
            }
            comments.AddRange(cms);
        }
        comments.Sort((c1,c2) => c1.Time.CompareTo(c2.Time));
        comments.Reverse();
        var vm = new NotificationIndexViewModel{
            currentuser = user,
            comments = comments
        };
        return View(vm);
    }
}
