using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace scsp.Models
{
    public class Message
    {
        [Key] [Required]
        public int MessageID {get; set;}
        [Required]
        public User From {get; set;} = new User();
        [Required]
        public User To {get; set;} = new User();
        [Required]
        public string Content {get; set;} = "";
        [Required] [DataType(DataType.Date)]
        public DateTime Time {get; set;} = new DateTime();
        
    }
}