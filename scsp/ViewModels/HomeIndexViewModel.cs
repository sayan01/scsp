using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class HomeIndexViewModel
    {
        public User currentuser {get; set;} = new User();
        public ICollection<Post> Posts {get; set;} = new List<Post>(); // posts of friends
        public ICollection<Foll> Followers {get; set;} = new List<Foll>();
        public ICollection<Foll> Following {get; set;} = new List<Foll>();
    }
}