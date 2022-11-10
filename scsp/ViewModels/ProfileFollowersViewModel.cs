using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class ProfileFollowersViewModel
    {
        public User currentuser {get; set;} = new User();
        public User targetuser {get; set;} = new User();
        public ICollection<Foll> Followers {get; set;} = new List<Foll>();
    }
}