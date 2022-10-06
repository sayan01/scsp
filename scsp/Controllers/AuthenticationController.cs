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
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        // POST: Authentication/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string fname, string lname, string bio, string password)
        {
            User user = new User();
            Console.WriteLine(username);
            Console.WriteLine(password);
            Console.WriteLine(fname);
            Console.WriteLine(lname);
            Console.WriteLine(bio);

            user.UserID = username;
            user.FName = fname;
            user.LName = lname;
            user.Bio = bio;

            byte[] passwordbytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] result;
            SHA512 shaM = SHA512.Create();
            result = shaM.ComputeHash(passwordbytes);
            string passwordhash = System.Text.Encoding.ASCII.GetString(result);

            user.PasswordHash = passwordhash;

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

    }
}