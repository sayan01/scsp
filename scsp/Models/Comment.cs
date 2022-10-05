using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace scsp.Models
{
    public class Comment
    {
        [Key] [Required]
        public int CommentID {get; set;}
        [Required]
        public User Author {get; set; } = new User();
        [Required]
        public Post Post {get; set; } = new Post();
        [Required]
        public string Content {get; set;} = "";
        [Required] [DataType(DataType.Date)]
        public DateTime Time {get; set; } = new DateTime();
    }
}