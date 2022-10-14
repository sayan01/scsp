using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scsp.Models
{
    public class Message
    {
        [Key] [Required]
        public int MessageID {get; set;}
        [ForeignKey("From")]
        public string FromId {get; set;} = "";
        [Required]
        public User From {get; set;} = new User();
        [ForeignKey("To")]
        public string ToId {get; set;} = "";
        [Required]
        public User To {get; set;} = new User();
        [Required]
        public string Content {get; set;} = "";
        [Required] [DataType(DataType.Date)]
        public DateTime Time {get; set;} = new DateTime();
        
    }
}