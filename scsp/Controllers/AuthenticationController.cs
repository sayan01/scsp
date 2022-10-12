using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using scsp.Models;
using System.Security.Cryptography;


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
        public IActionResult Login(string message)
        {
            if (message != null){
                ViewData["message"] = message;
            }
            return View();
        }
        public IActionResult Register(string message, string alert = "danger")
        {
            if (message != null){
                ViewData["message"] = message;
                ViewData["alert"] = alert;
            }
            return View();
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
            if(username == null)                        return Register("username cannot be empty");
            if(password == null)                        return Register("password cannot be empty");
            if(fname == null)                           return Register("First Name cannot be empty");
            if(username.Length > 32)                    return Register("Username cannot be greater than 32 characters");
            if(password.Length < 8)                     return Register("Password cannot be smaller than 8 characters");
            if(fname.Length > 32)                       return Register("First Name cannot be greater than 32 characters");
            if(lname != null && lname.Length > 32)      return Register("Last Name cannot be greater than 32 characters");
            if(bio != null && bio.Length > 250)         return Register("Bio cannot be greater than 250 characters");
            
            User user = new User();

            user.UserID = username;
            user.FName = fname;
            user.LName = lname;
            user.Bio = bio;

            byte[] passwordbytes = System.Text.Encoding.UTF8.GetBytes(username + password);
            byte[] result;
            SHA512 shaM = SHA512.Create();
            result = shaM.ComputeHash(passwordbytes);
            string passwordhash = System.Text.Encoding.ASCII.GetString(result);

            user.PasswordHash = passwordhash;

            try
            {
                var userifalreadyexists = _context.User.FirstOrDefault(m => m.UserID == username);
                if(userifalreadyexists == null){
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return Register("Registration successful", "success");
                }
                else{
                    return Register("User with the username (" + username + ") already exists, please choose some other username.");
                }
                
            }
            catch(Exception e){
                Console.WriteLine("Invalid");
                Console.WriteLine(e);
                return Register("Error occured while registering");
            }
        }

        // POST: Authentication/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if(username == null)   return Login("Username cannot be empty");
            if(password == null)   return Login("Password cannot be empty");
            
            string passwordhash;
            // finding the password hash from username+password
            {
                byte[] passwordbytes = System.Text.Encoding.UTF8.GetBytes(username + password);
                byte[] result;
                SHA512 shaM = SHA512.Create();
                result = shaM.ComputeHash(passwordbytes);
                passwordhash = System.Text.Encoding.ASCII.GetString(result);
            }

            try
            {
                var user = _context.User.FirstOrDefault(m => m.UserID == username);
                if(user == null || user.PasswordHash != passwordhash){
                    return Login("Incorrect Username or Password");
                }
                else{
                    return Login("Login Successful");
                }
                
            }
            catch(Exception e){
                Console.WriteLine("Invalid");
                Console.WriteLine(e);
                return Login("Error occured while loging in");
            }
        }
    }
}