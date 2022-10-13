using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class ProfileIndexViewModel
    {
        public User currentuser {get; set;} = new User();
        public ICollection<Post> Posts {get; set;} = new List<Post>();
        public ICollection<User> Followers {get; set;} = new List<User>();
        public ICollection<User> Following {get; set;} = new List<User>();
    }
}