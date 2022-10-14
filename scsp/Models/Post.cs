using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scsp.Models
{
    public class Post
    {
        [Key] [Required]
        public int PostID {get; set;}
        [Required]
        public string Content {get; set;} = "";
        [Required]
         [DataType(DataType.Date)]
        public DateTime Time {get; set; } = new DateTime();


        // foreign key
        [ForeignKey("Author")]
        public string AuthorId {get; set;} = "";
        // user(author)
        [Required]
        public User Author {get; set;} = new User();

        // photo
        public string Photo {get; set;} = "";

        // comments
        [InverseProperty("Post")]
        public ICollection<Comment> Comments {get; set;} = new List<Comment>();


        [InverseProperty("Post")]
        public ICollection<LikePost> Likes {get; set;} = new List<LikePost>();
        [InverseProperty("Post")]
        public ICollection<DislikePost> Dislikes {get; set;} = new List<DislikePost>();
    }
}