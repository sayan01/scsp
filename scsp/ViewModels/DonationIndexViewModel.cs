using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;

namespace scsp.ViewModels
{
    public class DonationIndexViewModel
    {
        public string AlertMsg {get; set;} = "";
        public string AlertType {get; set;} = "";
        public List<Donation> Donations {get; set;} = new List<Donation>();
        public User currentuser {get; set;} = new User();
    }
}