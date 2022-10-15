using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using scsp.Models;

namespace scsp.ViewModels
{
    public class AdminIndexViewModel
    {
        public int comments {get; set;}
        public int posts {get; set;}
        public int registrations {get; set;}
        public int messages {get; set;}
        public int likes {get; set;}
        public int dislikes {get; set;}
        public int donations {get; set;}
        public double donationTotal {get; set;}
    }
}