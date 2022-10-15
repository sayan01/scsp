using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SCSPDataContext _context;


    public AdminController(ILogger<HomeController> logger, SCSPDataContext context)
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
        if(username != "admin"){
            return Content("Not Authorized");
        }
        var vm = new AdminIndexViewModel{
            comments = _context.Comment.Count(),
            posts = _context.Post.Count(),
            registrations = _context.User.Count(),
            messages = _context.Message.Count(),
            likes = _context.LikePost.Count(),
            dislikes = _context.DislikePost.Count(),
            donations = _context.Donation.Count(),
            donationTotal = _context.Donation.Sum(i => i.Amount),
        };
        return View(vm);
    }
}
