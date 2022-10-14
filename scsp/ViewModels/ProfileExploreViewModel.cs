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
        public ICollection<Relation> Followers {get; set;}
        public ICollection<Relation> Following {get; set;} 
    }
}