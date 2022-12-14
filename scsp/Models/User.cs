using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scsp.Models
{
    public class User
    {
        [Key] [Required]
        public string UserID {get; set;} = "";
        [Required]
        public string PasswordHash {get; set;} = "";
        [Required]
        public string FName {get; set;} = "";
        public string? LName {get; set;} = "";
        public string? Bio {get; set;} = "";
        public string Photo {get; set; } = "";

        // foreign key to user table (follows)
        [InverseProperty("Follower")]

        public ICollection<Foll> Follows {get; set;} = new List<Foll>();
        [InverseProperty("Followee")]
        public ICollection<Foll> FollowedBy {get; set;} = new List<Foll>();

        // posts created by user
        [InverseProperty("Author")]
        public ICollection<Post> Posts {get; set;} = new List<Post>();

        // donations made by user
        [InverseProperty("User")]
        public ICollection<Donation> Donations {get; set;} = new List<Donation>();

        //messages
        [InverseProperty("From")]
        public ICollection<Message> MessagesSent {get; set;} = new List<Message>();

        [InverseProperty("To")]
        public ICollection<Message> MessagesRecieved {get; set;} = new List<Message>();
    }
}