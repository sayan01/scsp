using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;

namespace scsp.ViewModels
{
    public class ExploreIndexViewModel
    {
        public string query {get; set;} = "";
        public List<User> Users {get; set;} = new List<User>();
        public string AlertMsg {get; set; } = "";
        public string AlertType {get; set;} = "";
        public User currentuser {get; set;} = new User();
        
    }
}