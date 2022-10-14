﻿using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SCSPDataContext _context;


    public HomeController(ILogger<HomeController> logger, SCSPDataContext context)
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
        var followers = _context.UserUser.Where(uu => uu.Followee == user).ToList();
        var following = _context.UserUser.Where(uu => uu.Follower == user).ToList();
        var posts = _context.Post.ToList();
        var frndposts = new List<Post>();
        foreach (var post in posts)
        {
            foreach (var frnd in following)
            {
                if(post.AuthorId == frnd.FolloweeId){
                    post.Author = _context.User.FirstOrDefault(u => u.UserID == post.AuthorId) ?? post.Author;
                    var Comments = _context.Comment.Where(c => c.Post == post).ToList() ?? new List<Comment>();
                    foreach (var Comment in Comments){
                        Comment.Author = _context.User.FirstOrDefault(u => u.UserID == Comment.AuthorId) ?? new User();
                    }
                    post.Comments = Comments;
                    frndposts.Add(post);
                    break;
                }
            }
        }
        HomeIndexViewModel vm = new HomeIndexViewModel{
            currentuser = user,
            Followers = followers,
            Following = following,
            Posts = frndposts
        };
        return View(vm);
    }
}
