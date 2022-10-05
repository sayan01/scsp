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
        
        // foreign key to photo table
        public int? PhotoID {get; set;}
        public Photo? Photo {get; set; }

        // foreign key to user table (follows)
        public ICollection<User>? Follows {get; set;}
        [InverseProperty("Follows")]
        public ICollection<User>? FollowedBy {get; set;}

        // posts created by user
        [InverseProperty("Author")]
        public ICollection<Post>? Posts {get; set;}

        // donations made by user
        [InverseProperty("User")]
        public ICollection<Donation>? Donations {get; set;}

        //messages
        [InverseProperty("From")]
        public ICollection<Message>? MessagesSent {get; set;}

        [InverseProperty("To")]
        public ICollection<Message>? MessagesRecieved {get; set;}
    }
}