using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace scsp.Models
{
    public class Donation
    {
        [Key] [Required]
        public int DonationID {get; set;}
        [Required]
         [DataType(DataType.Date)]
        public DateTime Time {get; set;} = new DateTime();
        [Required]
        public double Amount {get; set;}
        [Required]
        public User User {get; set;} = new User();

    }
}