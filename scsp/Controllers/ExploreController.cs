using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers;

public class ExploreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SCSPDataContext _context;


    public ExploreController(ILogger<HomeController> logger, SCSPDataContext context)
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
        var users = _context.User.ToList();
        users.Sort((a,b) => a.UserID.CompareTo(b.UserID));
        ExploreIndexViewModel vm = new ExploreIndexViewModel{
            Users = users,
            currentuser = user
        };
        return View(vm);
    }
    [Authorize]
    [HttpPost]
    public IActionResult Index(string query)
    {
        query ??= "";
        query = query.ToLower();
        var identity = HttpContext.User.Identity;
        var username = identity != null ? identity.Name : null;
        var user = _context.User.FirstOrDefault(m => m.UserID == username);
        if(user == null){
            return RedirectToAction("Logout", "Authentication");
        }
        ExploreIndexViewModel vm = new ExploreIndexViewModel{
            query = query,
            currentuser = user,
            Users = _context.User.Where(u => u.UserID.ToLower().Contains(query) || u.FName.ToLower().Contains(query) || (u.LName??"").ToLower().Contains(query)).ToList()
        };
        return View(vm);
    }
}
