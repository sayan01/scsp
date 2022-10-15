using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scsp.Models
{
    public class Comment
    {
        [Key] [Required]
        public int CommentID {get; set;}

        // foreign key
        [ForeignKey("Author")]
        public string AuthorId {get; set;} = "";
        [Required]
        public User Author {get; set; } = new User();
        [ForeignKey("Post")]
        public int PostId {get; set;} 
        [Required]
        public Post Post {get; set; } = new Post();
        [Required]
        public string Content {get; set;} = "";
        [Required] [DataType(DataType.Date)]
        public DateTime Time {get; set; } = new DateTime();

        [InverseProperty("Comment")]
        public ICollection<LikeComment> Likes {get; set;} = new List<LikeComment>();
        [InverseProperty("Comment")]
        public ICollection<DislikeComment> Dislikes {get; set;} = new List<DislikeComment>();
    }
}