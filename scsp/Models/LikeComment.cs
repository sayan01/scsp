using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scsp.Models
{
    public class LikeComment
    {
        [Key] [Required]
        public int LikeID {get; set;}

        // foreign key
        [ForeignKey("Author")]
        public string AuthorId {get; set;} = "";
        [Required]
        public User Author {get; set; } = new User();
        [Required]
        public Comment Comment {get; set; } = new Comment();
    }
}