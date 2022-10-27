using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using scsp.Models;
using scsp.ViewModels;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace scsp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SCSPDataContext _context;

         public AuthenticationController(SCSPDataContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string message = "", string alert = "danger")
        {
            AuthLoginViewModel vm = new AuthLoginViewModel(){
                Title = "Login",
                AlertMsg = message,
                AlertType = alert ,
                password = "",
                User = new User()
            };
            return View("Login",vm);
        }
        public IActionResult Register(string message, string alert = "danger")
        {
            AuthRegisterViewModel vm = new AuthRegisterViewModel(){
                Title = "Register",
                AlertMsg = message,
                AlertType = alert,
                password = "",
                User = new User()
            };
            return View("Register", vm);
        }
        public IActionResult Error(string title, string details)
        {
            return View("Error", new ErrorViewModel { Title = title, Details = details });
        }
        // POST: Authentication/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string fname, string lname, string bio, string password)
        {
            if(username == null)                        return RedirectToAction(nameof(Register), new { message = "username cannot be empty" });
            if(password == null)                        return RedirectToAction(nameof(Register), new { message = "password cannot be empty"});
            if(fname == null)                           return RedirectToAction(nameof(Register), new { message = "First Name cannot be empty"});
            if(username.Length > 32)                    return RedirectToAction(nameof(Register), new { message = "Username cannot be greater than 32 characters"});
            if(password.Length < 8)                     return RedirectToAction(nameof(Register), new { message = "Password cannot be smaller than 8 characters"});
            if(fname.Length > 32)                       return RedirectToAction(nameof(Register), new { message = "First Name cannot be greater than 32 characters"});
            if(lname != null && lname.Length > 32)      return RedirectToAction(nameof(Register), new { message = "Last Name cannot be greater than 32 characters"});
            if(bio != null && bio.Length > 250)         return RedirectToAction(nameof(Register), new { message = "Bio cannot be greater than 250 characters"});
            
            User user = new User();

            user.UserID = username;
            user.FName = fname;
            user.LName = lname ?? "";
            user.Bio = bio ?? "";
            string initials = (String.IsNullOrEmpty(fname) ? "" : fname[0]) + "" + (String.IsNullOrEmpty(lname) ? "" : lname[0]);
            user.Photo = "https://avatars.dicebear.com/api/initials/"+initials+".svg?r=50&b=black";
            user.Follows = new List<Foll>();
            user.FollowedBy = new List<Foll>();
            user.Posts = new List<Post>();
            user.Donations = new List<Donation>();
            user.MessagesSent = new List<Message>();
            user.MessagesRecieved = new List<Message>();

            byte[] passwordbytes = System.Text.Encoding.UTF8.GetBytes(username + password);
            byte[] result;
            SHA512 shaM = SHA512.Create();
            result = shaM.ComputeHash(passwordbytes);
            string passwordhash = Convert.ToBase64String(result);

            user.PasswordHash = passwordhash;

            try
            {
                var userifalreadyexists = _context.User.FirstOrDefault(m => m.UserID == username);
                if(userifalreadyexists == null){
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Login), new { message = "Registration successful", alert = "success"});
                }
                else{
                    return RedirectToAction(nameof(Register), new { message = "User with the username (" + username + ") already exists, please choose some other username."});
                }
                
            }
            catch(Exception e){
                Console.WriteLine("Invalid");
                Console.WriteLine(e);
                return Error("Registration Error", "Error occured while registering");
            }
        }

        // POST: Authentication/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string remember)
        {
            if(username == null)   return RedirectToAction( nameof(Login), new { message = "Username cannot be empty" });
            if(password == null)   return RedirectToAction( nameof(Login), new { message = "Password cannot be empty" });
            Console.WriteLine("Remember Me:" + remember);
            string passwordhash;
            // finding the password hash from username+password
            {
                byte[] passwordbytes = System.Text.Encoding.UTF8.GetBytes(username + password);
                byte[] result;
                SHA512 shaM = SHA512.Create();
                result = shaM.ComputeHash(passwordbytes);
                passwordhash = Convert.ToBase64String(result);
            }

            try
            {
                var user = _context.User.FirstOrDefault(m => m.UserID == username);
                if(user == null || user.PasswordHash != passwordhash){
                    return RedirectToAction( nameof(Login), new { message = "Incorrect Username or Password" });
                }
                else{
                    var claims = new List<Claim>{ new Claim(ClaimTypes.Name, username) };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties{ 
                        AllowRefresh = true, 
                        IsPersistent = true, 
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(remember == "on" ? 86400 : 30)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);
                    return RedirectToAction("Index", username == "admin" ? "Admin" : "Home");
                }
                
            }
            catch(Exception e){
                Console.WriteLine("Invalid");
                Console.WriteLine(e);
                return Error("Login Error", "Error occured while loging in");
            }
        }
        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}