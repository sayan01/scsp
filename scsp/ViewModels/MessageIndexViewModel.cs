using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;

namespace scsp.ViewModels
{
    public class MessageIndexViewModel
    {
        public Dictionary<User, int> Users {get; set;} = new Dictionary<User, int>();
        public string errormsg {get; set;} = "";
        public User from {get; set;} = new User();
    }
}