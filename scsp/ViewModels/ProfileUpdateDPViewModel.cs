using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;
namespace scsp.ViewModels
{
    public class ProfileUpdateDPViewModel
    {
        public User currentuser {get; set;} = new User();
        public string AlertMsg {get; set;} = "";
        public string AlertType {get; set;} = "";
    }
}