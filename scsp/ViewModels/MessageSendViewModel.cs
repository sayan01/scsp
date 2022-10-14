using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;

namespace scsp.ViewModels
{
    public class MessageSendViewModel
    {
        public ICollection<Message> Messages {get; set;} = new List<Message>();
        public string message {get; set;} = "";
        public string errormsg {get; set;} = "";
        public User from {get; set;} = new User();
        public User to {get; set;} = new User();
    }
}