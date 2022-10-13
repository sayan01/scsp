using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace scsp.Models
{
    public class DislikePost
    {
        [Key] [Required]
        public int DislikeID {get; set;}
        [Required]
        public User Author {get; set; } = new User();
        [Required]
        public Post Post {get; set; } = new Post();
    }
}