using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scsp.ViewModels
{
    public class DonationCreateViewModel
    {
        public string AlertMsg {get; set;} = "";
        public string AlertType {get; set;} = "";
        public double Amount {get; set;}
    }
}