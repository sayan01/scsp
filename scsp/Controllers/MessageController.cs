using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using scsp.Models;
using scsp.ViewModels;

namespace scsp.Controllers
{
    public class MessageController : Controller
    {
        private readonly SCSPDataContext _context;

        public MessageController(SCSPDataContext context)
        {
            _context = context;
        }

        // GET: Message
        [Authorize]
        public IActionResult Index(string errormsg="")
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            var messages = _context.Message.Where(m => m.From == user || m.To == user).ToList();
            foreach (var m in messages)
            {
                Console.WriteLine("message From:" + m.From.UserID);
                Console.WriteLine("message To:" + m.To.UserID);
                Console.WriteLine("message Content:" + m.Content);
                Console.WriteLine("message Time:" + m.Time);
                Console.WriteLine(" ");
            }
            var users = new List<User>();
            foreach (var message in messages)
            {
                User frnd;
                if( message.From == user) frnd = message.To;
                else frnd = message.From;
                if(!users.Contains(frnd)) users.Add(frnd);
            }
            var vm = new MessageIndexViewModel(){
                errormsg = errormsg,
                Users = users,
                from = user
            };
            return _context.Message != null ? View(vm) : Content("Entity set 'SCSPDataContext.Message'  is null.");
        }

        [Authorize]
        public IActionResult Send(string id, string message="", string errormsg = "")
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            var to_username = id;
            var to_user = _context.User.FirstOrDefault(m => m.UserID == to_username);
            if(to_user == null){
                return Content("User " + id + " not found");
            }
            var messages = _context.Message
            .Where(m => ( m.From == user && m.To == to_user ) || ( m.To == user && m.From == to_user ) )
            .OrderBy(m => m.Time)
            .ToList();
            var vm = new MessageSendViewModel(){
                errormsg = errormsg,
                Messages = messages,
                message = message,
                from = user,
                to =  to_user
            };
              return _context.Message != null ? View(vm) : Content("Entity set 'SCSPDataContext.Message'  is null.");
        }

        
        // POST: Message/Send
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Send(string id, string message)
        {
            var identity = HttpContext.User.Identity;
            var username = identity != null ? identity.Name : null;
            var user = _context.User.FirstOrDefault(m => m.UserID == username);
            if(user == null){
                return RedirectToAction("Logout", "Authentication");
            }
            var to_username = id;
            var to_user = _context.User.FirstOrDefault(m => m.UserID == to_username);
            if(to_user == null){
                return Content("User " + id + " not found");
            }
            if(String.IsNullOrEmpty(message)){
                return RedirectToAction(nameof(Send), new {id = id, message = message, errormsg = "Message cannot be empty"});
            }
            if(message.Length > 250){
                return RedirectToAction(nameof(Send), new {id = id, message = message, errormsg = "Message too long. Max length is 250 characters."});
            }
            Message Message = new Message{
                From = user, To = to_user, Content = message, Time = DateTime.Now
            };
            _context.Add(Message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Send), new {id = id});

        }
        private bool MessageExists(int id)
        {
          return (_context.Message?.Any(e => e.MessageID == id)).GetValueOrDefault();
        }
    }
}
