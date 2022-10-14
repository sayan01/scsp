using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class ProfileExploreViewModel
    {
        public User user {get; set;} = new User();
        public ICollection<Post> Posts {get; set;} = new List<Post>();
        public ICollection<Foll> Followers {get; set;} = new List<Foll>();
        public ICollection<Foll> Following {get; set;} = new List<Foll>();
        public bool ifollowhim {get; set;}
        public bool hefollowsme {get; set;}
    }
}